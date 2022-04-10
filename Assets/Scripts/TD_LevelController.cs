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
            StopLevelActivity();
            ResultPanelController.Instance.ShowResults(null,false);
        };

        m_ReferenceTime += Time.time;
        
        EventLevelCompleted.AddListener(() =>
        {
            StopLevelActivity();
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

    private void StopLevelActivity()
    {
        void DisableAll<T>() where T : MonoBehaviour
        {
            foreach (var obj in FindObjectsOfType<T>())
            {
                obj.enabled = false;
            }
        }
        
        DisableAll<EntitySpawner>();
        DisableAll<Tower>();
        DisableAll<TD_PatrolController>();
        DisableAll<Projectile>();
        DisableAll<NextWaveGUI>();
    }
}
