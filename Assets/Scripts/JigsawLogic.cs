using System.Collections.Generic;
using UnityEngine;

public class JigsawLogic : MonoBehaviour
{
    [Header("Pieces Values")]
    [SerializeField] List<JigsawPuzzleSO> jigsawPuzzle;
    [SerializeField][Range(0,5)] float distanceToSnap;

    [Header("Save Data file")]
    [SerializeField] SaveDataSO saveData;
    [SerializeField] SettingsDataSO settingsData;

    [Header("Dialogue")]
    [SerializeField] GameObject conv;

    [Header("Editor Settings")]
    [SerializeField] string pieceName;
    [SerializeField] int piecesCompleted;

    List<GameObject> jigsawNewPieces;
    [SerializeField]List<GameObject> puzzleMat;
    ConversationHandler conversation;
    
    int section;

    void Start() 
    {
        conversation = conv.GetComponent<ConversationHandler>();
    }
    public void StartNewPuzzle(int section)
    {
        InitialisingVariables(section);
        CreatePieces();
        ChangePosition(false);
        CreateJigsawMat();
    }

    public void LoadPreviewsPuzzle()
    {
        if (saveData.CanLoad())
        {
            LoadVariables();
            CreatePieces();
            section = saveData.GetCurrentSection();
            ChangePosition(true);
            CreateJigsawMat();
        }  
    }

    void InitialisingVariables(int section)
    {
        this.section = section - 1;
        saveData.SetCurrentSection(section);
        jigsawNewPieces = new List<GameObject>(0);
        piecesCompleted = 0;
    }

    void LoadVariables()
    {
        section = saveData.GetCurrentSection();
        jigsawNewPieces = new List<GameObject>(0);
        piecesCompleted = saveData.GetCurrentPieces();
    }

    void CreateJigsawMat()
    {
        var mat = jigsawPuzzle[section].GetJigsawMat();
        puzzleMat.Add(Instantiate(mat[0], mat[0].transform.position ,mat[0].transform.rotation));
        if (settingsData.GetIsEasyMode())
        {
            puzzleMat.Add(Instantiate(mat[1], mat[1].transform.position, mat[1].transform.rotation));
        }
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
            newPiece.GetComponent<SpriteRenderer>().sortingOrder = 5;
        } 
    }

    void ChangePosition(bool isLoadFile)
    {
        for(int i = 0; i < jigsawNewPieces.Count; i++) 
        {
            jigsawNewPieces[i].transform.parent = this.gameObject.transform;
            if(isLoadFile)
            {
                jigsawNewPieces[i].transform.position = saveData.LoadPieces()[i].position;
                jigsawNewPieces[i].transform.rotation = saveData.LoadPieces()[i].rotation;
                piecesCompleted = saveData.GetCurrentPieces();
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
        var snapAtDistance = distanceToSnap * jigsawPuzzle[section].GetJigsawPiecesHeight();
        var posOnZ = int.Parse(piece.name.Replace(pieceName, "")) % jigsawPuzzle[section].GetPuzzleColumns();
        var posOnX = int.Parse(piece.name.Replace(pieceName, "")) / jigsawPuzzle[section].GetPuzzleColumns();
        var correctPosition = new Vector3(
                                        posOnX * jigsawPuzzle[section].GetJigsawPiecesHeight() + jigsawPuzzle[section].GetJigsawAdjustment().x, 
                                        jigsawPuzzle[section].GetJigsawAdjustment().y, 
                                        posOnZ * jigsawPuzzle[section].GetJigsawPiecesWidth() + jigsawPuzzle[section].GetJigsawAdjustment().z);
        var dist = Vector3.Distance(piece.transform.position, correctPosition);

        if(dist < distanceToSnap)
        {
            SnapLogic(piece, correctPosition);
            return true;
        }
        return false;
    }

     void SnapLogic(GameObject piece, Vector3 correctPosition)
    {
        piece.transform.position = correctPosition;
        Destroy(piece.GetComponent<BoxCollider>());
        piecesCompleted += 1;
        conversation.gameObject.SetActive(true);
        conversation.CheckIfPieces(saveData.GetTotalPieces() + piecesCompleted);
        saveData.SavePieces(jigsawNewPieces, piecesCompleted, section);
        piece.GetComponent<SpriteRenderer>().sortingOrder = 1;

        if (piecesCompleted == jigsawNewPieces.Count)
        {
            saveData.SectionCompleted(piecesCompleted);
            conversation.gameObject.SetActive(true);
            conversation.CheckIfSection(section + 1);
        }
    }

    void DestroyMat()
    {
        for(int i = 0; i < puzzleMat.Count; i++) 
        {
            Destroy(puzzleMat[i].gameObject);
        }
        puzzleMat.Clear();
    }

    public void DestroyPieces()
    {
        foreach(GameObject piece in jigsawNewPieces)
        {
            Destroy(piece.gameObject);
        }
        jigsawNewPieces.Clear();
        DestroyMat();
    }
}
