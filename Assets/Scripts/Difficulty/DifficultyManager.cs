using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSCamp
{
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager Instance { get; private set; }
        [SerializeField] private List<DifficultyData> _difficulties;
        public List<DifficultyData> Difficulties
        {
            get { return _difficulties; }
        }
        public DifficultyData Difficulty { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            SetDifficulty(_difficulties[SaveLoadSystem.LoadDifficulty()]);
            DontDestroyOnLoad(gameObject);
        }

        public void SetDifficulty(DifficultyData difficulty)
        {
            Difficulty = difficulty;
        }
    }
}