using UnityEngine;
using UnityEngine.UI;

public class RegisterFadeScreen : MonoBehaviour
{
    /// <summary>
    /// The image of the panel. Its alpha value will be modified.
    /// </summary>
    Image _image;
    /// <summary>
    /// Is it fading in ?
    /// </summary>
    bool _fadeIn;
    /// <summary>
    /// Remaining time of the transition.
    /// </summary>
    float _remainingTime;
    /// <summary>
    /// The total time of the transition.
    /// </summary>
    float _speed;

    /// <summary>
    /// The name of the trigger to activate after the transition.
    /// </summary>
    string _anim;

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

    public void SetAnimation(string anim)
    {
        _anim = anim;
    }

    /// <summary>
    /// Resets the transition values and reverses the state of fading in/out.
    /// </summary>
    /// <param name="speed"></param>
    public void Reverse(float speed)
    {
        _fadeIn = !_fadeIn;
        _remainingTime = 1;
        _speed = 1/(speed*0.5f);
    }

    /// <summary>
    /// Diminishes the alpha channel of the image according to the transition speed.
    /// </summary>
    void FadeIn()
    {
        float a = Mathf.Clamp01(_remainingTime);
        Color c = new Color(_image.color.r, _image.color.g, _image.color.b, a);
        _image.color = c;
        _remainingTime = Mathf.Clamp01(_remainingTime - Time.deltaTime*_speed);
    }

    /// <summary>
    /// Increases the alpha channel of the image according to the transition speed.
    /// </summary>
    void FadeOut()
    {
        float a = Mathf.Clamp01(1 - _remainingTime / 1);
        Color c = new Color(_image.color.r, _image.color.g, _image.color.b, a);
        _image.color = c;
        _remainingTime = Mathf.Clamp01(_remainingTime - Time.deltaTime*_speed);
    }

    /// <summary>
    /// Invokes the end of the transition after the specified time.
    /// </summary>
    /// <param name="transitionTime">The time to end the transition after.</param>
    public void FadeTime(float transitionTime)
    {
        Invoke("FadeEnd", transitionTime);
    }

    /// <summary>
    /// Invokes the end of the transition after the specified time when having a trigger to activate.
    /// </summary>
    /// <param name="speed"></param>
    public void FadeTimeAnim(float speed)
    {
        Invoke("FadeEndStartAnim", speed);
    }


    /// <summary>
    /// Puts an end to the transition and activates the trigger previously specified.
    /// </summary>
    public void FadeEndStartAnim()
    {
        Reverse(_speed);
        CameraManager.GetInstance().ActivateMainStartAnim(_anim);
        _anim = "";
    }

    /// <summary>
    /// Puts an end to the transition.
    /// </summary>
    void FadeEnd()
    {
        Reverse(_speed);
        CameraManager.GetInstance().ActivateMain();
    }

}
