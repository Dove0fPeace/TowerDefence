using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_LoadGameButton;

    private void Start()
    {
        m_LoadGameButton.interactable = FileHandler.HasFile(MapCompletion.filename);
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
    
}
