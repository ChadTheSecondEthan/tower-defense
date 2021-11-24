using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu Instance { get; private set; }

    GameObject currentMenu;

    void Awake() => Instance = this;

    public void MenuClicked(GameObject menu)
    {
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);
        if (currentMenu != menu)
            currentMenu?.SetActive(false);
        currentMenu = menu;
    }

    public void OpenMenu(GameObject menu)
    {
        currentMenu?.SetActive(false);
        menu.SetActive(true);
        currentMenu = menu;
    }
}
