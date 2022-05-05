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

    private int m_TotalScore;
    public int TotalScore => m_TotalScore;
    
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
        UpdateTotalScore();
    }

    private void SaveResult(Episode currentEpisode, int result)
    {
        foreach (var item in completionData)
        {
            if (item.Episode == currentEpisode)
            {
                if (result > item.Score) item.Score = result;
                Saver<EpisodeScore[]>.Save(filename, completionData);
                UpdateTotalScore();
            }
        }
    }

    private void UpdateTotalScore()
    {
        var score = 0;
        foreach (var episodeScore in completionData)
        {
            score += episodeScore.Score;
        }

        m_TotalScore = score;
    }
    public int GetEpisodeScore(Episode episode)
    {
        foreach (var data in completionData)
        {
            if (data.Episode == episode)
                return data.Score;
        }

        return 0; 
    }
}
