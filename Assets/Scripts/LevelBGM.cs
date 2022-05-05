using DefaultNamespace;
using UnityEngine;

public class LevelBGM : MonoBehaviour
{
    private void Start()
    {
        SoundPlayer.Instance.PlayBGM();
    }
}
