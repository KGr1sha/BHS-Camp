using System.Collections.Generic;
using System.Linq;
using BHSCamp.UI;
using UnityEngine;

namespace BHSCamp
{
    public static class SaveLoadSystem
    {
        private const string CollectedGemsKey = "CollectedGems";
        private const string MaxCompletedLevelKey = "MaxLevel";
        private const string DifficultyKey = "Difficulty";
        private const string MasterVolumeKey = "Master";
        private const string MusicVolumeKey = "Music";
        private const string SFXVolumeKey = "SFX";
        private static LevelPreviewData[] _levels;
        
        public static void Initialize(LevelPreviewData[] levels)
        {
            _levels = levels;
        }

        public static void SaveSound(SoundSettings settings)
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, settings.Master);
            PlayerPrefs.SetFloat(MusicVolumeKey, settings.Music);
            PlayerPrefs.SetFloat(SFXVolumeKey, settings.SFX);
        }

        public static SoundSettings LoadSound()
        {
            SoundSettings sound = new();
            sound.Master = PlayerPrefs.GetFloat(MasterVolumeKey, 1);
            sound.Music = PlayerPrefs.GetFloat(MusicVolumeKey, 1);
            sound.SFX = PlayerPrefs.GetFloat(SFXVolumeKey, 1);
            return sound;
        }

        public static void UnlockCompletedLevels()
        {
            int index = PlayerPrefs.GetInt(MaxCompletedLevelKey, 0);
            IEnumerable<LevelPreviewData> accesibleLevels = _levels.Take(index + 2);

            foreach (var level in accesibleLevels)
            {
                level.IsAccesible = true;
            }
        }

        public static void SaveLevel(int currentLevelIndex)
        {
            SaveLevelHighscore(currentLevelIndex);
            SaveMaxCompletedLevel(currentLevelIndex); 
        }

        public static void SaveDifficulty(int difficultyIndex)
        {
            PlayerPrefs.SetInt(DifficultyKey, difficultyIndex);
        }

        public static int LoadHighscore(int levelIndex)
        {
            return PlayerPrefs.GetInt(CollectedGemsKey + levelIndex);
        }

        public static int LoadDifficulty()
        {
            return PlayerPrefs.GetInt(DifficultyKey, 0);
        }

        private static void SaveLevelHighscore(int levelIndex)
        {
            int score = GameManager.Instance.Score;
            if (PlayerPrefs.GetInt(CollectedGemsKey + levelIndex, 0) < score)
                PlayerPrefs.SetInt(CollectedGemsKey + levelIndex, score);
        }

        private static void SaveMaxCompletedLevel(int currentLevelIndex)
        {
            int savedIndex = PlayerPrefs.GetInt(MaxCompletedLevelKey, 0);
            if (currentLevelIndex > savedIndex)
                PlayerPrefs.SetInt(MaxCompletedLevelKey, currentLevelIndex);
        }
    }
}