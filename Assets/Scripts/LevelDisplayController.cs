using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplayController : MonoBehaviour
{
    [SerializeField] private MapLevel[] m_MapLevels;
    [SerializeField] private BranchLevel[] m_BranchLevels;
    private void Start()
    {
        var drawLevel = 0;
        var score = 1;
        //MapCompletion.Instance.GetEpisodeScore(m_MapLevels[drawLevel].Episode) != 0 && 
        while (score != 0 && drawLevel < m_MapLevels.Length)
        {
            score = m_MapLevels[drawLevel].Initialise();
            drawLevel += 1;
        }
        
        for (int i = drawLevel; i < m_MapLevels.Length; i++)
        {
            m_MapLevels[i].gameObject.SetActive(false);
            print(m_MapLevels[i].name);
        }

        for (int i = 0; i < m_BranchLevels.Length; i++)
        {
            m_BranchLevels[i].TryActivate();
        }
    }
}
