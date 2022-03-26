using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class Player : SingletonBase<Player>
{
    [SerializeField] private int m_NumLives;
    public int NumLives => m_NumLives;
    [Space(3)]
    [SerializeField] private SpaceShip m_Ship;
    [SerializeField] private GameObject m_PlayerShipPrefab;
    public SpaceShip ActiveShip => m_Ship;
    [SerializeField] private GameObject m_ExplosionPrefab;

    //[SerializeField] private CinemachineVirtualCamera vcam;
    //[SerializeField] private MovementController m_MovementController;
    [Header("Recources view")]
    //[SerializeField] private HitPointView m_HitPointView;
    [SerializeField] private bool m_Racing;
    //[SerializeField] private Boss m_boss;

    private Vector3 m_ExplosionPosition;
    private Quaternion m_ExplosionRotation;

    protected override void Awake()
    {
        base.Awake();

        if(m_Ship != null)
            Destroy(m_Ship.gameObject);
    }
    private void Start()
    {
        Respawn();
    }

    private void Update()
    {
        if (m_Ship != null)
        {
            m_ExplosionPosition = m_Ship.transform.position;
            m_ExplosionRotation = m_Ship.transform.rotation;
        }

    }
    private void OnShipDeath()
    {
        m_NumLives--;
        var Explode = Instantiate(m_ExplosionPrefab, m_ExplosionPosition, m_ExplosionRotation);

        if (m_NumLives > 0)
            Invoke("Respawn", 1);
        else
            LevelSequenceController.Instance?.FinishCurrentLevel(false);
    }

    private void Respawn()
    {
        if(LevelSequenceController.PlayerShip != null)
        {
            var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);
            m_Ship = newPlayerShip.GetComponent<SpaceShip>();
            m_Ship.EventOnDeath.AddListener(OnShipDeath);
            m_Ship.SetDamageMode(m_Racing);

        }
    }

    public void TakeDamage(int damage)
    {
        m_NumLives -= damage;
        if(m_NumLives <= 0)
        {
            LevelSequenceController.Instance.FinishCurrentLevel(false);
        }
    }

    #region Score

    public float Score { get; private set; }
    public int NumKills { get; private set; }

    public void AddKill()
    {
        NumKills++;
    }

    public void AddScore(float num)
    {
        Score += num;
    }

    #endregion
}