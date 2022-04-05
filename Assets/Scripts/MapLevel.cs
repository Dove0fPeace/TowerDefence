using _Imported;
using UnityEngine;
using TMPro;

public class MapLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    private Episode m_Episode;
    public void LoadLevel()
    {
        LevelSequenceController.Instance.StartEpisode(m_Episode);
    }

    public void SetLevelData(Episode episode, int score)
    {
        m_Episode = episode;
        m_Text.text = $"{score}/3";
    }
}
