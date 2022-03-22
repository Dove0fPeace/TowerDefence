using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence;

namespace SpaceShooter
{

    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Path m_Path;

        [SerializeField] private Entity[] m_EntityPrefabs;

        [SerializeField] private EnemyAsset[] m_EnemySettings;

        [SerializeField] private CircleArea m_CircleArea;

        [SerializeField] private SpawnMode m_SpawnMode;

        [SerializeField] private int m_NumSpawns;

        [SerializeField] private float m_RespawnTime;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_RespawnTime;
        }

        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntities();

                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                int index = Random.Range(0, m_EntityPrefabs.Length);

                GameObject e = Instantiate(m_EntityPrefabs[index].gameObject);

                e.transform.position = m_CircleArea.GetRandomInsideZone();
                if (e.TryGetComponent<TD_PatrolController>(out var ai))
                {
                    ai.SetPath(m_Path);
                }
                if(e.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.Use(m_EnemySettings[Random.Range(0, m_EnemySettings.Length)]);
                }
            }
        }
    }
}