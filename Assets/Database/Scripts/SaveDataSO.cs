using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save File", menuName = "Create new Save File", order = 1)]
public class SaveDataSO : ScriptableObject 
{
    [SerializeField] List<PuzzlePieceData> pieces;
    [SerializeField] int section;
    [SerializeField] List<bool> isCompleted;
    [SerializeField] int totalPieces;
    [SerializeField] int currentPieces;


    public List<PuzzlePieceData> LoadPieces() => pieces;
    public int GetCurrentSection() => section - 1;
    public void SetCurrentSection(int section) { this.section = section;}
    public int GetTotalPieces() => totalPieces;
    public int GetCurrentPieces() => currentPieces;

    public bool CanLoad()
    {
        if(pieces.Count > 0)
        {
            return true;
        }
        return false;
    }

    public void SavePieces(List<GameObject> piecesToSave, int currentPieces, int section)
    {
        ResetPieces();
        pieces = new List<PuzzlePieceData>(piecesToSave.Count);
        for(int i = 0; i < piecesToSave.Count; i++) 
        {
            var newElement = new PuzzlePieceData();
            newElement.position = piecesToSave[i].transform.position;
            newElement.rotation = piecesToSave[i].transform.rotation;
            pieces.Add(newElement);
        }
        this.currentPieces = currentPieces;
        this.section = section;
    }

    public void SectionCompleted(int numberOfPieces)
    {
        if(!isCompleted[section-1])
        {
            totalPieces += numberOfPieces;
            isCompleted[section - 1] = true;
            ResetPieces();
        }
    }

    public void SetSection(int section)
    {
        this.section = section + 1;
    }

    void ResetPieces()
    {
        pieces = new List<PuzzlePieceData>();
        currentPieces = 0;
    }

    [System.Serializable]
    public class PuzzlePieceData
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}
