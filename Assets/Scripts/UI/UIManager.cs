using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
    private Stack<GameObject> _openStack;
    /// <summary>
    /// The list of panels opened with keeping tracks of the order of opening.
    /// </summary>
    private List<GameObject> _openNoStack;

    private Button _toggleButton;

    public Button ToggleButton
    {
        get
        {
            return _toggleButton;
        }

        set
        {
            _toggleButton = value;
        }
    }


    /// <summary>
    /// Constructor of the UIManager. Must only be called in GetInstance.
    /// </summary>
    private UIManager()
    {
        _panels = new Dictionary<string, GameObject>();
        _openStack = new Stack<GameObject>();
        _openNoStack = new List<GameObject>();
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
            if (_openStack.Contains(panel) || _openNoStack.Contains(panel))
            {
                return false;
            }
            panel.transform.SetAsLastSibling();
            if (panel != null)
            {
                panel.SetActive(true);
                _openStack.Push(panel);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Shows a panel and registers it as non-stacked panel.
    /// </summary>
    /// <param name="name">The name of the registered panel to show.</param>
    /// <returns>True if the name exists as registered panel that wasn't already open. False if the name doesn't exist of the panel is already shown.</returns>
    public bool ShowPanelNoStack(string name)
    {
        if (_panels.ContainsKey(name))
        {
            GameObject panel = _panels[name];
            if (_openStack.Contains(panel) || _openNoStack.Contains(panel))
            {
                return false;
            }
            panel.transform.SetAsLastSibling();
            if (panel != null)
            {
                panel.SetActive(true);
                _openNoStack.Add(panel);
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
    /// Fades a panel in.
    /// </summary>
    /// <param name="name">The name of the registered panel to fade in.</param>
    /// <returns>True if the name exists as a registered panel that wasn't already open. False if the name doesn't exist or the panel is already shown.</returns>
    public bool FadeInPanelNoStack(string name)
    {
        if (ShowPanelNoStack(name))
        {
            _panels[name].GetComponent<UIPanel>().FadeIn();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Hides a panel and puts it at the lowest level.
    /// </summary>
    /// <param name="name">The name of the panel to hide.</param>
    /// <returns>True is the name exists as a registered panel that was the last open. False if the name doesn't exist or the panel isn't the last.</returns>
    public bool HidePanel(string name)
    {
        if (_panels.ContainsKey(name))
        {
            GameObject panel = _panels[name];
            panel.transform.SetAsFirstSibling();
            if (panel != null)
            {
                if (_openStack.Count > 0)
                {
                    if (_openStack.Peek() == panel)
                    {
                        panel.SetActive(false);
                        _openStack.Pop();
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Hides a panel and puts it at the lowest level.
    /// </summary>
    /// <param name="name">The name of the panel to hide.</param>
    /// <returns>True if the name exists as a registered panel that was open. False if the name doesn't exist or the panel isn't open.</returns>
    public bool HidePanelNoStack(string name)
    {
        if (_panels.ContainsKey(name))
        {
            GameObject panel = _panels[name];
            panel.transform.SetAsFirstSibling();
            if(panel != null)
            {
                if (_openNoStack.Contains(panel))
                {
                    panel.SetActive(false);
                    _openNoStack.Remove(panel);
                    return true;
                }
            }
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
        if (_panels.ContainsKey(name))
        {
            GameObject panel = _panels[name];
            if (panel != null)
            {
                panel.transform.SetAsLastSibling();
                if (_openStack.Count > 0)
                {
                    if (_openStack.Peek() == panel)
                    {
                        panel.GetComponent<UIPanel>().FadeOut();
                        _openStack.Pop();
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Fades a panel out if not stacked.
    /// </summary>
    /// <param name="name">The name of the panel to fade out.</param>
    /// <returns>Tue if the name exists as a registered panel. False if the name doesn't exist of the panel isn't open.</returns>
    public bool FadeOutPanelNoStack(string name)
    {
        if (_panels.ContainsKey(name))
        {
            GameObject panel = _panels[name];
            if(panel != null)
            {
                panel.transform.SetAsLastSibling();
                if (_openNoStack.Contains(panel))
                {
                    panel.GetComponent<UIPanel>().FadeOut();
                    _openNoStack.Remove(panel);
                    return true;
                }
            }
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
        _openStack.Clear();
        _openNoStack.Clear();
    }

    /// <summary>
    /// Hides the panel opened last.
    /// </summary>
    /// <returns></returns>
    public bool HideLastOpen()
    {
        if (_openStack.Count > 0)
        {
            GameObject last = _openStack.Pop();
            last.SetActive(false);
            return true;
        }
        return false;
    }

}
