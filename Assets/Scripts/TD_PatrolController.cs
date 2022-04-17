using System;
using System.Diagnostics;
using _Imported;
using UnityEngine;
using UnityEngine.Events;

public enum ArmorType
{
    Physic = 0,
    Magic = 1
}
public class TD_PatrolController : Destructible
{
    [SerializeField] private ArmorType m_ArmorType;
    [SerializeField] private int m_Armor;
    
    private Path m_Path;
    private int m_PathIndex;
    [SerializeField] private UnityEvent OnEndPath;

    [SerializeField] private float m_Speed = 1.0f;
    [SerializeField] private float m_MinimumSpeed;
    private float _basicSpeed;
    
    [SerializeField] private Waypoint m_Waypoint;
    private Vector3 _movePosition;
    public Vector3 MovePosition => _movePosition;

    private bool slowed;

    public event Action OnEnd;

    private void Awake()
    {
        _basicSpeed = m_Speed;
        print($"Set basic speed {name}");
    }

    private void FixedUpdate()
    {
        transform.Translate(_movePosition * (Time.deltaTime * m_Speed), Space.World);
    }

    protected override void OnDestroy()
    {
        OnEnd?.Invoke();
        base.OnDestroy();
    }

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
            OnEndPath.Invoke(); ;
            Destroy(gameObject);    
        }
    }

    public void SetSlowed(bool slow, float slowPower)
    {
        if (slow)
        {
            if(slowed) return;
            m_Speed -= slowPower;
            print($"Set slowed {name}");
            slowed = true;
            if (m_Speed < m_MinimumSpeed)
                m_Speed = m_MinimumSpeed;
        }

        else
        {
            m_Speed = _basicSpeed;
            slowed = false;
        }
    }
    public void SetWaypoint(Waypoint point)
    {
        m_Waypoint = point;
        _movePosition = (m_Waypoint.transform.position - transform.position).normalized;
    }

    public virtual void ApplyDamage(int damage, bool playersProjectile, DamageType type)
    {
        /*
        switch (type)
        {
            case DamageType.Magic:
                damage = Mathf.Max(1, damage - m_MagicArmor);
                break;
            case DamageType.Physic:
                damage = Mathf.Max(1, damage - m_PhysicArmor);
                break;
        }
        */
        damage = ArmorDamageFunction(damage, type);
        base.ApplyDamage(damage, playersProjectile);
    }

    /*
    private static Func<int, DamageType, int, int>[] ArmorDamageFunction =
    {
        (int power, DamageType damageType, int armor) =>
        {//ArmorType = Physic
            switch (damageType)
            {
                case DamageType.Magic: return power;
                default: return Mathf.Max(power - armor, 1);
            }
        },
        (int power, DamageType damageType, int armor) =>
        {//ArmorType = Magic
            if (damageType == DamageType.Physic)
                armor = armor / 2;
            
            return Mathf.Max(power - armor, 1);
        }
    };
    */

    private int ArmorDamageFunction(int damage, DamageType type)
    {
        var armor = m_Armor;
        switch (m_ArmorType)
        {
            case ArmorType.Physic:
            {
                switch (type)
                {
                    case DamageType.Magic:
                    {
                        return damage;
                    }
                    case DamageType.Physic:
                    {
                        return Mathf.Max(damage - m_Armor, 1);
                    }
                }
                break;
            }
            
            case ArmorType.Magic:
            {
                if (type == DamageType.Physic)
                    armor = armor / 2;
                return Mathf.Max(1, damage - armor);
            }
                
        }
        return damage;
    }
}