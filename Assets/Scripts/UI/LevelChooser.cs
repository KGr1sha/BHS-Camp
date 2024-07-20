using BHSCamp;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChooser : MonoBehaviour
{
    [Header("Levels Data")]
    [SerializeField] private LevelPreviewData[] _levels;

    [Header("UI fields")]
    [SerializeField] private Image _levelPreviewImage;
    [SerializeField] private TMP_Text _levelNameText;
    [SerializeField] private Button _playButton;

    private int _currentLevelIndex = 0;

    private void OnEnable()
    {
        ShowLevel(_currentLevelIndex);
    }

    private void ShowLevel(int index)
    {
        _levelPreviewImage.sprite = _levels[_currentLevelIndex].LevelPreview;
        _levelNameText.text = _levels[_currentLevelIndex].LevelName;
        _playButton.interactable = _levels[_currentLevelIndex].IsAccesible;
    }

    public void ShowPreviousLevel() => ShowLevel(
        _currentLevelIndex = (_currentLevelIndex -1 + _levels.Length) % _levels.Length
        );

    public void ShowNextLevel() => ShowLevel(
        _currentLevelIndex = (_currentLevelIndex + 1 ) % _levels.Length
        );

    public void LoadChoosenLevel()
    {
        SceneManager.LoadScene(_levels[_currentLevelIndex].CorrespondingSceneIndex);
        GameManager.Instance.SetLevelIndex(_currentLevelIndex);
    }
}
