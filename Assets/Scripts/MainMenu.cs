using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_LoadGameButton;
    [SerializeField] private GameObject m_ConfirmNewGamePanel;

    private void Start()
    {
        m_LoadGameButton.interactable = FileHandler.HasFile(MapCompletion.filename);
    }

    public void TryNewGame()
    {
        if (FileHandler.HasFile(MapCompletion.filename))
        {
            ShowConfirmNewGamePannel();
        }
        else
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        FileHandler.Reset(MapCompletion.filename);
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void ShowConfirmNewGamePannel()
    {
        m_ConfirmNewGamePanel.SetActive(true);
    }
    
}
