using TMPro;
using UnityEngine;


namespace BHSCamp.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
            GameManager.OnScoreChanged += UpdateScore;
        }

        private void OnDisable()
        {
            GameManager.OnScoreChanged -= UpdateScore;
        }

        private void UpdateScore()
        {
            _scoreText.text = GameManager.Instance.Score.ToString();
        }
    }
}