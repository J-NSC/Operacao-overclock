using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] Level level;
    [SerializeField] BGCell bgCellPrefab;
    [SerializeField] Block blockPrefab;
    [SerializeField] float blockSpawnSize;
    [SerializeField] float blockHighLightSize;
    [SerializeField] float blockPutSize;

    BGCell[,] bgCellsGrid;
    bool hasGameFinished;
    Block currentBlock;
    Vector2 currentPos, previousPos;
    List<Block> gridBlocks;


    void Awake()
    {
        instance = this;

        hasGameFinished = false;

        gridBlocks = new List<Block>();

        SpwanGrid();
        SpawnBlocks();
    }

    void SpwanGrid()
    {
        bgCellsGrid = new BGCell[level.Rows, level.Columns];

        for (int i = 0; i < level.Rows; i++)
        {
            for (int j = 0; j < level.Columns; j++)
            {
                BGCell bgCell = Instantiate(bgCellPrefab);
                bgCell.transform.position = new Vector3(j + 0.5f, i + 0.5f, 0f);
                bgCell.Init(level.Data[i * level.Columns + j]);
                bgCellsGrid[i, j] = bgCell;
            }
        }
    }

    void SpawnBlocks()
    {
        Vector3 startPos = Vector3.zero;
        startPos.x = 0.25f + (level.Columns - level.BlocksColumns * blockSpawnSize) * .5f;
        startPos.y = - level.BlocksColumns * blockSpawnSize  + 0.25f - 1f;


        for (int i = 0; i < level.blocks.Count; i++)
        {
            Block block = Instantiate(blockPrefab);
            Vector2Int blockPos = level.blocks[i].StartPos;
            Vector3 bloackSpawnPos = startPos + new Vector3(blockPos.y , blockPos.x, 0f) * blockSpawnSize;
            block.transform.position = bloackSpawnPos;
            block.Init(level.blocks[i].BlocksPositions,bloackSpawnPos, level.blocks[i].Id);
        }
        
        float maxColums = Math.Max(level.Columns, level.BlocksColumns * blockSpawnSize);
        float maxRows = level.Rows + 2f + level.BlocksRows * blockSpawnSize;

        Camera.main.orthographicSize = Math.Max(maxColums, maxRows) * 0.65f;
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = level.Columns * .5f;
        camPos.y = (level.Rows + .5f + startPos.y) *.5f;
        Camera.main.transform.position = camPos;
    }

     private void Update()
    {
        if (hasGameFinished) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (!hit) return;
            currentBlock = hit.collider.transform.parent.GetComponent<Block>();
            if (currentBlock == null) return;
            currentPos = mousePos2D;
            previousPos = mousePos2D;
            currentBlock.ElevateSprites();
            currentBlock.transform.localScale = Vector3.one * blockHighLightSize;
            if(gridBlocks.Contains(currentBlock))
            {
                gridBlocks.Remove(currentBlock);
            }
            UpdateFilled();
            ResetHighLight();
            UpdateHighLight();
        }
        else if(Input.GetMouseButton(0) && currentBlock != null) 
        {
            currentPos = mousePos;
            currentBlock.UpdatePos(currentPos - previousPos);
            previousPos = currentPos;
            ResetHighLight();
            UpdateHighLight();
        }
        else if(Input.GetMouseButtonUp(0) && currentBlock != null)
        {
            currentBlock.ElevateSprites(true);
            
            if(IsCorrectMove())
            {
                currentBlock.UpdateCorrectMove();
                currentBlock.transform.localScale = Vector3.one * blockPutSize;
                gridBlocks.Add(currentBlock);
            }
            else if(mousePos2D.y < 0)
            {
                currentBlock.UpdateStartMove();
                currentBlock.transform.localScale = Vector3.one * blockSpawnSize;
            }
            else
            {
                currentBlock.UpdateIncorrectMove();
                if(currentBlock.CurrentPos.y > 0)
                {
                    gridBlocks.Add(currentBlock);
                    currentBlock.transform.localScale = Vector3.one * blockPutSize;
                }
                else
                {
                    currentBlock.transform.localScale = Vector3.one * blockSpawnSize;
                }
            }

            currentBlock = null;
            ResetHighLight();
            UpdateFilled();
            CheckWin();
        }
    }

    void ResetHighLight()
    {
        for (int i = 0; i < level.Rows; i++)
        {
            for (int j = 0; j < level.Columns; j++)
            {
                if (!bgCellsGrid[i,j].IsBlocked)
                {
                    bgCellsGrid[i,j].ResetHighLight();
                }
            }
        }
    }

    void UpdateFilled()
    {
        for (int i = 0; i < level.Rows; i++)
        {
            for (int j = 0; j < level.Columns; j++)
            {
                if (!bgCellsGrid[i,j].IsBlocked)
                {
                    bgCellsGrid[i,j].IsFilled = false;
                }
            }
        }

        foreach (var block in gridBlocks)
        {
            foreach (var pos in block.BlockPositions())
            {
                if (IsValidPos(pos))
                {
                    bgCellsGrid[pos.x, pos.y].IsFilled = true;
                }
            }
        }
    }


    void UpdateHighLight()
    {
        bool isCorrect = IsCorrectMove();
        foreach (var pos in currentBlock.BlockPositions())
        {
            if (IsValidPos(pos))
            {
                bgCellsGrid[pos.x, pos.y].UpdateHighLight(isCorrect);
            }
        }
    }

    bool IsCorrectMove()
    {
        foreach (var pos in currentBlock.BlockPositions())
        {
            if (!IsValidPos(pos) || bgCellsGrid[pos.x, pos.y].IsFilled)
            {
                return false;
            }
        }

        return true;
    }

    bool IsValidPos(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < level.Rows && pos.y < level.Columns;
    }

    void CheckWin()
    {
        for (int i = 0; i < level.Rows; i++)
        {
            for (int j = 0; j < level.Columns; j++)
            {
                if (!bgCellsGrid[i,j].IsFilled)
                {
                    return;
                }
            }
        }
        hasGameFinished = true;
        StartCoroutine(GameWin());
    }

    IEnumerator GameWin()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    
    


}
