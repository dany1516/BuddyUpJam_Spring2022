using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Tags
{
    PuzzlePiece,
    None
}

public class JigsawLogic : MonoBehaviour
{
    [Header("Pieces Values")]
    [SerializeField] JigsawPuzzleSO jigsawPuzzle;
    [SerializeField] Tags pieceTag;
    [SerializeField][Range(0,5)] float distanceToSnap;
    [SerializeField][Range(0,2)] int thirdCoordValue;

    [Header("Coordonates to spawn pieces")]
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    [Header("Editor Settings")]
    [SerializeField] string pieceName;

    List<GameObject> jigsawNewPiece;

    void Start()
    {
        //To Be Refab change value with ones from save settings
        jigsawNewPiece = new List<GameObject>(0);
        CreatePieces();
        ChangeValues();
    }

    void ChangeValues()
    {
        for(int i = 0; i < jigsawNewPiece.Count; i++) 
        {
            jigsawNewPiece[i].tag = pieceTag.ToString();
            jigsawNewPiece[i].transform.parent = this.gameObject.transform;
            jigsawNewPiece[i].transform.position = this.gameObject.transform.position + 
                 new Vector3(Random.Range(minX,maxX), Random.Range(minY,maxY), thirdCoordValue); 
        }    
    }

    void CreatePieces()
    {
        for(int i = 0; i < jigsawPuzzle.GetJigsawPieces().Count; i++)
        {
            var newPiece = new GameObject(pieceName + (i));

            jigsawNewPiece.Add(newPiece);
            newPiece.AddComponent<SpriteRenderer>().sprite = jigsawPuzzle.GetJigsawPieces()[i];
            newPiece.AddComponent<BoxCollider2D>();
            newPiece.AddComponent<MovePiece>();
        } 
    }

    public void SnapPiece(GameObject piece)
    {
        var posOnX = int.Parse(piece.name.Replace(pieceName, "")) % jigsawPuzzle.GetPuzzleColumns();
        var posOnY = int.Parse(piece.name.Replace(pieceName, "")) / jigsawPuzzle.GetPuzzleRows();
        var correctPosition = new Vector3(posOnX * jigsawPuzzle.GetJigsawPiecesSize(), posOnY * jigsawPuzzle.GetJigsawPiecesSize() * -1, thirdCoordValue);
        var dist = Vector3.Distance(piece.transform.position, correctPosition);

        if(dist < distanceToSnap)
        {
            piece.transform.position = correctPosition;
        } 
    }

    void Update()
    {
        
    }
}
