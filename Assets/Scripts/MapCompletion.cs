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

    public const string filename = "completion.dat";
    
    [SerializeField] private EpisodeScore[] completionData;
    
    public static void SaveEpisodeResult(int result)
    {
        if(Instance)
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, result);
        else
        {
            Debug.Log($"Complete level with score {result}");
        }
    }

    private new void Awake()
    {
        base.Awake();
        Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
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

    private void SaveResult(Episode currentEpisode, int result)
    {
        foreach (var item in completionData)
        {
            if (item.Episode == currentEpisode)
            {
                if (result > item.Score) item.Score = result;
                Saver<EpisodeScore[]>.Save(filename, completionData);
            }
        }
    }
}
