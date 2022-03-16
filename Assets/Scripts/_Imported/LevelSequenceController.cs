using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_Menu";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public static SpaceShip PlayerShip {get; set;}
        
        public bool LastLevelSuccess { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        {
            LastLevelSuccess = success;

            CalculateLevelStatistic();
            SaveGlobalStatistics();

            ResultPanelController.Instance.ShowResults(LevelStatistics, LastLevelSuccess);
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if(CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        private void CalculateLevelStatistic()
        {
            float score = Player.Instance.Score * LevelController.Instance.m_ScoreMultiiplier;
            LevelStatistics.score = (int)score;
            LevelStatistics.numKills = Player.Instance.NumKills;
            LevelStatistics.time = (int)LevelController.Instance.LevelTime;
        }

        private void SaveGlobalStatistics()
        {
            int allScore = PlayerPrefs.GetInt("Score") + LevelStatistics.score;
            int AllKills = PlayerPrefs.GetInt("All kills") + LevelStatistics.numKills;
            int AllTime = PlayerPrefs.GetInt("All time") + LevelStatistics.time;

            PlayerPrefs.SetInt("Score", allScore);
            PlayerPrefs.SetInt("All kills", AllKills);
            PlayerPrefs.SetInt("All time", AllTime);

            PlayerPrefs.Save();
        }
    }
}