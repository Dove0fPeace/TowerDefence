using System;
using System.Collections;
using _Imported;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : SingletonBase<Abilities>
{
    public interface IUsable { void Use(); }
    [Serializable]
    public class FireAbility : IUsable
    {
        [SerializeField] private float m_Cooldown = 5f;
        [SerializeField] private int m_Damage = 10;

        [SerializeField] private Color m_TargetColor;
        public void Use()
        {
            ClickProtection.Instance.Activate((Vector2 v) =>
            {
                Vector3 position = v;
                if (Camera.main == null) return;
                position.z = -Camera.main.transform.position.z;
                position =  Camera.main.ScreenToWorldPoint(position);
                foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                {
                    if (collider.transform.root.TryGetComponent<TD_PatrolController>(out var enemy))
                    {
                        enemy.ApplyDamage(m_Damage, true, DamageType.Magic);
                    }
                }

                Instance.StartCoroutine(FireAbilityButton());
            });
        }
        
        private IEnumerator FireAbilityButton()
        {
            Instance.m_FireAbilityButton.interactable = false;
            yield return new WaitForSeconds(m_Cooldown);
            Instance.m_FireAbilityButton.interactable = true;
        }
    }


    [Serializable]
    public class TimeAbility : IUsable
    {
        [SerializeField] private int m_Cost = 10;
        [SerializeField] private float m_Cooldown = 10f;
        
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
            Instance.StartCoroutine(TimeAbilityButton());
        }

        private IEnumerator TimeAbilityButton()
        {
            Instance.m_TimeAbilityButton.interactable = false;
            yield return new WaitForSeconds(m_Cooldown);
            Instance.m_TimeAbilityButton.interactable = true;
        }
    }

    [SerializeField] private FireAbility m_FireAbility;
    [SerializeField] private Image m_TargetingCircle;
    [SerializeField] private Button m_FireAbilityButton;
    public void UseFireAbility() => m_FireAbility.Use();
    
    [SerializeField] private TimeAbility m_TimeAbility;
    [SerializeField] private Button m_TimeAbilityButton;
    public void UseTimeAbility() => m_TimeAbility.Use();
    
    
}

