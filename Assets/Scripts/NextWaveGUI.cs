using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextWaveGUI : MonoBehaviour
{
    private EnemyWavesManager m_WaveManager;
    [SerializeField] private TMP_Text m_BonusAmount;
    private float m_TimeToNextWave;

    private void Start()
    {
        m_WaveManager = FindObjectOfType<EnemyWavesManager>();
        EnemyWave.OnWavePrepare += (float time) =>
        {
            m_TimeToNextWave = time;
        };
    }

    public void CallWave()
    {
        m_WaveManager.ForceNextWave();
    }

    private void Update()
    {
        var bonus = (int) m_TimeToNextWave;
        if (bonus < 0) bonus = 0;
        m_BonusAmount.text = bonus.ToString();
        m_TimeToNextWave -= Time.deltaTime;
    }
}
