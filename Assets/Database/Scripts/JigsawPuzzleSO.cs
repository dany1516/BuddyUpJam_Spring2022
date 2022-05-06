using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jigsaw Puzzle", menuName = "Jigsaw/Create New Jigsaw Puzzle", order = 2)]
public class JigsawPuzzleSO : ScriptableObject 
{
    [Header("Pieces Specs")]
    [SerializeField] int puzzleRows;
    [SerializeField] int puzzleColumns;
    [SerializeField] float jigsawPieceSize;
    [SerializeField] List<Sprite> jigsawPieces;

    public int GetPuzzleRows() => puzzleRows;
    public int GetPuzzleColumns() => puzzleColumns;
    public float GetJigsawPiecesSize() => jigsawPieceSize;
    public List<Sprite> GetJigsawPieces() => jigsawPieces;
    
    [Header("Spawn pieces area")]
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    public float GetMinX() => minX;
    public float GetMaxX() => maxX;
    public float GetMinY() => minY;
    public float GetMaxY() => maxY;
    
}

[System.Serializable]
public class PuzzlePiece
{
    public Sprite pieceSprite;
}