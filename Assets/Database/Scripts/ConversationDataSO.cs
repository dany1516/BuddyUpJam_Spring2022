using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Create New Conversation", order = 2)]
public class ConversationDataSO : ScriptableObject 
{
    [SerializeField] List<ConversationInput> conversationInput;
    [SerializeField] bool isTriggerAtSection = false;
    [SerializeField] int section = 0;
    [SerializeField] bool isTriggerAtPieces = false;
    [SerializeField] int numberOfPieces = 0;
    [SerializeField] bool isOther;
    [SerializeField] bool isRepeatable;
    [SerializeField] bool hasBeenPlayed = false;

    
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