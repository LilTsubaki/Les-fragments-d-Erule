using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterFadeScreen : MonoBehaviour
{

    Image _image;
    bool _fadeIn;
    float _remainingTime;

    void Start()
    {
        _image = gameObject.transform.GetComponent<Image>();
        _fadeIn = true;
        _remainingTime = 0;
        CameraManager.GetInstance().SetFadeScreen(this);
    }

    void Update()
    {
        if (_fadeIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    public void Reverse()
    {
        _fadeIn = !_fadeIn;
        _remainingTime = 1;
    }

    void FadeIn()
    {
        float a = Mathf.Clamp01(_remainingTime / 1);
        Color c = new Color(_image.color.r, _image.color.g, _image.color.b, a);
        _image.color = c;
        _remainingTime = Mathf.Clamp01(_remainingTime - Time.deltaTime);
    }

    void FadeOut()
    {
        float a = Mathf.Clamp01(1 - _remainingTime / 1);
        Color c = new Color(_image.color.r, _image.color.g, _image.color.b, a);
        _image.color = c;
        _remainingTime = Mathf.Clamp01(_remainingTime - Time.deltaTime);
    }

    public void FadeTime()
    {
        Invoke("FadeEnd", 1);
    }

    void FadeEnd()
    {
        Reverse();
        CameraManager.GetInstance().ActivateMain();
    }

}
