using _Imported;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float m_Radius;

    private Turret[] m_Turrets;
    private Destructible m_Target = null;

    private void Awake()
    {
        m_Turrets = GetComponentsInChildren<Turret>();
    }

    private void Update()
    {
        if (m_Target)
        {
            Vector2 targetVector = m_Target.PositionPrediction() - transform.position;

            if (targetVector.magnitude <= m_Radius)
            {
                foreach (var turret in m_Turrets)
                {
                    turret.transform.up = targetVector;
                    turret.Fire(true);
                }
            }
            else
            {
                m_Target = null;
            }
        }
        else
        {
            var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
            if (enter)
            {
                m_Target = enter.transform.root.GetComponent<Destructible>();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }

    public void SetTurret(TurretProperties props)
    {
        foreach (var turret in m_Turrets)
        {
            turret.AssignLoadout(props);
        }
    }
}