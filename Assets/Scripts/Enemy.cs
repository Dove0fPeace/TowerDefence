using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private TargetLayer m_Type;

    [SerializeField] private Transform m_Target;
    public TargetLayer Type => m_Type;
    
    [SerializeField] private int m_Damage = 1;
    [SerializeField] private int m_GoldCost = 5;
    public void Use(EnemyAsset asset)
    {
        var spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        spriteRenderer.color = asset.color;
    }

    public void DamagePlayer()
    {
        TD_Player.Instance.ChangeLife(m_Damage);
    }

    public void GivePlayerGold()
    {
        TD_Player.Instance.ChangeGold(m_GoldCost);
    }

    public Transform ReturnTarget()
    {
        return m_Target;
    }
}