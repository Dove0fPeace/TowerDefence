using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] protected float m_Lifetime;

        private float m_Timer;

        void Update()
        {
            if (m_Timer < m_Lifetime)
                m_Timer += Time.deltaTime;
            else
                Destroy(gameObject);
        }
    }
}