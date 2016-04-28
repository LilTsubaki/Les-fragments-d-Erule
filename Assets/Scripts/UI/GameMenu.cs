using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    public Button MenuButton;

    public void OpenMenu()
    {
        AudioManager.GetInstance().Play("menu");
        UIManager.GetInstance().ShowPanel("menu");
        MenuButton.interactable = false;
    }

    public void Quit()
    {
        Server server = ServerManager.GetInstance()._server;
        if (server != null)
        {
            if (server.Client1 != null)
            {
                server.Client1.RestartGame();
            }
            if (server.Client2 != null)
            {
                server.Client2.RestartGame();
            }
        }
        AudioManager.GetInstance().Play("choixMap");
        ManagerManager.GetInstance().ResetAll();
        #if( UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void CancelMenu()
    {
        AudioManager.GetInstance().Play("menu");
        UIManager.GetInstance().HideLastOpen();
        MenuButton.interactable = true;
    }

    public void OpenVolumes()
    {
        AudioManager.GetInstance().Play("menu");
        UIManager.GetInstance().ShowPanel("panelVolumes");
    }

    public void CancelVolume()
    {
        AudioManager.GetInstance().Play("menu");
        UIManager.GetInstance().HideLastOpen();
    }
}
