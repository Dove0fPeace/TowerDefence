using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        #region UnityEvents
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }
            else if (m_Mode == TurretMode.Auto)
            {
                Fire(true);
            }
        }
        #endregion

        #region PublicAPI

        public void Fire(bool IsPlayerShip)
        {
            if (m_TurretProperties == null) return;
            if (CanFire == false) return;

            //if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false
            //    | m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false)
            //    return;

            Projectile projectile = Instantiate(m_TurretProperties.ProgectilePrefab).GetComponent<Projectile>();
            projectile.IsPlayerProjectile = IsPlayerShip;
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            m_RefireTimer = m_TurretProperties.RateOfFire;

            //SFX

        }

        public void AssignLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;
            m_TurretProperties = props;
        }

        #endregion

    }
}