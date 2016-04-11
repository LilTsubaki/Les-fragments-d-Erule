using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    public Button MenuButton;

    public void OpenMenu()
    {
        UIManager.GetInstance().ShowPanel("menu");
        MenuButton.interactable = false;
    }

    public void Quit()
    {
        ManagerManager.GetInstance().ResetAll();
        Application.Quit();
    }

    public void CancelMenu()
    {
        UIManager.GetInstance().HideLastOpen();
        MenuButton.interactable = true;
    }
}
