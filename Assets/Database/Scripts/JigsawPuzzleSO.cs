using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jigsaw Puzzle", menuName = "Jigsaw/Create New Jigsaw Puzzle", order = 0)]
public class JigsawPuzzleSO : ScriptableObject 
{
    [SerializeField] PuzzlePiece[] jigsawPiece;
    [SerializeField] int puzzleRows;
    [SerializeField] int puzzleColumns;

    public PuzzlePiece[] GetJigsawPieces() => jigsawPiece;
    public int GetPuzzleRows() => puzzleRows;
    public int GetPuzzleColumns() => puzzleColumns;
}

[System.Serializable]
public class PuzzlePiece
{
    public Sprite pieceSprite;
    public Vector3 piecePosition;
}