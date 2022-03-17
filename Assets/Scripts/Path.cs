using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    public class Path : MonoBehaviour
    {
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
}
