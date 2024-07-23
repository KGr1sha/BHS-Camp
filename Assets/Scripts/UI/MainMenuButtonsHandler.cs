using UnityEngine;

namespace BHSCamp.UI
{
    public class MainMenuButtonsHandler : MonoBehaviour
    {
        [Header("Menus")]
        [SerializeField] private GameObject _levelsMenu;
        [SerializeField] private GameObject _optionsMenu;

        public void ShowLevelMenu() => _levelsMenu.SetActive(true);
        public void ShowOptions() => _optionsMenu.SetActive(true);
        public void Quit() => Application.Quit();
    }
}
