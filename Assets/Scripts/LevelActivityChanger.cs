using _Imported;
using UnityEngine;

public class LevelActivityChanger : SingletonBase<LevelActivityChanger>
{
    public void ChangeLevelActivity(bool state)
    {
        void ChangeAll<T>() where T : MonoBehaviour
        {
            foreach (var obj in FindObjectsOfType<T>())
            {
                obj.enabled = state;
            }
        }
        
        ChangeAll<EnemyWavesManager>();
        ChangeAll<EnemyWave>();
        ChangeAll<Tower>();
        ChangeAll<TD_PatrolController>();
        ChangeAll<Projectile>();
        ChangeAll<NextWaveGUI>();
    }
}