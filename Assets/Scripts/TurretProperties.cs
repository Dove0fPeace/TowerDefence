using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public enum TurretMode
{
    Primary,
    Secondary,
    Auto
}

[CreateAssetMenu]
public sealed class TurretProperties : ScriptableObject
{
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private Projectile m_ProjectilePrefab;
    public Projectile ProgectilePrefab => m_ProjectilePrefab;

    [SerializeField] private float m_RateOfFire;
    public float RateOfFire => m_RateOfFire;

    [SerializeField] private int m_EnergyUsgae;
    public int EnergyUsage => m_EnergyUsgae;

    [SerializeField] private int m_AmmoUsage;
    public int AmmoUsage => m_AmmoUsage;

    [SerializeField] private AudioClip m_LaunchSFX;
    public AudioClip LaunchSFX => m_LaunchSFX;
}