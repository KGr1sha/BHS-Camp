using UnityEngine;

namespace BHSCamp
{
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager Instance { get; private set; }
        public DifficultyData Difficulty; //{ get; private set; }

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

        public void SetDifficulty(DifficultyData difficulty)
        {
            Difficulty = difficulty;
        }
    }
}