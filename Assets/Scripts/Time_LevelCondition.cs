using System;
using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class Time_LevelCondition : MonoBehaviour, ILevelCondition
{
    [SerializeField] private float m_TimeLimit = 4.0f;
    private bool levelComplete;

    private bool reached;

    private void Start()
    {
        m_TimeLimit += Time.time;
        TD_Player.Instance.OnPlayerDead += () =>
        {
            levelComplete = true;
        };
    }

    public bool IsCompleted
    {
        get
        {
            if (Time.time > m_TimeLimit && levelComplete == false)
            {
                reached = true;
            }

            return reached;
        }
        
    }
}
