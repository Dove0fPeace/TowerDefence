using System;
using UnityEngine;

namespace _Imported
{
    public enum DamageType
    {
        Physic,
        Magic
    }
    
    public class Projectile : Entity
    {
        [SerializeField] protected float m_Velocity;

        [SerializeField] protected float m_Lifetime;

        [SerializeField] private int m_Damage;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        [SerializeField] private UpgradeAsset m_ProjectileUpgrade;

        [SerializeField] private DamageType m_DamageType;

        private float speedBonus;
        private int damageBonus;
        [SerializeField] private int m_DamageBonus;

        //При создании проджектайла ему передается булевое значение выпущен ли этот проджектайл игроком.
        //Если да, то очки засчитаются даже если в полете корабль игрока уничтожат, потому что это значение передается при создании проджектайла, а не идет проверка при столкновении.
        public bool IsPlayerProjectile;
        public bool Static = false;
        private Enemy _target;

        public TargetLayer Layer;


        protected float m_Timer;

        private void Start()
        {
            var upgradeLevel = Upgrades.GetUpgradeLevel(m_ProjectileUpgrade);
            
            speedBonus = ((float)upgradeLevel * 10) / 100;
            damageBonus = upgradeLevel * m_DamageBonus;

            m_Damage += damageBonus;

        }

        protected virtual void FixedUpdate()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer > m_Lifetime)
                OnProjectileLifeEnd();
            if(Static) return;
            if (_target == null)
            {
                OnProjectileLifeEnd();
                return;
            }
            
            float stepLenght = Time.deltaTime * (m_Velocity + (m_Velocity * speedBonus));
            transform.up = (_target.ReturnTarget().position - transform.position).normalized;
            Vector2 step = transform.up * stepLenght;
            
            transform.position += new Vector3(step.x, step.y, 0);

        }

        private void OnProjectileLifeEnd()
        {
            if (m_ImpactEffectPrefab != null)
            {
                ImpactEffect impact = Instantiate(m_ImpactEffectPrefab).GetComponent<ImpactEffect>();
                impact.transform.position = transform.position;
                impact.transform.up = transform.up;
            }
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.root.GetComponent<Enemy>().Type == Layer || Layer == TargetLayer.Both)
            {
                TD_PatrolController dest = collision.transform.root.GetComponent<TD_PatrolController>();
                print(dest.name);
                if (dest != null)
                {
                    dest.ApplyDamage(m_Damage, IsPlayerProjectile, m_DamageType);
                    /*
                    if (IsPlayerProjectile && dest.TeamID != Destructible.TeamIDNeutral)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);
                    }
                    */
                }
                OnProjectileLifeEnd();
            }
        }

        public void SetEnemy(Enemy target)
        {
            _target = target;
        }

    }
}