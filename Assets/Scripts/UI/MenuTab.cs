using UnityEngine;
using UnityEngine.UI;

public class MenuTab : MonoBehaviour
{
    public Button close;
    public Button menuButton; // both open and close

    void Start()
    {
        close.onClick.AddListener(() => gameObject.SetActive(false));
        menuButton.onClick.AddListener(() => Menu.Instance.MenuClicked(gameObject));
    }

    public void Open() => Menu.Instance.OpenMenu(gameObject);
    public void Clicked() => Menu.Instance.MenuClicked(gameObject);
}