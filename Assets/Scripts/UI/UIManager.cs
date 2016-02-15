using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager
{
    /// <summary>
    /// The mapping between a name and the GameObject of the Panel registered.
    /// </summary>
    private Dictionary<string, GameObject> _panels;
    /// <summary>
    /// The instance of the UIManager (singleton).
    /// </summary>
    static private UIManager _instance;
    /// <summary>
    /// The stack of panels opened.
    /// </summary>
    private Stack<GameObject> _open;

    /// <summary>
    /// The UI used to show player 1 information.
    /// </summary>
    private CharacterUI _uiPlayer1;

    /// <summary>
    /// The UI used to show player 2 information.
    /// </summary>
    private CharacterUI _uiPlayer2;

    public CharacterUI UiPlayer1
    {
        get
        {
            return _uiPlayer1;
        }

        set
        {
            _uiPlayer1 = value;
        }
    }

    public CharacterUI UiPlayer2
    {
        get
        {
            return _uiPlayer2;
        }

        set
        {
            _uiPlayer2 = value;
        }
    }


    /// <summary>
    /// Constructor of the UIManager. Must only be called in GetInstance.
    /// </summary>
    private UIManager()
    {
        _panels = new Dictionary<string, GameObject>();
        _open = new Stack<GameObject>();
    }

    /// <summary>
    /// Gets the only instance of UIManager. Creates it if null.
    /// </summary>
    /// <returns></returns>
    public static UIManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new UIManager();
        }
        return _instance;
    }

    /// <summary>
    /// Registers a GameObject as panel in _panels under the given name.
    /// </summary>
    /// <param name="c">The panel to register.</param>
    /// <param name="name">The name of the panel.</param>
    /// <returns>True if the name didn't already exist. Else false.</returns>
    public bool RegisterPanel(GameObject c, string name)
    {
        if (!_panels.ContainsKey(name))
        {
            _panels.Add(name, c);
            return true;
        }
        return false;
    }

    public void RegisterUiCharacter1(CharacterUI charaUi)
    {
        UiPlayer1 = charaUi;
    }

    public void RegisterUiCharacter2(CharacterUI charaUi)
    {
        UiPlayer2 = charaUi;
    }

    /// <summary>
    /// Shows a panel and puts it in the higher level.
    /// </summary>
    /// <param name="name">The name of the registered panel to show.</param>
    /// <returns>True if the name exists as a registered panel that wasn't already open. False if the name doesn't exist or the panel is already shown.</returns>
    public bool ShowPanel(string name)
    {
        if (_panels.ContainsKey(name))
        {
            GameObject panel = _panels[name];
            if (_open.Contains(panel))
            {
                return false;
            }
            panel.transform.SetAsLastSibling();
            if (panel != null)
            {
                panel.SetActive(true);
                _open.Push(panel);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Fades a panel in.
    /// </summary>
    /// <param name="name">The name of the panel to fade in.</param>
    /// <returns>True if the name exists as a registered panel that wasn't already open. False if the name doesn't exist or the panel is already shown.</returns>
    public bool FadeInPanel(string name)
    {
        if (ShowPanel(name))
        {
            _panels[name].GetComponent<UIPanel>().FadeIn();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Hides a panel and puts it at the lowest level.
    /// </summary>
    /// <param name="name">the name of the panel to hide.</param>
    /// <returns>True is the name exists as a registered panel that was the last open. False if the name doesn't exist or the panel isn't the last.</returns>
    public bool HidePanel(string name)
    {
        if (_panels.ContainsKey(name))
        {
            GameObject panel = _panels[name];
            panel.transform.SetAsFirstSibling();
            if (panel != null)
            {
                if (_open.Peek() == panel)
                {
                    panel.SetActive(false);
                    _open.Pop();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Fades a panel out.
    /// </summary>
    /// <param name="name">The name of the panel to fade out.</param>
    /// <returns>True is the name exists as a registered panel that was the last open. False if the name doesn't exist or the panel isn't the last.</returns>
    public bool FadeOutPanel(string name)
    {
        if (ShowPanel(name))
        {
            _panels[name].GetComponent<UIPanel>().FadeOut();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Hides every open registered panel.
    /// </summary>
    public void HideAll()
    {
        foreach (GameObject go in _panels.Values)
        {
            go.SetActive(false);
        }
        _open.Clear();
    }

    /// <summary>
    /// Hides the panel opened last.
    /// </summary>
    /// <returns></returns>
    public bool HideLastOpen()
    {
        if (_open.Count > 0)
        {
            GameObject last = _open.Pop();
            last.SetActive(false);
            return true;
        }
        return false;
    }

}
