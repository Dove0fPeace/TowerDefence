using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Waypoint m_NextPoint;

        private void OnTriggerEnter2D(Collider2D other)
        {
            AIController AI = other.transform.root.GetComponent<AIController>();
            if(AI != null)
            {
                AI.SetWaypoint(m_NextPoint);
            }
        }
    }
}