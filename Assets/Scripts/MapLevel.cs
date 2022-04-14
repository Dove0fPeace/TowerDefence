using _Imported;
using UnityEngine;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour
{
    [SerializeField] private RectTransform m_ResultPanel;

    [SerializeField] private Image[] m_ResultImage;
    [SerializeField] private Episode m_Episode;
    public Episode Episode => m_Episode;

    public bool IsComplete => gameObject.activeSelf && m_ResultPanel.gameObject.activeSelf;

    public void LoadLevel()
    {
        LevelSequenceController.Instance.StartEpisode(m_Episode);
    }

    public int Initialise()
    {
        var score = MapCompletion.Instance.GetEpisodeScore(m_Episode);
        m_ResultPanel.gameObject.SetActive(score > 0);

        for (int i = 0; i < score; i++)
        {
            m_ResultImage[i].color = Color.white;
        }

        return score;
    }
}
