using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.Audio;

public class UIPanel : MonoBehaviour
{

    public string panelID;
    public bool hideOnLoad = true;
    public float fadeSpeed;
    
    void Start()
    {
        UIManager.GetInstance().RegisterPanel(gameObject, panelID);
        if (hideOnLoad)
        {
            UIManager.GetInstance().ShowPanel(panelID);
            UIManager.GetInstance().HidePanel(panelID);
        }
    }

    public void FadeIn()
    {
        Image im = gameObject.GetComponent<Image>();
        im.canvasRenderer.SetAlpha(0);
        im.CrossFadeAlpha(1, fadeSpeed , true);
    }

    public void FadeOut()
    {
        Image im = gameObject.GetComponent<Image>();
        float alpha = im.canvasRenderer.GetAlpha();
        im.CrossFadeAlpha(0, fadeSpeed * alpha, true);
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void ClosePanel()
    {
        UIManager.GetInstance().HidePanel(panelID);
    }

}
