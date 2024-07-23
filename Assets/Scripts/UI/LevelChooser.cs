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
        [SerializeField] private Button _playButton;
        [SerializeField] private Image _lock;

        private int _currentLevelIndex = 0;

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
        }

        public void ShowPreviousLevel() => ShowLevel(
            _currentLevelIndex = (_currentLevelIndex - 1 + _levels.Length) % _levels.Length
        );

        public void ShowNextLevel() => ShowLevel(
            _currentLevelIndex = (_currentLevelIndex + 1) % _levels.Length
        );

        public void LoadChoosenLevel()
        {
            SceneManager.LoadScene(_levels[_currentLevelIndex].SceneIndex);
            GameManager.Instance.SetLevelIndex(_currentLevelIndex);
        }
    }
}