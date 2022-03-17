using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence;

namespace SpaceShooter
{
    public class Waypoint : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            TD_PatrolController AI = other.transform.root.GetComponent<TD_PatrolController>();
            if(AI != null)
            {
                AI.GetNewPoint();
            }
        }
    }
}