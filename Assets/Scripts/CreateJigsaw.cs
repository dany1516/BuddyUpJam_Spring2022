using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Tags
{
    PuzzlePiece,
    None
}

public class CreateJigsaw : MonoBehaviour
{
    [Header("Pieces Values")]
    [SerializeField] JigsawPuzzleSO jigsawPuzzle;
    [SerializeField] Tags pieceTag;

    [Header("Coordonates to spawn pieces")]
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    List<GameObject> jigsawNewPiece;

    void Start()
    {
        jigsawNewPiece = new List<GameObject>(0);
        CreatePieces();
        ChangeValues();
    }

    private void ClearJigsaw()
    {
        jigsawNewPiece.Clear();
    }

    private void ChangeValues()
    {
        var piecePosition = new Vector3(0, 0, 0);

        for(int i = 0; i < jigsawPuzzle.GetPuzzleRows(); i++) 
        {
            for(int j = 0; j < jigsawPuzzle.GetPuzzleColumns(); j++) 
            {
                var currentPiece = jigsawPuzzle.GetPuzzleRows() * i + j;

                jigsawNewPiece[currentPiece].tag = pieceTag.ToString();
                jigsawNewPiece[currentPiece].transform.parent = this.gameObject.transform;
                jigsawNewPiece[currentPiece].transform.position = this.gameObject.transform.position + 
                    new Vector3(Random.Range(minX,maxX), Random.Range(minY,maxY),0);
                piecePosition.x += jigsawPuzzle.GetJigsawPiecesSize();   
            }
            piecePosition.x = 0;   
            piecePosition.y -= jigsawPuzzle.GetJigsawPiecesSize();
        }    
    }

    private void CreatePieces()
    {
        for(int i = 0; i < jigsawPuzzle.GetJigsawPieces().Count; i++)
        {
            var newPiece = new GameObject("JigsawPiece" + (i));

            jigsawNewPiece.Add(newPiece);
            newPiece.AddComponent<SpriteRenderer>().sprite = jigsawPuzzle.GetJigsawPieces()[i];
            newPiece.AddComponent<BoxCollider2D>();
            newPiece.AddComponent<MovePiece>();

            Debug.Log(newPiece.name);
        } 
    }

    void Update()
    {
        
    }
}
