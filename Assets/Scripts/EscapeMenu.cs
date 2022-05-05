using UnityEngine.SceneManagement;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_EscapePanel;

    private void Start()
    {
        m_EscapePanel.SetActive(false);
    }

    public void ShowEscapePanel()
    {
        LevelActivityChanger.Instance.ChangeLevelActivity(false);
        m_EscapePanel.SetActive(true);
    }

    public void HideEscapePanel()
    {
        m_EscapePanel.SetActive(false);
        LevelActivityChanger.Instance.ChangeLevelActivity(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
