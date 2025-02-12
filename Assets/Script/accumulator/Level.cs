using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName= "Level")]
public class Level : ScriptableObject
{
    public int Rows;
    public int Columns;
    public List<int> Data;
    public int BlocksRows;
    public int BlocksColumns;
    public List<BlockPiece> blocks;
}

[Serializable]
public struct BlockPiece
{
    public int Id;
    public Vector2Int StartPos;
    public Vector2Int CenterPos;
    public List<Vector2Int> BlocksPositions;
}
