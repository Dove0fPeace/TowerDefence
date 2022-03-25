using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefence
{
    public class TD_Player : Player
    {
        public static new TD_Player Instance
        { get
            {
                return Player.Instance as TD_Player;
            }
        }

        public static event Action<int> OnGoldUpdate;
        public static event Action<int> OnLifeUpdate;

        [Header("TD")]
        [SerializeField] private int m_Gold;

        public int CurrentGold => m_Gold;

        private void Start()
        {
            OnGoldUpdate(CurrentGold);
            OnLifeUpdate(NumLives);
        }

        public void AddGold(int gold)
        {
            m_Gold += gold;
            OnGoldUpdate(CurrentGold);
        }

        public void ChsngeLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }
    }
}