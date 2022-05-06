using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save File", menuName = "Create new Save File", order = 1)]
public class SaveDataSO : ScriptableObject 
{
    [SerializeField] List<PuzzlePieceData> pieces;
    [SerializeField] int level;

    public bool CanLoad()
    {
        if(pieces.Count > 0)
        {
            return true;
        }
        return false;
    }

    public void SavePieces(List<GameObject> piecesToSave)
    {
        ResetPieces();
        pieces = new List<PuzzlePieceData>(piecesToSave.Count);
        Debug.Log("Save file Length is: " + pieces.Count + " and objects to save length is: " + piecesToSave.Count);
        for(int i = 0; i < piecesToSave.Count; i++) 
        {
            var newElement = new PuzzlePieceData();
            newElement.position = piecesToSave[i].transform.position;
            Debug.Log(newElement.position + "  " + piecesToSave[i].transform.position);
            newElement.inPlace = piecesToSave[i].GetComponent<MovePiece>().GetIfInPlace();
            pieces.Add(newElement);
        }
    }

    public List<PuzzlePieceData> LoadPieces()
    {
        return pieces;
    } 

    void ResetPieces()
    {
        pieces = new List<PuzzlePieceData>();
    }

    [System.Serializable]
    public class PuzzlePieceData
    {
        public Vector3 position;
        public bool inPlace;
    }
}

