using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextWaveGUI : MonoBehaviour
{
    private EnemyWavesManager m_WaveManager;
    [SerializeField] private TMP_Text m_BonusAmount;
    private float m_TimeToNextWave;

    [SerializeField] private Image m_Mask;
    private float _originalSize;
    private float _currentSize;
    [SerializeField] private float m_SmoothingBarMovement = 5.0f;

    private float _allEnemies;
    private float _killedEnemies;

    private float actualSize;

    private List<TD_PatrolController> SubscribeEnemies;
    

    private void Start()
    {
        SubscribeEnemies = new List<TD_PatrolController>();
        
        _originalSize = m_Mask.rectTransform.rect.width;
        m_Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        _currentSize = 0;
        
        m_WaveManager = FindObjectOfType<EnemyWavesManager>();
        EnemyWave.OnWavePrepare += (float time) =>
        {
            m_TimeToNextWave = time;
        };

        EnemyWavesManager.OnEnemySpawn += SubscribeOnNewEnemy;
        EnemyWavesManager.OnCurrentWavesDead += OnKillAllEnemies;
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
        
        actualSize  = _killedEnemies / _allEnemies;
        if (Math.Abs(actualSize - _currentSize) > 0.01)
        {
            float value = Mathf.Lerp(_currentSize, actualSize, m_SmoothingBarMovement * Time.deltaTime);
            _currentSize = value;
            m_Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _originalSize * value);
        }
        
    }

    //TODO: Fix progress bar on new wave
    private void OnKillAllEnemies()
    {
        _allEnemies = 0;
        _killedEnemies = 0;
        actualSize = 0;
    }
    private void SubscribeOnNewEnemy(TD_PatrolController enemy)
    {
        _allEnemies += 1;
        enemy.EventOnDeath.AddListener(AddEnemyKill);
        SubscribeEnemies.Add(enemy);
    }

    private void AddEnemyKill()
    {
        _killedEnemies += 1;
    }

    private void OnDestroy()
    {
        foreach (var enemy in SubscribeEnemies)
        {
            enemy.EventOnDeath.RemoveListener(AddEnemyKill);
        }

        EnemyWavesManager.OnEnemySpawn -= SubscribeOnNewEnemy;
        EnemyWavesManager.OnCurrentWavesDead -= OnKillAllEnemies;

        SubscribeEnemies.Clear();
    }
}
