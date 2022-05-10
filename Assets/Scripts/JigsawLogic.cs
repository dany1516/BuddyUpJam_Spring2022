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
    [SerializeField][Range(0,5)] float distanceToSnap;
    [SerializeField] Vector3 adjustmentVector;

    [Header("Save Data file")]
    [SerializeField] SaveDataSO saveFile;

    [Header("Editor Settings")]
    [SerializeField] string pieceName;
    [SerializeField] int piecesLeft;

    List<GameObject> jigsawNewPieces;
    
    int section;

    void Start()
    {
        jigsawNewPieces = new List<GameObject>(0);
        section = saveFile.GetCurrentLevel();
        CreatePieces();
        piecesLeft = jigsawNewPieces.Count;
        if(saveFile.CanLoad())
        {
            ChangePosition(true);
            return;
        }
        ChangePosition(false);
    }

    void CreatePieces()
    {
        for(int i = 0; i < jigsawPuzzle[section].GetJigsawPieces().Count; i++)
        {
            var newPiece = new GameObject(pieceName + (i));

            jigsawNewPieces.Add(newPiece);
            newPiece.AddComponent<SpriteRenderer>().sprite = jigsawPuzzle[section].GetJigsawPieces()[i];
            newPiece.AddComponent<BoxCollider>().size = new Vector3(jigsawPuzzle[section].GetJigsawPiecesWidth(), jigsawPuzzle[section].GetJigsawPiecesWidth(), 0.01f);
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
                jigsawNewPieces[i].transform.position = this.gameObject.transform.position + new Vector3(
                    Random.Range(jigsawPuzzle[section].GetMinVector().x, jigsawPuzzle[section].GetMaxVector().x),
                    Random.Range(jigsawPuzzle[section].GetMinVector().y, jigsawPuzzle[section].GetMaxVector().y),
                    Random.Range(jigsawPuzzle[section].GetMinVector().z, jigsawPuzzle[section].GetMaxVector().z));
                jigsawNewPieces[i].transform.rotation = this.gameObject.transform.rotation;
            }  
        }    
    }

    public bool SnapPiece(GameObject piece)
    {
        var posOnZ = int.Parse(piece.name.Replace(pieceName, "")) % jigsawPuzzle[section].GetPuzzleColumns();
        var posOnX = int.Parse(piece.name.Replace(pieceName, "")) / jigsawPuzzle[section].GetPuzzleColumns();
        var correctPosition = new Vector3(
                                        posOnX * jigsawPuzzle[section].GetJigsawPiecesHeight() + jigsawPuzzle[section].GetJigsawAdjustment().x, 
                                        jigsawPuzzle[section].GetJigsawAdjustment().y, 
                                        posOnZ * jigsawPuzzle[section].GetJigsawPiecesWidth() + jigsawPuzzle[section].GetJigsawAdjustment().z);
        var dist = Vector3.Distance(piece.transform.position, this.transform.position + correctPosition);

        if(dist < distanceToSnap)
        {
            piece.transform.position = correctPosition;
            Destroy(piece.GetComponent<BoxCollider>());
            piecesLeft -= 1;
            if(piecesLeft <= 0)
            {
                DestroyPieces();
                saveFile.NewSection();
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
