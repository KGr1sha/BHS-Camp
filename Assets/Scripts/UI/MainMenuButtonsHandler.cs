using UnityEngine;

public class MainMenuButtonsHandler : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject LevelsMenu;
    [SerializeField] private GameObject OptionsMenu;

    public void ShowLevelMenu() => LevelsMenu.SetActive(true);

    public void ShowOptions() => OptionsMenu.SetActive(true);

    public void Quit() => Application.Quit();
}
