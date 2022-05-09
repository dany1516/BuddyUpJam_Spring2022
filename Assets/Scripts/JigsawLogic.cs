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
    [SerializeField] List<JigsawPuzzleSO> jigsawPuzzle;
    [SerializeField] Tags pieceTag;
    [SerializeField] Vector3 colliderSize;
    [SerializeField][Range(0,5)] float distanceToSnap;
    [SerializeField][Range(0,2)] int thirdCoordValue;

    [Header("Save Data file")]
    [SerializeField] SaveDataSO saveFile;

    [Header("Editor Settings")]
    [SerializeField] string pieceName;
    [SerializeField] int piecesLeft;

    List<GameObject> jigsawNewPieces;
    int level;

    void Start()
    {
        jigsawNewPieces = new List<GameObject>(0);
        CreatePieces();
        piecesLeft = jigsawNewPieces.Count;
        level = saveFile.GetCurrentLevel();
        if(saveFile.CanLoad())
        {
            ChangePosition(true);
            return;
        }
        ChangePosition(false);
    }

    void CreatePieces()
    {
        for(int i = 0; i < jigsawPuzzle[level].GetJigsawPieces().Count; i++)
        {
            var newPiece = new GameObject(pieceName + (i));

            jigsawNewPieces.Add(newPiece);
            newPiece.AddComponent<SpriteRenderer>().sprite = jigsawPuzzle[level].GetJigsawPieces()[i];
            newPiece.AddComponent<BoxCollider>().size = colliderSize;
            newPiece.AddComponent<MovePiece>();
        } 
    }
    void ChangePosition(bool isLoadFile)
    {
        for(int i = 0; i < jigsawNewPieces.Count; i++) 
        {
            jigsawNewPieces[i].tag = pieceTag.ToString();
            jigsawNewPieces[i].transform.parent = this.gameObject.transform;
            if(isLoadFile)
            {
                jigsawNewPieces[i].transform.position = this.gameObject.transform.position + saveFile.LoadPieces()[i].position;
            }
            else
            {
                jigsawNewPieces[i].transform.position = this.gameObject.transform.position + 
                new Vector3(
                    Random.Range(jigsawPuzzle[level].GetMinVector().x, jigsawPuzzle[level].GetMaxVector().x),
                    Random.Range(jigsawPuzzle[level].GetMinVector().y, jigsawPuzzle[level].GetMaxVector().y),
                    Random.Range(jigsawPuzzle[level].GetMinVector().z, jigsawPuzzle[level].GetMaxVector().z));
                jigsawNewPieces[i].transform.rotation = this.gameObject.transform.rotation;
            }  
        }    
    }

    //To be refabed
    public bool SnapPiece(GameObject piece)
    {
        var posOnX = int.Parse(piece.name.Replace(pieceName, "")) % jigsawPuzzle[level].GetPuzzleColumns();
        var posOnY = int.Parse(piece.name.Replace(pieceName, "")) / jigsawPuzzle[level].GetPuzzleColumns();
        var correctPosition = new Vector3(posOnX * jigsawPuzzle[level].GetJigsawPiecesWidth(), posOnY * jigsawPuzzle[level].GetJigsawPiecesHeight() * -1, thirdCoordValue);
        var dist = Vector3.Distance(piece.transform.position, this.transform.position + correctPosition);

        if(dist < distanceToSnap)
        {
            piece.transform.position = correctPosition;
            Destroy(piece.GetComponent<BoxCollider>());
            piecesLeft -= 1;
            if(piecesLeft <= 0)
            {
                DestroyPieces();
                saveFile.NextLevel();
            }
            return true;
        }
        return false;
    }

    void DestroyPieces()
    {
        foreach(GameObject piece in jigsawNewPieces)
        {
            Destroy(piece.gameObject);
        }
        jigsawNewPieces.Clear();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            saveFile.SavePieces(jigsawNewPieces);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            DestroyPieces();
        }
    }
}
