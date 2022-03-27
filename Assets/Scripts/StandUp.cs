using UnityEngine;

public class StandUp : MonoBehaviour
{
    private TD_PatrolController _controller;
    private SpriteRenderer m_SpriteRenderer;
    private void Start()
    {
        _controller = GetComponentInParent<TD_PatrolController>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        transform.up = Vector2.up;
        var xMotion = _controller.MovePosition.x;
        if (xMotion > 0.01f) m_SpriteRenderer.flipX = false;
        else if (xMotion < 0.01f) m_SpriteRenderer.flipX = true;
    }
}