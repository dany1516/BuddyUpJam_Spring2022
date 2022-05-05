using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jigsaw Puzzle", menuName = "Jigsaw/Create New Jigsaw Puzzle", order = 0)]
public class JigsawPuzzleSO : ScriptableObject 
{
    [SerializeField] int puzzleRows;
    [SerializeField] int puzzleColumns;
    [SerializeField] float jigsawPieceSize;
    [SerializeField] List<Sprite> jigsawPieces;

    public int GetPuzzleRows() => puzzleRows;
    public int GetPuzzleColumns() => puzzleColumns;
    public float GetJigsawPiecesSize() => jigsawPieceSize;
    public List<Sprite> GetJigsawPieces() => jigsawPieces;
}

[System.Serializable]
public class PuzzlePiece
{
    public Sprite pieceSprite;
}