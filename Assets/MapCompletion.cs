using System;
using _Imported;
using UnityEngine;

public class MapCompletion : SingletonBase<MapCompletion>
{
    [Serializable] private class EpisodeScore
    {
        public Episode Episode;
        public int Score;
    }

    public bool TryIndex(int id, out Episode episode, out int score)
    {
        if (id >= 0 && id <= completionData.Length)
        {
            episode = completionData[id].Episode;
            score = completionData[id].Score;
            return true;
        }

        episode = null;
        score = 0;
        return false;
    }
    public static void SaveEpisodeResult(int result)
    {
        Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, result);
    }

    [SerializeField] private EpisodeScore[] completionData;
    private void SaveResult(Episode currentEpisode, int result)
    {
        foreach (var item in completionData)
        {
            if (item.Episode == currentEpisode)
            {
                if (result > item.Score) item.Score = result;
            }
        }
    }
}
