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
        FindObjectOfType<EnemyWavesManager>().OnAllWavesDead += () =>
        {
            isCompleted = true;
            print(isCompleted);
        };
    }

    public bool IsCompleted
    {
        get { return isCompleted; }
    }
}
