using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BHSCamp.UI
{
    public class LevelChooser : MonoBehaviour
    {
        [Header("Levels Data")]
        [SerializeField] private LevelPreviewData[] _levels;

        [FormerlySerializedAs("_levelPreview")]
        [Header("UI fields")] 
        [SerializeField] private Image _preview;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _highscoreText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Image _lock;

        private int _currentLevelIndex = 0;

        private const string CollectedGemsPrefBase = "collectedGems";
        public static string CollectedGemsPref;
        
        private void OnEnable()
        {
            ShowLevel(_currentLevelIndex);
        }

        private void ShowLevel(int index)
        {
            LevelPreviewData level = _levels[index];
            _preview.sprite = level.Preview;
            _nameText.text = level.Name;
            _playButton.gameObject.SetActive(level.IsAccesible);
            _lock.enabled = false == level.IsAccesible;

            CollectedGemsPref = CollectedGemsPrefBase + index.ToString();
            var collectedGems = PlayerPrefs.GetInt(CollectedGemsPref, 0);
            _highscoreText.text = $"{collectedGems}/{_levels[index].GemsAmount} собрано";
        }

        public void ShowPreviousLevel() => ShowLevel(
            _currentLevelIndex = (_currentLevelIndex - 1 + _levels.Length) % _levels.Length
        );

        public void ShowNextLevel() => ShowLevel(
            _currentLevelIndex = (_currentLevelIndex + 1) % _levels.Length
        );

        public void LoadChoosenLevel()
        {
            CollectedGemsPref = CollectedGemsPrefBase + _currentLevelIndex.ToString();
            SceneManager.LoadScene(_levels[_currentLevelIndex].SceneIndex);
            GameManager.Instance.SetLevelIndex(_currentLevelIndex);
        }
    }
}