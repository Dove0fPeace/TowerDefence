using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class TD_LevelController : LevelController
{
    private int LevelScore = 3;
    private new void Start()
    {
        base.Start();
        TD_Player.Instance.OnPlayerDead += () =>
        {
            LevelActivityChanger.Instance.ChangeLevelActivity(false);
            ResultPanelController.Instance.ShowResults(null,false);
        };

        m_ReferenceTime += Time.time;
        
        EventLevelCompleted.AddListener(() =>
        {
            LevelActivityChanger.Instance.ChangeLevelActivity(false);
            if (m_ReferenceTime <= Time.time)
            {
                LevelScore -= 1;
            }
            MapCompletion.SaveEpisodeResult(LevelScore);
        });

        void LifeScoreChange(int _)
        {
            LevelScore -= 1;
            TD_Player.OnLifeUpdate -= LifeScoreChange;
        }

        TD_Player.OnLifeUpdate += LifeScoreChange;
    }
}
