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

    [Header("Save Data file")]
    [SerializeField] SaveDataSO saveFile;

    [Header("Editor Settings")]
    [SerializeField] string pieceName;
    //To Be refabed
    [SerializeField] int piecesLeft;

    List<GameObject> jigsawNewPiece;

    void Start()
    {
        jigsawNewPiece = new List<GameObject>(0);
        CreatePieces();
        piecesLeft = jigsawNewPiece.Count;
        if(saveFile.CanLoad())
        {
            ChangeValues(true);
            return;
        }
        ChangeValues(false);
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
    void ChangeValues(bool isLoadFile)
    {
        for(int i = 0; i < jigsawNewPiece.Count; i++) 
        {
            jigsawNewPiece[i].tag = pieceTag.ToString();
            jigsawNewPiece[i].transform.parent = this.gameObject.transform;
            if(isLoadFile)
            {
                jigsawNewPiece[i].transform.position = this.gameObject.transform.position + saveFile.LoadPieces()[i].position;
            }
            else
            {
                jigsawNewPiece[i].transform.position = this.gameObject.transform.position + 
                new Vector3(Random.Range(jigsawPuzzle.GetMinX(), jigsawPuzzle.GetMaxX()),
                    Random.Range(jigsawPuzzle.GetMinY(), jigsawPuzzle.GetMaxY()), thirdCoordValue); 
            }
            
        }    
    }

    public bool SnapPiece(GameObject piece)
    {
        var posOnX = int.Parse(piece.name.Replace(pieceName, "")) % jigsawPuzzle.GetPuzzleRows();
        var posOnY = int.Parse(piece.name.Replace(pieceName, "")) / jigsawPuzzle.GetPuzzleRows();
        var correctPosition = new Vector3(posOnX * jigsawPuzzle.GetJigsawPiecesSize(), posOnY * jigsawPuzzle.GetJigsawPiecesSize() * -1, thirdCoordValue);
        var dist = Vector3.Distance(piece.transform.position, correctPosition);

        //To Be fixed bug when click on snap piece the counter acts
        if(dist < distanceToSnap)
        {
            piece.transform.position = correctPosition;
            piecesLeft -= 1;
            if(piecesLeft <= 0)
            {
                Debug.Log("Level complete");
            }
            return true;
        }
        return false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            saveFile.SavePieces(jigsawNewPiece);
        }
    }
}
