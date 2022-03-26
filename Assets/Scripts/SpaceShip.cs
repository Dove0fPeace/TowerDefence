using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : Destructible
{
    [Header("SpaceShip")]
    [SerializeField] private float m_Mass;
    [SerializeField] private float m_Trust;
    [SerializeField] private float m_Mobility;
    [SerializeField] private float m_MaxLinearVelocity;
    public float MaxLinearVelocity => m_MaxLinearVelocity;
    [SerializeField] private float m_MaxAngularVelocity;
    public float MaxAngularVelocity => m_MaxAngularVelocity;

    [Space(10)]
    [SerializeField] private int m_MaxEnergy;
    public int MaxEnergy => m_MaxEnergy;
    [SerializeField] private int m_MaxAmmo;
    public int MaxAmmo => m_MaxAmmo;
    [SerializeField] private int m_EnergyRegenPerSecond;

    private float m_PrimaryEnergy;
    public float Energy => m_PrimaryEnergy;
    private int m_SecondaryAmmo;
    public int Ammo => m_SecondaryAmmo;

    [Space(10)]
    [SerializeField] private TrailController trail;
    [SerializeField] private Turret[] m_Turrets;

    [Space(10)]
    [SerializeField] private GameObject m_Bubble;

    private Rigidbody2D m_Rigid;

    [SerializeField] private bool IsSpeedBoosted;
    private float SpeedUpTimer;
    private float BaseThrust;
    private float BaseMobility;

    [Header("bool Is Players Ship")]
    [SerializeField] private bool m_IsPlayerShip;
    public bool IsPlayersShip => m_IsPlayerShip;

    [SerializeField] private Sprite m_ShipPreview;
    public Sprite ShipPreview => m_ShipPreview;

    [Space(10)]
    [SerializeField] private bool m_DamageSlowDownTheShip;
    private bool m_Slowed;
    [SerializeField] private float m_SlowTime;
    [SerializeField] private float m_PowerOfSlowDown;

    #region Public API
        
    public float TrustControl { get; set; }
    public float TorqueControl { get; set; }
    //TODO: Change method. Using in AIController
    public void Fire(TurretMode mode)
    {
        return;
    }
    /*
        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if(m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }

            return false;
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties props)
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }
        */
    public void SpeedBust(float velocity, float time)
    {
        IsSpeedBoosted = true;
        m_Trust += velocity;
        m_Mobility += velocity;
        SpeedUpTimer = time;
    }

    public void SetIndestructible(float time)
    {
        timer = time;
        m_Indestructible = true;
        m_Bubble.SetActive(true);
    }
    public override void ApplyDamage(int damage, bool playersProjectile)
    {
        if (m_DamageSlowDownTheShip)
        {
            m_Slowed = true;
            m_Trust -= m_PowerOfSlowDown;
        }
        else
        {
            base.ApplyDamage(damage, playersProjectile);
        }
    }

    public void SetDamageMode(bool mode)
    {
        m_DamageSlowDownTheShip = mode;
    }
    #endregion

    #region Unity Events
    protected override void Start()
    {
        base.Start();
        BaseThrust = m_Trust;
        BaseMobility = m_Mobility;

        m_Rigid = GetComponent<Rigidbody2D>();
        m_Rigid.mass = m_Mass;

        m_Rigid.inertia = 1;

        //InitOffensive();
    }

    private void Update()
    {
        /*
            if (TrustControl != 0)
                trail.TrailOn();
            else
                trail.TrailOff();

            */
        if(IsSpeedBoosted)
        {
            SpeedUpTimer -= Time.deltaTime;
            if(SpeedUpTimer <= 0)
            {
                IsSpeedBoosted = false;
                m_Trust = BaseThrust;
                m_Mobility = BaseMobility;
            }
        }

        if (m_Indestructible == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                m_Bubble.SetActive(false);
                m_Indestructible = false;
            }
        }

        if(m_Slowed == true)
        {
            m_SlowTime -= Time.deltaTime;
            if(m_SlowTime <= 0)
            {
                m_Slowed = false;
                m_Trust += m_PowerOfSlowDown;
            }
        }

    }

    private void FixedUpdate()
    {
        UpdateRigidbody();

        //EnergyRegen();

    }
    #endregion

    /// <summary>
    /// Method of adding forces to move a ship
    /// </summary>
    /// 
        
    private void UpdateRigidbody()
    {
        if (TrustControl > 0.1)
        {
            m_Rigid.AddForce(TrustControl * m_Trust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        m_Rigid.AddForce(-m_Rigid.velocity * (m_Trust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
            

        m_Rigid.AddTorque(-TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);
        m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
    }
    /*
        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void EnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }
        */
    protected override void OnDeath(bool playersProjectile)
    {
        //if (playersProjectile == true && m_IsPlayerShip == false)
        //    Player.Instance.AddKill();
        base.OnDeath(playersProjectile);
    }
}