using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_Damage = 1;
        [SerializeField] private int m_GoldCost = 5;
        public void Use(EnemyAsset asset)
        {
            var spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            spriteRenderer.color = asset.color;
        }

        public void DamagePlayer()
        {
            TD_Player.Instance.ChsngeLife(m_Damage);
        }

        public void GivePlayerGold()
        {
            TD_Player.Instance.AddGold(m_GoldCost);
        }
    }
}