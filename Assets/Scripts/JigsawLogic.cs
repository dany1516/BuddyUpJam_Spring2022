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
    [SerializeField] float colliderSize;
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
            ChangeValues(true);
            return;
        }
        ChangeValues(false);
    }

    void CreatePieces()
    {
        for(int i = 0; i < jigsawPuzzle[level].GetJigsawPieces().Count; i++)
        {
            var newPiece = new GameObject(pieceName + (i));

            jigsawNewPieces.Add(newPiece);
            newPiece.AddComponent<SpriteRenderer>().sprite = jigsawPuzzle[level].GetJigsawPieces()[i];
            newPiece.AddComponent<BoxCollider2D>().size = new Vector2(colliderSize, colliderSize);
            newPiece.AddComponent<MovePiece>();
        } 
    }
    void ChangeValues(bool isLoadFile)
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
                new Vector3(Random.Range(jigsawPuzzle[level].GetMinX(), jigsawPuzzle[level].GetMaxX()),
                    Random.Range(jigsawPuzzle[level].GetMinY(), jigsawPuzzle[level].GetMaxY()), thirdCoordValue); 
            }
            
        }    
    }

    public bool SnapPiece(GameObject piece)
    {
        var posOnX = int.Parse(piece.name.Replace(pieceName, "")) % jigsawPuzzle[level].GetPuzzleColumns();
        var posOnY = int.Parse(piece.name.Replace(pieceName, "")) / jigsawPuzzle[level].GetPuzzleColumns();
        var correctPosition = new Vector3(posOnX * jigsawPuzzle[level].GetJigsawPiecesWidth(), posOnY * jigsawPuzzle[level].GetJigsawPiecesHeight() * -1, thirdCoordValue);
        var dist = Vector3.Distance(piece.transform.position, correctPosition);

        if(dist < distanceToSnap)
        {
            piece.transform.position = correctPosition;
            Destroy(piece.GetComponent<BoxCollider2D>());
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
