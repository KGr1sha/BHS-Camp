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
        
        public int Score
        {
            get { return _score; }
        }
        private int _score;

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
            SaveLoadSystem.Initialize(_levels);
            SaveLoadSystem.UnlockCompletedLevels();
        }

        public void AddScore(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(
                    $"Amount should be positive!: {gameObject.name}"
                );
            _score += amount;
            OnScoreChanged?.Invoke();
        }

        public void FinishCurrentLevel()
        {
            SaveLoadSystem.SaveLevel(_currentLevelIndex);
            SceneManager.LoadScene(0); //Back to main menu
            OpenAccessToNextlevel();
        }

        private void OpenAccessToNextlevel()
        {
            if (_currentLevelIndex + 1 < _levels.Length)
                _levels[_currentLevelIndex + 1].IsAccesible = true;
        }

        public void SetLevelIndex(int newIndex)
        {
            _currentLevelIndex = newIndex;
        }
    }
}