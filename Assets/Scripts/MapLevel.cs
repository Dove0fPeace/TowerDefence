using _Imported;
using UnityEngine;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour
{
    [SerializeField] private RectTransform m_ResultPanel;

    [SerializeField] private Image[] m_ResultImage;
    private Episode m_Episode;
    public void LoadLevel()
    {
        LevelSequenceController.Instance.StartEpisode(m_Episode);
    }

    public void SetLevelData(Episode episode, int score)
    {
        m_Episode = episode;
        m_ResultPanel.gameObject.SetActive(score > 0);

        for (int i = 0; i < score; i++)
        {
            m_ResultImage[i].color = Color.white;
        }
    }
}
