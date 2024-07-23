using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using BHSCamp.UI;

namespace BHSCamp
{
    public class GameManager : MonoBehaviour
    {
        //lecture 7
        public static GameManager Instance { get; private set; }
        public static event Action OnScoreChanged;

        [SerializeField] private LevelPreviewData[] _levels;
        private int _currentLevelIndex;
        
        private int _coinsCollectedOnLevel;

        public int Score
        {
            get { return _score; }
        }
        private int _score;

        private const string MaxLevelPlayerPref = "MaxLevel";
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return; 
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            UnlockCompletedLevels();
        }

        private void UnlockCompletedLevels()
        {
            var index = PlayerPrefs.GetInt(MaxLevelPlayerPref, 0);
            var accessibleLevels = _levels.Take(index + 1);

            foreach (var level in accessibleLevels)
            {
                level.IsAccesible = true;
            }
        }

        private void SaveMaxAchievedLevelIndex(int levelIndex)
        {
            PlayerPrefs.SetInt(MaxLevelPlayerPref, levelIndex);
        }

        public void AddScore(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(
                    $"Amount should be positive!: {gameObject.name}"
                );
            _score += amount;
            OnScoreChanged?.Invoke();
            _coinsCollectedOnLevel++;
        }

        public void FinishCurrentLevel()
        {
            SaveLevelHighscore();
            SaveMaxAchievedLevelIndex(_currentLevelIndex);
            _coinsCollectedOnLevel = 0;
            SceneManager.LoadScene(0); //Back to main menu
            OpenAccessToNextlevel();
        }

        private void OpenAccessToNextlevel()
        {
            if (_currentLevelIndex + 1 == _levels.Length)
                return;

            _levels[_currentLevelIndex + 1].IsAccesible = true;
        }

        public void SetLevelIndex(int newIndex)
        {
            _currentLevelIndex = newIndex;
        }

        private void SaveLevelHighscore()
        {
            if (PlayerPrefs.GetInt(LevelChooser.CollectedGemsPref, 0) < _coinsCollectedOnLevel)
                PlayerPrefs.SetInt(LevelChooser.CollectedGemsPref, _coinsCollectedOnLevel);
        }
    }
}