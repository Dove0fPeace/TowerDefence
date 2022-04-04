using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplayController : MonoBehaviour
{
    [SerializeField] private MapLevel[] m_MapLevels;
    private void Start()
    {
        var drawLevel = 0;
        var score = 1;

        while (score != 0 && drawLevel < m_MapLevels.Length &&
               MapCompletion.Instance.TryIndex(drawLevel, out var episode,out score))
        {
            m_MapLevels[drawLevel].SetLevelData(episode,score);
            drawLevel += 1;
        }
        
        for (int i = drawLevel; i < m_MapLevels.Length; i++)
        {
            m_MapLevels[i].gameObject.SetActive(false);
        }
    }
}
