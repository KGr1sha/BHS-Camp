using System;
using UnityEngine;

namespace BHSCamp
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static event Action OnScoreChanged;
        public int Score
        {
            get { return _score; }
        }
        private int _score;

        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    }
}