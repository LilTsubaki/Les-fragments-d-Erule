using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayVideo : MonoBehaviour
{

    public GameObject _trailer;
    public GameObject _sort;
    public GameObject _decks;
    public GameObject _presentation;

    private enum State { idle, trailerPlaying, vanaheimPresentation, sortPlaying, deckPlaying }
    private State _currentState;
    MovieTexture _mtTrailer;
    MovieTexture _mtSort;
    MovieTexture _mtDeck;

    int _audioTrailer;

    void Awake()
    {
        _mtTrailer = (MovieTexture)_trailer.GetComponent<RawImage>().mainTexture;
        _mtSort = (MovieTexture)_sort.GetComponent<RawImage>().mainTexture;
        _mtDeck = (MovieTexture)_decks.GetComponent<RawImage>().mainTexture;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            switch(_currentState)
            {
                case State.idle:
                    Logger.Debug("idle -> trailer");
                    _trailer.SetActive(true);
                    _mtTrailer.Play();
                    _audioTrailer = AudioManager.GetInstance().Play("trailer");
                    _currentState = State.trailerPlaying;
                    break;
                case State.trailerPlaying:
                    Logger.Debug("trailer -> vanaheim");
                    AudioManager.GetInstance().StopPlayer(_audioTrailer);
                    _trailer.SetActive(false);
                    _presentation.SetActive(true);
                    _currentState = State.vanaheimPresentation;
                    break;
                case State.vanaheimPresentation:
                    _presentation.SetActive(false);
                    _sort.SetActive(true);
                    _mtSort.Play();
                    _currentState = State.sortPlaying;
                    break;
                case State.sortPlaying:
                    Logger.Debug("sort -> deck");
                    _sort.SetActive(false);
                    _decks.SetActive(true);
                    _mtDeck.Play();
                    _currentState = State.deckPlaying;
                    break;
                case State.deckPlaying:
                    Logger.Debug("jeu vidéo");
                    SceneManager.LoadScene("Scenes/Tests/Clement/Menu");
                    break;
            }
        }
    }
}