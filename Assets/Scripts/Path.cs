using _Imported;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private CircleArea m_StartArea;
    public CircleArea StartArea
    {
        get { return m_StartArea; }
    }
    
    [SerializeField] private Waypoint[] m_Waypoints;

    public int Lenght { get => m_Waypoints.Length; }
    public Waypoint this[int i] { get => m_Waypoints[i]; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        foreach (var point in m_Waypoints)
        {
            Gizmos.DrawSphere(point.transform.position, 0.3f);
        }
    }
}