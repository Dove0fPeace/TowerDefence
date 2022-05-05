using DefaultNamespace;
using UnityEngine.Playables;

public enum Sound
{
    Arrow = 0,
    ArrowHit = 1,
    EnemyDie = 2,
    EnemyWin = 3,
    PlayerLose = 4,
    PlayerWin = 5,
}

public static class SoundExtention
{
    public static void Play(this Sound sound)
    {
        SoundPlayer.Instance.Play(sound);
    }
}


