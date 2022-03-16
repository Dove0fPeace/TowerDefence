using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{

    public class Destructible : Entity
    {
        #region Properties

        [SerializeField] private int m_TeamID;
        public int TeamID => m_TeamID;
        [SerializeField] private bool Enemy;

        [SerializeField] private float m_ScoreValue;
        public float ScoreValue => m_ScoreValue;

        [SerializeField] private float m_PredictionMultiply = 1.3f;

        public const int TeamIDNeutral = 0;
        [SerializeField] protected bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        protected Rigidbody2D _thisRB;

        /// <summary>
        /// Start HP
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public int HP => m_HitPoints;

        /// <summary>
        /// Current HP
        /// </summary>
        private int m_CurrentHP;
        public int CurrentHP => m_CurrentHP;

        protected float timer;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHP = m_HitPoints;
            _thisRB = GetComponent<Rigidbody2D>();
        }


        #endregion

        #region Public API

        public virtual void ApplyDamage(int damage, bool playersProjectile)
        {
            if (m_Indestructible) return;

            m_CurrentHP -= damage;
            if (m_CurrentHP <= 0)
                OnDeath(playersProjectile);
        }

        public Vector3 PositionPrediction()
        {
            Vector3 pos = (Vector2)transform.position + (_thisRB.velocity * m_PredictionMultiply);
            return pos;
        }

        #endregion

        protected virtual void OnDeath(bool playersProjectile)
        {
            m_EventOnDeath?.Invoke();
            Destroy(gameObject);
            
        }

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        private static HashSet<Destructible> m_AllDestructibles;
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        private static HashSet<Destructible> m_Enemies;
        public static IReadOnlyCollection<Destructible> Enemies => m_Enemies;

        protected virtual void OnEnable()
        {
            if(m_AllDestructibles == null)
            {
                m_AllDestructibles = new HashSet<Destructible>();
            }
            if(m_Enemies == null)
            {
                m_Enemies = new HashSet<Destructible>();
            }
            if(Enemy)
            {
                m_Enemies.Add(this);
            }
            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            if(Enemy)
            {
                m_Enemies.Remove(this);
            }
            m_AllDestructibles.Remove(this);
        }
    }
}
