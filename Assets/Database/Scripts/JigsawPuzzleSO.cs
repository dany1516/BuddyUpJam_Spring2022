using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jigsaw Puzzle", menuName = "Jigsaw/Create New Jigsaw Puzzle", order = 2)]
public class JigsawPuzzleSO : ScriptableObject 
{
    [Header("Pieces Specs")]
    [SerializeField] int puzzleColumns;
    [SerializeField] int puzzleRows;
    [SerializeField] float jigsawPieceWidth;
    [SerializeField] float jigsawPieceHeight;
    [SerializeField] List<Sprite> jigsawPieces;

    public int GetPuzzleColumns() => puzzleColumns;
    public int GetPuzzleRows() => puzzleRows;
    public float GetJigsawPiecesWidth() => jigsawPieceWidth;
    public float GetJigsawPiecesHeight() => jigsawPieceHeight;
    public List<Sprite> GetJigsawPieces() => jigsawPieces;
    
    [Header("Spawn pieces area")]
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;

    public float GetMinX() => minX;
    public float GetMaxX() => maxX;
    public float GetMinY() => minY;
    public float GetMaxY() => maxY;
    public float GetMinZ() => minZ;
    public float GetMaxZ() => maxZ;
}

[System.Serializable]
public class PuzzlePiece
{
    public Sprite pieceSprite;
}
