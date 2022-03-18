using UnityEngine;

namespace TowerDefence
{
    public class Enemy : MonoBehaviour
    {
        public void Use(EnemyAsset asset)
        {
            var spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            spriteRenderer.color = asset.color;
        }
    }
}