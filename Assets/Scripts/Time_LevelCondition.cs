using System;
using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class Time_LevelCondition : MonoBehaviour, ILevelCondition
{
    [SerializeField] private float m_TimeLimit = 4.0f;

    private void Start()
    {
        m_TimeLimit += Time.time;
    }

    public bool IsCompleted => Time.time > m_TimeLimit;
}
