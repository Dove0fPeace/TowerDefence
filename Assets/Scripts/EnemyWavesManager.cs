using System;
using _Imported;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWavesManager : MonoBehaviour
{
    [SerializeField] private Path[] m_Paths;
    [SerializeField] private EnemyWave m_CurrentWave;
    
    private int _activeEnemyCount = 0;

    public static event Action OnAllWavesDead;
    public static event Action OnCurrentWavesDead;
    public static event Action<TD_PatrolController> OnEnemySpawn; 

    private void Start()
    {
        m_CurrentWave.Prepare(SpawnEnemies);
    }

    private void RecordEnemyDead()
    {
        if (--_activeEnemyCount == 0)
        {
            OnCurrentWavesDead?.Invoke();
            ForceNextWave();
        }
        
    }

    private void SpawnEnemies()
    {
        foreach ((TD_PatrolController EnemyPrefab, int count, int pathIndex) in m_CurrentWave.EnumerateSquads())
        {
            if (pathIndex < m_Paths.Length)
            {
                for (int i = 0; i < count; i++)
                {
                    GameObject e = Instantiate(EnemyPrefab.gameObject, m_Paths[pathIndex].StartArea.GetRandomInsideZone(), Quaternion.identity);
                    var enemy = e.GetComponent<TD_PatrolController>();
                    enemy.SetPath(m_Paths[pathIndex]);
                    enemy.OnEnd += RecordEnemyDead;
                    _activeEnemyCount += 1;
                    OnEnemySpawn?.Invoke(enemy);
                }
            }
        }
        
        m_CurrentWave = m_CurrentWave.PrepareNext(SpawnEnemies);
        
    }

    public void ForceNextWave()
    {
        if (m_CurrentWave)
        {
            TD_Player.Instance.ChangeGold((int) m_CurrentWave.GetRemainingTime());
            SpawnEnemies();
        }
        else if(_activeEnemyCount == 0)
        {
            OnAllWavesDead?.Invoke();
        }
    }
}

