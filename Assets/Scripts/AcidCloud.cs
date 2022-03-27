using System;
using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;
using Random = UnityEngine.Random;

public class AcidCloud : MonoBehaviour
{
    [SerializeField] private float m_DamageDelay;
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_SlowPower = 0.4f;
    
    private Timer _timer;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _timer = new Timer(0.1f);
        _animator.speed = Random.Range(0.08f, 0.2f);
    }

    private void Update()
    {
        _timer.RemoveTime(Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.transform.root.GetComponent<Enemy>() != null
           && other.transform.root.GetComponent<Enemy>().Type == TargetLayer.Air) return;
        
        Destructible destructible = other.transform.root.GetComponent<Destructible>();
        if (destructible != null && _timer.IsFinished)
        {
            destructible.ApplyDamage(m_Damage, true);
            _timer.Start(m_DamageDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.root.GetComponent<Enemy>() != null
           && other.transform.root.GetComponent<Enemy>().Type == TargetLayer.Air) return;
        
        TD_PatrolController enemy = other.transform.root.GetComponent<TD_PatrolController>();
        if (enemy != null)
        {
            enemy.SetSlowed(true, m_SlowPower);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.root.GetComponent<Enemy>() != null
           && other.transform.root.GetComponent<Enemy>().Type == TargetLayer.Air) return;
        
        TD_PatrolController enemy = other.transform.root.GetComponent<TD_PatrolController>();
        if (enemy != null)
        {
            enemy.SetSlowed(false, m_SlowPower);
        }
    }
}
