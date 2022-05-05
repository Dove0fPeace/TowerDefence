using UnityEngine;

public class UpgradesMenuOpener : MonoBehaviour
{
    [SerializeField] private GameObject m_MenuPanel;

    private bool menuIsActive;

    private void Start()
    {
        m_MenuPanel.SetActive(false);
        menuIsActive = false;
    }

    public void ChangeMenuActiveState()
    {
        menuIsActive = !menuIsActive;
        m_MenuPanel.SetActive(menuIsActive);
    }
}
