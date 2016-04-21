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
        Server server = ServerManager.GetInstance()._server;
        if (server.Client1 != null)
        {
            server.Client1.RestartGame();
        }
        if (server.Client2 != null)
        {
            server.Client2.RestartGame();
        }
        ManagerManager.GetInstance().ResetAll();
#if( UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void CancelMenu()
    {
        UIManager.GetInstance().HideLastOpen();
        MenuButton.interactable = true;
    }
}
