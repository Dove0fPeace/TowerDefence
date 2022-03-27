using System;
using _Imported;
using UnityEngine;
using UnityEngine.Events;

public class TD_PatrolController : Destructible
{
    private Path m_Path;
    private int m_PathIndex;
    [SerializeField] private UnityEvent OnEndPath;

    [SerializeField] private float m_Speed = 1.0f;
    [SerializeField] private float m_MinimumSpeed;
    private float _basicSpeed;
    
    [SerializeField] private Waypoint m_Waypoint;
    private Vector3 _movePosition;
    public Vector3 MovePosition => _movePosition;

    protected override void Start()
    {
        base.Start();
        _basicSpeed = m_Speed;
    }

    private void FixedUpdate()
    {
        transform.Translate(_movePosition * (Time.deltaTime * m_Speed), Space.World);
    }

    public void SetPath(Path newPath)
    {
        m_Path = newPath;
        m_PathIndex = 0;
        SetWaypoint(m_Path[m_PathIndex]);
    }

    public void GetNewPoint()
    {
        m_PathIndex += 1;
        if(m_Path.Lenght > m_PathIndex)
        {
            SetWaypoint(m_Path[m_PathIndex]);
        }
        else
        {
            OnEndPath.Invoke();
            Destroy(gameObject);    
        }
    }

    public void SetSlowed(bool slow, float slowPower)
    {
        if (slow == true)
        {
            m_Speed -= slowPower;
            if (m_Speed < m_MinimumSpeed)
                m_Speed = m_MinimumSpeed;
        }

        else
        {
            m_Speed = _basicSpeed;
        }
    }
    public void SetWaypoint(Waypoint point)
    {
        m_Waypoint = point;
        _movePosition = (m_Waypoint.transform.position - transform.position).normalized;
    }
}