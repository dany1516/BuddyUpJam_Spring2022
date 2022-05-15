using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationHandler : MonoBehaviour
{
    [Header("Conversation UI  Stuff")]
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] GameObject dadImage;
    [SerializeField] GameObject girlImage;
    [SerializeField] Sprite[] dadSprites;
    [SerializeField] Sprite[] girlSprites;
    [SerializeField] float secondsToSkip;

    [Header("Stories")]
    [SerializeField] List<ConversationDataSO> sectionRelated;
    [SerializeField] List<ConversationDataSO> piecesRelated;

    [Header("Skipper")]
    [SerializeField] Button skipButton;

    bool isSectionsRelatedCompleted = false;
    int sectionToCompare = 0;
    int piecesToCompare = 0;

    public void CheckIfSection(int section)
    {
        sectionToCompare = section;
        StartCoroutine("WaitAtSection");
    }

    public void CheckIfPieces(int pieces)
    {
        piecesToCompare = pieces;
        StartCoroutine("WaitAtPieces");
    }

    IEnumerator WaitAtSection()
    {
        for(int i = 0; i < sectionRelated.Count; i++) 
        {
            if(sectionToCompare == sectionRelated[i].GetSection())
            { 
                skipButton.gameObject.SetActive(true);
                for(int j = 0; j < sectionRelated[i].GetConversationInput().Count; j++)
                {
                    textBox.text = sectionRelated[i].GetConversationInput()[j].dialogueLine;
                    if (sectionRelated[i].GetConversationInput()[j].convParticipant.ToString() == "Dad")
                    {
                        dadImage.GetComponent<Image>().sprite = dadSprites[0];
                        girlImage.GetComponent<Image>().sprite = girlSprites[1];
                    }
                    else
                    {
                        dadImage.GetComponent<Image>().sprite = dadSprites[1];
                        girlImage.GetComponent<Image>().sprite = girlSprites[0];
                    }
                    int textLength = textBox.text.Length / 10;
                    if(textLength < 2)
                        textLength = 2;
                    if(j == 0)
                        textLength = 4;
                    yield return new WaitForSeconds(secondsToSkip * textLength);
                }
            }
        }
        skipButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator WaitAtPieces()
    {
        for(int i = 0; i < piecesRelated.Count; i++) 
        {
            if(piecesToCompare == piecesRelated[i].GetNumberOfPieces())
            { 
                skipButton.gameObject.SetActive(true);
                for(int j = 0; j < piecesRelated[i].GetConversationInput().Count; j++)
                {
                    textBox.text = piecesRelated[i].GetConversationInput()[j].dialogueLine;
                    if (piecesRelated[i].GetConversationInput()[j].convParticipant.ToString() == "Dad")
                    {
                        dadImage.GetComponent<Image>().sprite = dadSprites[0];
                        girlImage.GetComponent<Image>().sprite = girlSprites[1];
                    }
                    else
                    {
                        dadImage.GetComponent<Image>().sprite = dadSprites[1];
                        girlImage.GetComponent<Image>().sprite = girlSprites[0];
                    }
                    int textLength = textBox.text.Length / 10;
                    if(textLength < 2)
                        textLength = 2;
                    if(j == 0)
                        textLength = 4;
                    yield return new WaitForSeconds(secondsToSkip * textLength);
                }
            }
        }
        skipButton.gameObject.SetActive(false);
        gameObject.SetActive(false); 
    }

    public void SkipConversation() 
    {
        if(Input.anyKey)
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
}