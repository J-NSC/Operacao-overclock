using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public static LevelCreator Instance;

    [SerializeField] int _rows;
    [SerializeField] int _columns;
    [SerializeField] int _spawnRows;
    [SerializeField] int _spawnColumns;
    [SerializeField] Transform _spawnBGPrefab;
    [SerializeField] Level _level;
    [SerializeField] Cell _cellPrefab;
    [SerializeField] Transform _centerPrefab;
    [SerializeField] float _blockSpawnSize = 0.5f;
    [SerializeField] List<Sprite> _blockSprites;
    [SerializeField] SpawnedBlock _blockPrefab;

    bool isNewLevel;
    Cell[,] gridCells;
    int currentCellFillValue;
    Dictionary<int, Vector2Int> startCenters;
    List<Transform> centerObjects;
    Dictionary<int, SpawnedBlock> spawnedBlocks;
    Vector3 startPos;

    void Awake()
    {
        Instance = this;
        SpawnBlock();
        SpawnGrid();
    }

    void SpawnBlock()
    {
        isNewLevel = !(_rows == _level.Rows && _columns == _level.Columns);

        if (isNewLevel)
        {
            _level.Rows = _rows;
            _level.Columns = _columns;
            _level.BlocksRows = _spawnRows;
            _level.BlocksColumns = _spawnColumns;
            _level.blocks = new List<BlockPiece>();
            _level.Data = new List<int>();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    _level.Data.Add(-1);
                }
            }
        }

        gridCells = new Cell[_rows, _columns];
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                gridCells[i, j] = Instantiate(_cellPrefab);
                gridCells[i, j].Init(_level.Data[i * _columns + j]);
                gridCells[i, j].transform.position = new Vector3(j + 0.5f, i + 0.5f, 0f);
            }
        }

        currentCellFillValue = -1;
    }

    void SpawnGrid()
    {
        startPos = Vector3.zero;
        startPos.x = 0.25f + (_level.Columns - _level.BlocksColumns * _blockSpawnSize) * 0.5f;
        startPos.y = -_level.BlocksRows * +_blockSpawnSize - 1f + 0.25f;

        for (int i = 0; i < _spawnRows; i++)
        {
            for (int j = 0; j < _spawnColumns; j++)
            {
                Vector3 spawnPos = startPos + new Vector3(j, i, 0) * _blockSpawnSize;
                Transform spawnCell = Instantiate(_spawnBGPrefab);
                spawnCell.position = spawnPos;
            }
        }

        float maxColumns = Mathf.Max(_level.Columns, _level.BlocksColumns * _blockSpawnSize);
        float maxRows = _level.Rows + 2f + _level.BlocksRows * _blockSpawnSize;
        Camera.main.orthographicSize = Mathf.Max(maxColumns, maxRows) * 0.65f;
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = _level.Columns * 0.5f;
        camPos.y = (_level.Rows + 0.5f + startPos.y) * 0.5f;
        Camera.main.transform.position = camPos;

        //Set StartCenters
        startCenters = new Dictionary<int, Vector2Int>();
        centerObjects = new List<Transform>();
        spawnedBlocks = new Dictionary<int, SpawnedBlock>();

        List<Sprite> sprites = _blockSprites;

        for (int i = 1; i < sprites.Count; i++)
        {
            spawnedBlocks[i - 1] = null;
            startCenters[i - 1] = Vector2Int.zero;
            centerObjects.Add(Instantiate(_centerPrefab));
            centerObjects[i - 1].GetChild(0).GetComponent<SpriteRenderer>().sprite =
                sprites[i];
            centerObjects[i - 1].gameObject.SetActive(false);
        }

        for (int i = 0; i < _level.blocks.Count; i++)
        {
            int tempId = _level.blocks[i].Id;
            Vector2Int pos = _level.blocks[i].CenterPos;
            centerObjects[tempId].gameObject.SetActive(true);
            centerObjects[tempId].transform.position =
                new Vector3(pos.y + 0.5f, pos.x + 0.5f, 0f);
            spawnedBlocks[tempId] = Instantiate(_blockPrefab);
            spawnedBlocks[tempId].Init(_level.blocks[i], startPos);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Set Grid Pos
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int mousePosGrid = new Vector2Int(
                Mathf.FloorToInt(mousePos.y),
                Mathf.FloorToInt(mousePos.x)
                );
            if (!IsValidPosition(mousePosGrid)) return;
            gridCells[mousePosGrid.x, mousePosGrid.y].Init(currentCellFillValue);
            _level.Data[mousePosGrid.x * _columns + mousePosGrid.y] = currentCellFillValue;
            EditorUtility.SetDirty(_level);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int mousePosGrid = new Vector2Int(
                Mathf.FloorToInt(mousePos.y),
                Mathf.FloorToInt(mousePos.x)
                );
            if (!IsValidPosition(mousePosGrid)) return;
            if (currentCellFillValue == -1) return;            
            centerObjects[currentCellFillValue].gameObject.SetActive(true);
            centerObjects[currentCellFillValue].transform.position = new Vector3(
                mousePosGrid.y + 0.5f,
                mousePosGrid.x + 0.5f,
                0
                );
            startCenters[currentCellFillValue] = mousePosGrid;
            EditorUtility.SetDirty(_level);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (currentCellFillValue == -1) return;
            BlockPiece spawnedPiece = GetBlockPiece();
            for (int i = 0; i < _level.blocks.Count; i++)
            {
                if (_level.blocks[i].Id == spawnedPiece.Id)
                {
                    _level.blocks.RemoveAt(i);
                    i--;
                }
            }
            _level.blocks.Add(spawnedPiece);
            if (spawnedBlocks[currentCellFillValue] != null)
            {
                Destroy(spawnedBlocks[currentCellFillValue].gameObject);
            }
            spawnedBlocks[currentCellFillValue] = Instantiate(_blockPrefab);
            spawnedBlocks[currentCellFillValue].Init(spawnedPiece, startPos);
            EditorUtility.SetDirty(_level);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            MoveBlock(Vector2Int.down);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            MoveBlock(Vector2Int.up);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            MoveBlock(Vector2Int.left);
        }
        else if( Input.GetKeyDown(KeyCode.W))
        {
            MoveBlock(Vector2Int.right);
        }
    }

    void MoveBlock(Vector2Int offset)
    {
        for (int i = 0; i < _level.blocks.Count; i++)
        {
            if (_level.blocks[i].Id == currentCellFillValue)
            {
                Vector2Int pos = _level.blocks[i].StartPos;
                pos.x += offset.x;
                pos.y += offset.y;
                BlockPiece piece = _level.blocks[i];
                piece.StartPos = pos;
                _level.blocks[i] = piece;
                Vector3 movePos = spawnedBlocks[currentCellFillValue].transform.position;
                movePos.x += offset.y * _blockSpawnSize;
                movePos.y += offset.x * _blockSpawnSize;
                spawnedBlocks[currentCellFillValue].transform.position = movePos;
            }
        }
        EditorUtility.SetDirty(_level);
    }

    BlockPiece GetBlockPiece()
    {
        int id = currentCellFillValue;
        BlockPiece result = new BlockPiece();
        result.Id = id;
        result.CenterPos = startCenters[id];
        result.StartPos = Vector2Int.zero;
        result.BlocksPositions = new List<Vector2Int>();
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                if (gridCells[i,j].CellValue == id)
                {
                    result.BlocksPositions.Add(new Vector2Int(i, j) - result.CenterPos);
                }
            }
        }
        return result;
    }

    bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < _rows && pos.y < _columns;
    }

    public void ChangeCellFillValue(int value)
    {
        currentCellFillValue = value;
    }
}