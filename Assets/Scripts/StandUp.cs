using UnityEngine;

namespace TowerDefence
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D m_RigidBody;
        private SpriteRenderer m_SpriteRenderer;
        private void Start()
        {
            m_RigidBody = transform.root.GetComponent<Rigidbody2D>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void LateUpdate()
        {
            transform.up = Vector2.up;
            var xMotion = m_RigidBody.velocity.x;
            if (xMotion > 0.01f) m_SpriteRenderer.flipX = false;
            else if (xMotion < 0.01f) m_SpriteRenderer.flipX = true;
        }
    }
}