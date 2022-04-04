using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class TD_LevelController : LevelController
{
    public int LevelScore => 1;
    private new void Start()
    {
        base.Start();
        TD_Player.Instance.OnPlayerDead += () =>
        {
            StopLevelActivity();
            ResultPanelController.Instance.ShowResults(null,false);
        };

        m_EventLevelCompleted.AddListener(() =>
        {
            StopLevelActivity();
            MapCompletion.SaveEpisodeResult(LevelScore);
        });
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
    }
}
