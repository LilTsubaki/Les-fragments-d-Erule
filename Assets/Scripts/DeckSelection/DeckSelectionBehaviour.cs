using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeckSelectionBehaviour : MonoBehaviour {

    private DeckSelection _deckSelection;

    public GameObject _listGO;
    public GameObject _handGO;

    public GameObject _airRuneAsset;
    public GameObject _earthRuneAsset;
    public GameObject _fireRuneAsset;
    public GameObject _metalRuneAsset;
    public GameObject _waterRuneAsset;
    public GameObject _woodRuneAsset;

    public Image _tooltipAir;
    public Image _tooltipEau;
    public Image _tooltipFeu;
    public Image _tooltipBois;
    public Image _tooltipMetal;
    public Image _tooltipTerre;

    private GameObject _heldRune;

    public DeckSelection DeckSelection
    {
        get
        {
            return _deckSelection;
        }

        set
        {
            _deckSelection = value;
        }
    }

    void InstantiateRune(GameObject prefab, Element element, Transform parent)
    {
        GameObject runeGO = GameObject.Instantiate(prefab);
        runeGO.transform.SetParent(parent);
        runeGO.transform.localPosition = new Vector3(0, 0.3f, 0);
        runeGO.transform.rotation = prefab.transform.rotation;
        runeGO.GetComponent<RuneBehaviour>()._rune = new Rune(element, -1, -1);
        runeGO.transform.localScale = new Vector3(4.5f,4.5f,4.5f);
    }

    void InstantiateRunes()
    {
        InstantiateRune(_airRuneAsset, Element.GetElement(2), _listGO.transform.GetChild(0));
        InstantiateRune(_earthRuneAsset, Element.GetElement(3), _listGO.transform.GetChild(1));
        InstantiateRune(_fireRuneAsset, Element.GetElement(0), _listGO.transform.GetChild(2));
        InstantiateRune(_metalRuneAsset, Element.GetElement(5), _listGO.transform.GetChild(3));
        InstantiateRune(_waterRuneAsset, Element.GetElement(1), _listGO.transform.GetChild(4));
        InstantiateRune(_woodRuneAsset, Element.GetElement(4), _listGO.transform.GetChild(5));
    }

    private void UpdateRuneToolTip(RuneBehaviour rb)
    {
        Element elem = rb._rune.Element;

        _tooltipAir.gameObject.SetActive(false);
        _tooltipEau.gameObject.SetActive(false);
        _tooltipFeu.gameObject.SetActive(false);
        _tooltipBois.gameObject.SetActive(false);
        _tooltipMetal.gameObject.SetActive(false);
        _tooltipTerre.gameObject.SetActive(false);

        switch (elem._name)
        {
            case "Fire":
                _tooltipFeu.gameObject.SetActive(true);
                break;
            case "Water":
                _tooltipEau.gameObject.SetActive(true);
                break;
            case "Air":
                _tooltipAir.gameObject.SetActive(true);
                break;
            case "Earth":
                _tooltipTerre.gameObject.SetActive(true);
                break;
            case "Wood":
                _tooltipBois.gameObject.SetActive(true);
                break;
            case "Metal":
                _tooltipMetal.gameObject.SetActive(true);
                break;
        }
    }

    void InputUpdate()
    {
        // If mouse is pressed, check if a rune is underneath. If a rune is found, put his gameObject in _heldRune.
        if (Input.GetMouseButtonDown(0))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(camRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Runes")))
            {
                _heldRune = hitInfo.collider.gameObject;
                RuneBehaviour behaviourHeldRune = _heldRune.GetComponent<RuneBehaviour>();
                if (behaviourHeldRune._rune.PositionInHand < 0)
                {
                    InstantiateRune(_heldRune, behaviourHeldRune._rune.Element, _heldRune.transform.parent);
                }
                RuneBehaviour runeBehaviour = hitInfo.collider.gameObject.GetComponent<RuneBehaviour>();
                if (runeBehaviour != null)
                {
                    runeBehaviour._state = RuneBehaviour.State.BeingTaken;
                }
                UpdateRuneToolTip(behaviourHeldRune);
            }
        }

        if (Input.GetMouseButtonUp(0) && _heldRune != null)
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            RuneBehaviour runeBehaviour = _heldRune.GetComponent<RuneBehaviour>();

            if (Physics.Raycast(camRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Runes Slot")))
            {
                RuneSlotBehaviour runeSlotBehaviour = hitInfo.collider.gameObject.GetComponent<RuneSlotBehaviour>();
                if (runeSlotBehaviour != null)
                {
                    Rune rune = _heldRune.GetComponent<RuneBehaviour>()._rune;
                    
                    // Rune is not in hand
                    if (rune.PositionInHand < 0)
                    {
                        if(DeckSelection.PlaceRuneInHand(rune, runeSlotBehaviour._position))
                        {
                            if(runeSlotBehaviour._runeGO != null)
                            {
                                Destroy(runeSlotBehaviour._runeGO);
                            }
                            runeSlotBehaviour._runeGO = _heldRune;
                            _heldRune.transform.SetParent(hitInfo.collider.transform);
                        }
                        else
                        {
                            Destroy(_heldRune);
                        }
                    }
                    // Rune is already in hand
                    else
                    {
                        if(DeckSelection.ChangeRunePosition(rune.PositionInHand, runeSlotBehaviour._position))
                        {
                            if (runeSlotBehaviour._runeGO != null)
                            {
                                GameObject runeSource = runeSlotBehaviour._runeGO;
                                runeSlotBehaviour._runeGO.transform.SetParent(_heldRune.transform.parent);
                                runeSlotBehaviour._runeGO.GetComponent<RuneBehaviour>()._state = RuneBehaviour.State.BeingReleased;
                                runeSlotBehaviour._runeGO = _heldRune;

                                RuneSlotBehaviour slotSource = _heldRune.GetComponentInParent<RuneSlotBehaviour>();
                                slotSource._runeGO = runeSource;
                            }
                            else
                            {
                                runeSlotBehaviour._runeGO = _heldRune;
                                RuneSlotBehaviour slotSource = _heldRune.GetComponentInParent<RuneSlotBehaviour>();
                                slotSource._runeGO = null;
                            }
                            _heldRune.transform.SetParent(hitInfo.collider.transform);
                        }
                    }
                }
            }
            else
            {
                if (_heldRune.GetComponent<RuneBehaviour>()._rune.PositionInHand >= 0)
                {
                    DeckSelection.RemoveRuneFromHand(_heldRune.GetComponent<RuneBehaviour>()._rune.PositionInHand);
                }
                Destroy(_heldRune);
            }

            runeBehaviour._state = RuneBehaviour.State.BeingReleased;
            _heldRune = null;
            AudioManager.GetInstance().Play("releaseRune", true, false);
        }
    }

    void Awake()
    {
        _tooltipAir.gameObject.SetActive(false);
        _tooltipEau.gameObject.SetActive(false);
        _tooltipFeu.gameObject.SetActive(false);
        _tooltipBois.gameObject.SetActive(false);
        _tooltipMetal.gameObject.SetActive(false);
        _tooltipTerre.gameObject.SetActive(false);
        DeckSelection = new DeckSelection();
        InstantiateRunes();
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        InputUpdate();
	}
}
