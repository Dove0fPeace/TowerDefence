using System;
using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class AllWavesDead_LevelCondition : MonoBehaviour, ILevelCondition
{
    private bool isCompleted;

    private void Start()
    {
        EnemyWavesManager.OnAllWavesDead += () =>
        {
            isCompleted = true;
        };
    }

    public bool IsCompleted
    {
        get { return isCompleted; }
    }
}
