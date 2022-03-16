using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }

    public class LevelController : SingletonBase<LevelController>
    {
        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        [SerializeField] private UnityEvent m_EventLevelCompleted;

        private ILevelCondition[] m_Conditions;

        private bool m_IsLevelCompleted;

        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        [Header("Score multiplier")]
        [SerializeField] private float m_GoldTime;
        [SerializeField] private float m_SilverTime;
        [SerializeField] private float m_BronzeTime;

        public float m_ScoreMultiiplier { get; private set; }

        private void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
            m_LevelTime = 0f;
        }

        private void Update()
        {
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0) return;

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            {
                if (v.IsCompleted)
                    numCompleted++;
            }

            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;
                SetScoreMultiplier(m_LevelTime);
                m_EventLevelCompleted?.Invoke();

                LevelSequenceController.Instance.FinishCurrentLevel(m_IsLevelCompleted);
            }
        }

        private void SetScoreMultiplier(float time)
        {
            if (time <= m_GoldTime)
                m_ScoreMultiiplier = 2.0f;
            if (time > m_GoldTime && time <= m_SilverTime)
                m_ScoreMultiiplier = 1.5f;
            if (time > m_SilverTime && time <= m_BronzeTime)
                m_ScoreMultiiplier = 1.1f;
            if (time > m_BronzeTime)
                m_ScoreMultiiplier = 1.0f;

        }
    }
}