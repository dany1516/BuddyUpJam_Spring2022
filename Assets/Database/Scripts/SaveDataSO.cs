using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save File", menuName = "Create new Save File", order = 1)]
public class SaveDataSO : ScriptableObject 
{
    [SerializeField] List<PuzzlePieceData> pieces;
    [SerializeField] int section;

    public List<PuzzlePieceData> LoadPieces() => pieces;
    public int GetCurrentLevel() => section - 1;

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
        for(int i = 0; i < piecesToSave.Count; i++) 
        {
            var newElement = new PuzzlePieceData();
            newElement.position = piecesToSave[i].transform.position;
            pieces.Add(newElement);
        }
    }

    public int NewSection()
    {
        ResetPieces();
        return section - 1;
    }

    public void SetSection(int section)
    {
        this.section = section + 1;
    }

    void ResetPieces()
    {
        pieces = new List<PuzzlePieceData>();
    }

    [System.Serializable]
    public class PuzzlePieceData
    {
        public Vector3 position;
    }
}
