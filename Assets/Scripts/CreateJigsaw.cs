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

    void ClearJigsaw()
    {
        jigsawNewPiece.Clear();
    }

    void ChangeValues()
    {
        for(int i = 0; i < jigsawNewPiece.Count; i++) 
        {
            jigsawNewPiece[i].tag = pieceTag.ToString();
            jigsawNewPiece[i].transform.parent = this.gameObject.transform;
            jigsawNewPiece[i].transform.position = this.gameObject.transform.position + 
                 new Vector3(Random.Range(minX,maxX), Random.Range(minY,maxY),0); 
        }    
    }

    void CreatePieces()
    {
        for(int i = 0; i < jigsawPuzzle.GetJigsawPieces().Count; i++)
        {
            var newPiece = new GameObject("JigsawPiece" + (i));

            jigsawNewPiece.Add(newPiece);
            newPiece.AddComponent<SpriteRenderer>().sprite = jigsawPuzzle.GetJigsawPieces()[i];
            newPiece.AddComponent<BoxCollider2D>();
            newPiece.AddComponent<MovePiece>();
        } 
    }

    void Update()
    {
        
    }
}
