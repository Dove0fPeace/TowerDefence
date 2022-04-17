using System;
using System.Collections;
using _Imported;
using UnityEngine;

public class Abilities : SingletonBase<Abilities>
{
    public interface IUsable { void Use(); }
    [Serializable]
    public class FireAbility : IUsable
    {
        [SerializeField] private float m_Cooldown = 5f;
        [SerializeField] private int m_Damage = 10;
        public void Use()
        {
            
        }
    }
    [Serializable]
    public class TimeAbility : IUsable
    {
        [SerializeField] private int m_Cost = 10;
        
        [SerializeField] private float m_Duration = 5f;
        [SerializeField] private float m_Power = 0.4f;
        
        public void Use()
        {
            void Slow(Destructible enemy)
            {
                enemy.GetComponent<TD_PatrolController>().SetSlowed(true, m_Power);
            }

            IEnumerator Restore()
            {
                yield return new WaitForSeconds(m_Duration);
                foreach (var enemy in Destructible.Enemies)
                {
                    print(enemy.name);
                    enemy.GetComponent<TD_PatrolController>().SetSlowed(false, m_Power);
                }
                EnemyWavesManager.OnEnemySpawn -= Slow;
                
            }

            if (Destructible.Enemies != null)
            {
                foreach (var enemy in Destructible.Enemies)
                {
                    Slow(enemy);
                }
            }

            EnemyWavesManager.OnEnemySpawn += Slow;

            Instance.StartCoroutine(Restore());

        }
    }

    [SerializeField] private FireAbility m_FireAbility;
    public void UseFireAbility() => m_FireAbility.Use();
    
    [SerializeField] private TimeAbility m_TimeAbility;
    public void UseTimeAbility() => m_TimeAbility.Use();
    
    
}

