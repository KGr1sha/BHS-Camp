using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BHSCamp.UI
{
    public class DifficultyChooser : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _difficultyDropdown;
        private List<DifficultyData> _difficulties => DifficultyManager.Instance.Difficulties;
        private const string DIFFICULTY_KEY = "Difficulty";

        private void Start()
        {
            IEnumerable<string> difficultyNames =
                from difficulty in _difficulties
                select difficulty.Name;
            
            _difficultyDropdown.ClearOptions();
            _difficultyDropdown.AddOptions(difficultyNames.ToList());
            _difficultyDropdown.RefreshShownValue();
            if (PlayerPrefs.HasKey(DIFFICULTY_KEY))
                _difficultyDropdown.value = PlayerPrefs.GetInt(DIFFICULTY_KEY);
        }

        public void SaveDifficulty()
        {
            DifficultyManager.Instance.SetDifficulty(_difficulties[_difficultyDropdown.value]);
            PlayerPrefs.SetInt(DIFFICULTY_KEY, _difficultyDropdown.value);
            PlayerPrefs.Save();
        }
    }
}