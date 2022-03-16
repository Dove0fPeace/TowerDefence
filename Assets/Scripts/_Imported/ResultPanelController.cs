using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private TMP_Text m_Kills;
        [SerializeField] private TMP_Text m_Score;
        [SerializeField] private TMP_Text m_Time;
        
        [SerializeField] private TMP_Text m_Result;

        [SerializeField] private Text m_ButtonNextText;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResult, bool success)
        {
            gameObject.SetActive(true);

            Time.timeScale = 0;

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";
            m_ButtonNextText.text = success ? "Next" : "Restart";

            m_Kills.text = "Kills : " + levelResult.numKills.ToString();
            m_Score.text = "Score : " + levelResult.score.ToString();
            m_Time.text = "Time : " + levelResult.time.ToString();
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