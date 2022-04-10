using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Imported;

public class EnemyWave : MonoBehaviour
{
    public static Action<float> OnWavePrepare;
    
    [Serializable]
    private class Squad
    {
        public TD_PatrolController EnemyPrefab;
        public int Count;
    }

    [Serializable]
    private class PathGroup
    {
        public Squad[] Squads;
    }

    [SerializeField] private PathGroup[] m_Groups;

    [SerializeField] private EnemyWave m_NextWave;
    
    [SerializeField] private float m_PrepareTime = 10f;

    public float GetRemainingTime()
    {
        return m_PrepareTime - Time.time;
    }

    private event Action OnWaveReady;
    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        if (Time.time >= m_PrepareTime)
        {
            OnWaveReady?.Invoke();
            enabled = false;
        }
    }

    public void Prepare(Action spawnEnemies)
    {
        OnWavePrepare?.Invoke(m_PrepareTime);
        m_PrepareTime += Time.time;
        enabled = true;
        OnWaveReady += spawnEnemies;
    }

    public EnemyWave PrepareNext(Action spawnEnemies)
    {
        OnWaveReady -= spawnEnemies;
        if(m_NextWave) m_NextWave.Prepare(spawnEnemies);
        return m_NextWave;
    }

    public IEnumerable<(TD_PatrolController EnemyPrefab, int count, int pathIndex)> EnumerateSquads()
    {
        for (int i = 0; i < m_Groups.Length; i++)
        {
            foreach (var squad in m_Groups[i].Squads)
            {
                yield return (squad.EnemyPrefab, squad.Count, i);
            }
        }
    }
}
