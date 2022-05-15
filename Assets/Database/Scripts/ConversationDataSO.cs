using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Create New Conversation", order = 2)]
public class ConversationDataSO : ScriptableObject 
{
    [SerializeField] List<ConversationInput> conversationInput;
    [SerializeField] int section = 0;
    [SerializeField] int numberOfPieces = 0;

    public List<ConversationInput> GetConversationInput() => conversationInput;
    public int GetSection() => section;
    public int GetNumberOfPieces() => numberOfPieces;
}

[System.Serializable]
public class ConversationInput
{
    [TextArea(4,5)] public string dialogueLine;
    public ConversationParticipants convParticipant;
    public UnityEvent OtherFunctions;

    void CallOtherFunctions()
     {
         OtherFunctions.Invoke();
     }
}

public enum ConversationParticipants
{
    Dad,
    Girl
}