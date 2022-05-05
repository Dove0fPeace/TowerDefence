using DefaultNamespace;
using UnityEngine;

public class MenuBGM : MonoBehaviour
{
    void Start()
    {
        SoundPlayer.Instance.PlayMenuBGM();
    }
}
