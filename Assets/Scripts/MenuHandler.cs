using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button continueButton;
    [SerializeField] SaveDataSO saveGame;
    bool isGameState = false;
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGameState)
            {
                pauseMenu.gameObject.SetActive(true);
            }
        }    
    }

    public void ContinueButton()
    {
        if(saveGame.CanLoad())
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    public void IsGameState()
    {
        isGameState = true;
    }

    public void NotGameState()
    {
        isGameState = false;
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
