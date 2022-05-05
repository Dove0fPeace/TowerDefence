using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Imported
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private TMP_Text m_Kills;
        [SerializeField] private TMP_Text m_Score;
        [SerializeField] private TMP_Text m_Time;

        [SerializeField] private TMP_FontAsset m_WinFont;
        [SerializeField] private TMP_FontAsset m_LoseFont;

        [SerializeField] private Color m_LoseFontColor;
        
        [SerializeField] private TMP_Text m_Result;

        [SerializeField] private Text m_ButtonNextText;

        [SerializeField] private Sound m_WinSound = Sound.PlayerWin;
        [SerializeField] private Sound m_LoseSound = Sound.PlayerLose;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResult, bool success)
        {
            gameObject.SetActive(true);

            m_Success = success;

            switch (success)
            {
                case true:
                    m_Result.font = m_WinFont;
                    m_Result.color =Color.white;
                    m_Result.text ="Win";
                    m_WinSound.Play();
                    break;
                case false:
                    m_Result.font = m_LoseFont;
                    m_Result.color = m_LoseFontColor;
                    m_Result.text ="Lose";
                    m_LoseSound.Play();
                    break;
            }
            /*
            m_ButtonNextText.text = success ? "Next" : "Restart";
            m_Kills.text = "Kills : " + levelResult.numKills.ToString();
            m_Score.text = "Score : " + levelResult.score.ToString();
            m_Time.text = "Time : " + levelResult.time.ToString();*/
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if(m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }
    }
}