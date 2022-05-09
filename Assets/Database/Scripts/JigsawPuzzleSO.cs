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
    [SerializeField] Vector3 min;
    [SerializeField] Vector3 max;

    public Vector3 GetMinVector() => min;
    public Vector3 GetMaxVector() => max;
}

[System.Serializable]
public class PuzzlePiece
{
    public Sprite pieceSprite;
}
