using System;
using System.Collections;
using _Imported;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Abilities : SingletonBase<Abilities>
{
    public interface IUsable { void Use();
        void CheckUpgrade();
        void CheckCost(int mana);
    }
    [Serializable]
    public class FireAbility : IUsable
    {
        [SerializeField] private float m_Cooldown = 5f;
        [SerializeField] private int m_Damage = 10;
        [SerializeField] private int m_Cost;

        [SerializeField] private int m_DamageUpgradePerLVL;

        [SerializeField] private Color m_TargetColor;
        [SerializeField] private UpgradeAsset m_Upgrade;

        [SerializeField] private TMP_Text m_CostText;
        private bool NeedUpgrade;

        private bool _cooldown;

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
                TD_Player.Instance.ChangeMana(m_Cost);
            });
        }
        
        private IEnumerator FireAbilityButton()
        {
            Instance.m_FireAbilityButton.interactable = false;
            _cooldown = true;
            CheckCost(TD_Player.Instance.Mana);
            yield return new WaitForSeconds(m_Cooldown);
            _cooldown = false;
            CheckCost(TD_Player.Instance.Mana);
        }

        public void CheckUpgrade()
        {
            if (Upgrades.GetUpgradeLevel(m_Upgrade) == 0)
            {
                Instance.m_FireAbilityButton.interactable = false;
                m_CostText.text = "X";
                NeedUpgrade = true;
            }
            else
            {
                Instance.m_FireAbilityButton.interactable = true;
                m_Damage += m_DamageUpgradePerLVL * Upgrades.GetUpgradeLevel(m_Upgrade);
                NeedUpgrade = false;
            }

            m_CostText.text = m_Cost.ToString();
        }

        public void CheckCost(int mana)
        {
            if(NeedUpgrade) return;
            print($"Fire cost {m_Cost}, Player mana {mana}");
            /*
            if (mana >= m_Cost != Instance.m_FireAbilityButton.interactable)
            {
                if (!_cooldown)
                {
                    Instance.m_FireAbilityButton.interactable = !Instance.m_FireAbilityButton.interactable;
                    m_CostText.color = Instance.m_FireAbilityButton.interactable ? Color.white : Color.red;
                }
            }
            */

            if (mana >= m_Cost && _cooldown == false)
            {
                Instance.m_FireAbilityButton.interactable = true;
                m_CostText.color = Color.white;
            }

            if (mana < m_Cost)
            {
                Instance.m_FireAbilityButton.interactable = false;
                m_CostText.color = Color.red;
            }
        }
    }


    [Serializable]
    public class TimeAbility : IUsable
    {
        [SerializeField] private int m_Cost = 10;
        [SerializeField] private float m_Cooldown = 10f;
        
        [SerializeField] private float m_Duration = 5f;
        [SerializeField] private float m_Power = 0.4f;

        [SerializeField] private float m_DurationUpgradePerLVL;
        
        [SerializeField] private UpgradeAsset m_Upgrade;

        [SerializeField] private TMP_Text m_CostText;

        private bool NeedUpgrade;

        private bool _cooldown;
        
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
            TD_Player.Instance.ChangeMana(m_Cost);
        }

        private IEnumerator TimeAbilityButton()
        {
            Instance.m_TimeAbilityButton.interactable = false;
            _cooldown = true;
            CheckCost(TD_Player.Instance.Mana);
            yield return new WaitForSeconds(m_Cooldown);
            _cooldown = false;
            CheckCost(TD_Player.Instance.Mana);
        }
        public void CheckUpgrade()
        {
            if (Upgrades.GetUpgradeLevel(m_Upgrade) == 0)
            {
                Instance.m_TimeAbilityButton.interactable = false;
                m_CostText.text = "X";
                NeedUpgrade = true;
            }
            else
            {
                Instance.m_TimeAbilityButton.interactable = true;
                m_Duration += m_DurationUpgradePerLVL * Upgrades.GetUpgradeLevel(m_Upgrade);
                NeedUpgrade = false;
            }

            m_CostText.text = m_Cost.ToString();
        }

        public void CheckCost(int mana)
        {
            if(NeedUpgrade) return;
            if (mana >= m_Cost && _cooldown == false)
            {
                Instance.m_TimeAbilityButton.interactable = true;
                m_CostText.color = Color.white;
            }

            if (mana < m_Cost)
            {
                Instance.m_TimeAbilityButton.interactable = false;
                m_CostText.color = Color.red;
            }
        }

    }

    [SerializeField] private FireAbility m_FireAbility;
    [SerializeField] private Image m_TargetingCircle;
    [SerializeField] private Button m_FireAbilityButton;
    public void UseFireAbility() => m_FireAbility.Use();
    
    [SerializeField] private TimeAbility m_TimeAbility;
    [SerializeField] private Button m_TimeAbilityButton;
    public void UseTimeAbility() => m_TimeAbility.Use();

    private void Start()
    {
        m_TimeAbility.CheckUpgrade();
        TD_Player.ManaUpdateSubscribe(m_TimeAbility.CheckCost);
        
        m_FireAbility.CheckUpgrade();
        TD_Player.ManaUpdateSubscribe(m_FireAbility.CheckCost);
    }

    private void OnDestroy()
    {
        TD_Player.ManaUpdateUnSubscribe(m_FireAbility.CheckCost);
        TD_Player.ManaUpdateUnSubscribe(m_TimeAbility.CheckCost);
    }
}

