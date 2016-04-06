using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.Audio;

public class UIPanel : MonoBehaviour
{

    public string panelID;
    public bool hideOnLoad = true;
    public float fadeSpeed;

    private float _now;
    private bool _isFadingIn;
    private bool _isFadingOut;
    private CanvasGroup _group;

    void Start()
    {
        UIManager.GetInstance().RegisterPanel(gameObject, panelID);
        UIManager.GetInstance().ShowPanelNoStack(panelID);
        if (hideOnLoad)
        {
            UIManager.GetInstance().HidePanelNoStack(panelID);
        }
        _isFadingIn = false;
        _isFadingOut = false;

        _group = gameObject.AddComponent<CanvasGroup>();
    }

    void Update()
    {
        if (_isFadingIn)
        {
            _now += Time.deltaTime;
            float alpha = _now / fadeSpeed;
            _group.alpha = alpha;
            if(alpha > 1)
            {
                _isFadingIn = false;
            }
        }
        if(_isFadingOut)
        {
            _now += Time.deltaTime;
            float alpha = 1 - (_now / fadeSpeed);
            _group.alpha = alpha;
            if (alpha < 0)
            {
                _isFadingOut = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void FadeIn()
    {
        _isFadingOut = false;
        _isFadingIn = true;
        _now = 0;
        /*Image im = gameObject.GetComponent<Image>();
        im.canvasRenderer.SetAlpha(0);
        im.CrossFadeAlpha(1, fadeSpeed , true);*/
    }

    public void FadeOut()
    {
        _isFadingIn = false;
        _isFadingOut = true;
        _now = 0;
        /*Image im = gameObject.GetComponent<Image>();
        float alpha = im.canvasRenderer.GetAlpha();
        im.CrossFadeAlpha(0, fadeSpeed * alpha, true);*/
    }

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    void ClosePanel()
    {
        UIManager.GetInstance().HidePanel(panelID);
    }

}
