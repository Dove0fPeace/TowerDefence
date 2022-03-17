using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefence
{

    public class TD_PatrolController : AIController
    {
        private Path m_Path;
        private int m_PathIndex;
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
                Destroy(gameObject);    
            }
        }
    }
}