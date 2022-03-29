using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private TurretProperties m_TurretProperties;

    private float m_RefireTimer;

    public bool CanFire => m_RefireTimer <= 0;

    private SpaceShip m_Ship;
    public Enemy Target { get; set; }

    private TargetLayer _targetLayer;

    #region UnityEvents
    private void Start()
    {
        m_Ship = transform.root.GetComponent<SpaceShip>();
        _targetLayer = GetComponentInParent<Tower>().Type;
    }

    private void Update()
    {
        if (m_RefireTimer > 0)
        {
            m_RefireTimer -= Time.deltaTime;
        }
        else if (m_Mode == TurretMode.Auto)
        {
            Fire(Target);
        }
    }
    #endregion

    #region PublicAPI

    public void Fire(Enemy Target)
    {
        if (m_TurretProperties == null) return;
        if (CanFire == false) return;

        //if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false
        //    | m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false)
        //    return;

        Projectile projectile = Instantiate(m_TurretProperties.ProgectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        if (m_Mode == TurretMode.Auto)
        {
            projectile.Static = true;
        }
        else
        {
            projectile.transform.up = transform.up;
            projectile.SetEnemy(Target);
            projectile.Layer = _targetLayer;

        }

        m_RefireTimer = m_TurretProperties.RateOfFire;

        //SFX

    }

    public void AssignLoadout(TurretProperties props)
    {
        if (props is not null && m_Mode != props.Mode)
        {
            m_Mode = props.Mode;
        }

        m_RefireTimer = 0;
        m_TurretProperties = props;
    }

    #endregion

}