using System;
using _Imported;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private TargetLayer _type;
    
    [SerializeField] private float m_Radius;

    private Turret[] m_Turrets;
    private Enemy m_Target = null;

    private CircleCollider2D _triggerArea;

    private void Awake()
    {
        _triggerArea = GetComponent<CircleCollider2D>();
        _triggerArea.radius = m_Radius;
        m_Turrets = GetComponentsInChildren<Turret>();
    }

    private void Update()
    {
        if (m_Target)
        {
            Vector2 targetVector = transform.position - m_Target.transform.position;

            if (targetVector.magnitude <= m_Radius + 0.2f)
            {
                foreach (var turret in m_Turrets)
                {
                    if (turret.Mode != TurretMode.Auto)
                    {
                        turret.transform.up = targetVector;
                    }

                    turret.Fire(m_Target);
                }
            }
            else
            {
                m_Target = null;
            }
        }
        /*
        else
        {
            var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
            print($"{name} find target");
            if (enter)
            {
                m_Target = enter.transform.root.GetComponent<Destructible>();
            }
        }
        */
    }
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
    */
    public void SetTurret(TurretProperties props)
    {
        foreach (var turret in m_Turrets)
        {
            turret.AssignLoadout(props);
        }
    }

    public void SetType(TargetLayer type)
    {
        _type = type;
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_Target == null)
        {
            Enemy enemy = other.transform.root.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (enemy.Type == _type || _type == TargetLayer.Both)
                {
                    m_Target = enemy;
                }
            }
        }
    }
    */
    private void OnTriggerStay2D(Collider2D other)
    {
        if (m_Target == null)
        {
            Enemy enemy = other.transform.root.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (enemy.Type == _type || _type == TargetLayer.Both)
                {
                    m_Target = enemy;
                }
            }
        }
    }
}