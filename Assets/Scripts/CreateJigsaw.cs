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
    [SerializeField] JigsawPuzzleSO jigsawPuzzle;
    [SerializeField] Tags pieceTag;
    [SerializeField] float zValue;

    GameObject jigsawNewPiece;

    void Start()
    {
        for(int i = 0; i < jigsawPuzzle.GetJigsawPieces().Length; i++)
        {
            jigsawNewPiece = new GameObject("JigsawPiece" + (i));
            AddComponents(i);
            ModifyAttributes(i);
        }
    }

    private void ModifyAttributes(int i)
    {
        jigsawNewPiece.tag = pieceTag.ToString();
        jigsawNewPiece.transform.parent = this.gameObject.transform;
        jigsawNewPiece.transform.position = this.gameObject.transform.position + jigsawPuzzle.GetJigsawPieces()[i].piecePosition + new Vector3(-13,6,0);      
    }

    void RandomPosition()
    {
         
    }

    private void AddComponents(int i)
    {
        jigsawNewPiece.AddComponent<SpriteRenderer>().sprite = jigsawPuzzle.GetJigsawPieces()[i].pieceSprite;
        jigsawNewPiece.AddComponent<BoxCollider2D>();
        jigsawNewPiece.AddComponent<MovePiece>();   
    }

    void Update()
    {
        
    }
}
