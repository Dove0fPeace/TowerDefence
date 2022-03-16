using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] protected float m_Velocity;

        [SerializeField] protected float m_Lifetime;

        [SerializeField] private int m_Damage;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        //При создании проджектайла ему передается булевое значение выпущен ли этот проджектайл игроком.
        //Если да, то очки засчитаются даже если в полете корабль игрока уничтожат, потому что это значение передается при создании проджектайла, а не идет проверка при столкновении.
        public bool IsPlayerProjectile;


        protected float m_Timer;

        protected virtual void FixedUpdate()
        {
            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLenght;

            m_Timer += Time.deltaTime;
            if (m_Timer > m_Lifetime)
                OnProjectileLifeEnd();

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
            Destructible dest = collision.transform.root.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.ApplyDamage(m_Damage, IsPlayerProjectile);
                
                if (IsPlayerProjectile && dest.TeamID != Destructible.TeamIDNeutral)
                {
                    Player.Instance.AddScore(dest.ScoreValue);
                }
            }
            OnProjectileLifeEnd();
        }
    }
}