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

    [Header("Save Data file")]
    [SerializeField] SaveDataSO saveFile;

    [Header("Editor Settings")]
    [SerializeField] string pieceName;
    [SerializeField] int piecesCompleted;

    List<GameObject> jigsawNewPieces;
    
    int section;

    public void StartPuzzle(int section)
    {
        jigsawNewPieces = new List<GameObject>(0);
        this.section = section - 1;
        CreatePieces();
        piecesCompleted = 0;
        if(saveFile.CanLoad())
        {
            section = saveFile.GetCurrentSection();
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
                jigsawNewPieces[i].transform.position = saveFile.LoadPieces()[i].position;
                jigsawNewPieces[i].transform.rotation = saveFile.LoadPieces()[i].rotation;
                piecesCompleted = saveFile.GetCurrentPieces();
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
        Debug.Log("Position on X is: " + posOnX);
        Debug.Log("Position on Z is: " + posOnZ);
        var correctPosition = new Vector3(
                                        posOnX * jigsawPuzzle[section].GetJigsawPiecesHeight() + jigsawPuzzle[section].GetJigsawAdjustment().x, 
                                        jigsawPuzzle[section].GetJigsawAdjustment().y, 
                                        posOnZ * jigsawPuzzle[section].GetJigsawPiecesWidth() + jigsawPuzzle[section].GetJigsawAdjustment().z);
        Debug.Log("Correct position is: " + correctPosition);
        var dist = Vector3.Distance(piece.transform.position, correctPosition);
        Debug.Log("Distance is broken in: piece position -> " + piece.transform.position + " ,this game object position -> " + this.transform.position + " ,and correct position -> " + correctPosition);

        if(dist < distanceToSnap)
        {
            piece.transform.position = correctPosition;
            Debug.Log("Correct position is: " + correctPosition);
            Destroy(piece.GetComponent<BoxCollider>());
            piecesCompleted += 1;
            if(piecesCompleted == jigsawNewPieces.Count)
            {
                saveFile.SectionCompleted(piecesCompleted);
            }
            return true;
        }
        return false;
    }

    public void DestroyPieces()
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
            saveFile.SavePieces(jigsawNewPieces, piecesCompleted, section);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            DestroyPieces();
        }
    }
}
