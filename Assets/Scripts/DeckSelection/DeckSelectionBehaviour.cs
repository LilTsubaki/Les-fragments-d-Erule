using UnityEngine;
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

    private GameObject _heldRune;

    void InstantiateRune(GameObject prefab, Element element, Transform parent)
    {
        GameObject runeGO = GameObject.Instantiate(prefab);
        runeGO.transform.SetParent(parent);
        runeGO.transform.Rotate(new Vector3(0, 1, 0), 90);
        runeGO.transform.localPosition = new Vector3(0, 0.3f, 0);
        runeGO.GetComponent<RuneBehaviour>()._rune = new Rune(element, -1, -1);
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
                RuneBehaviour runeBehaviour = hitInfo.collider.gameObject.GetComponent<RuneBehaviour>();
                if (runeBehaviour != null)
                {
                    runeBehaviour._state = RuneBehaviour.State.BeingTaken;
                }
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
                        if(_deckSelection.PlaceRuneInHand(rune, runeSlotBehaviour._position))
                        {
                            runeSlotBehaviour._runeGO = _heldRune;
                            _heldRune.transform.SetParent(hitInfo.collider.transform);
                        }
                    }
                    // Rune is already in hand
                    else
                    {
                        if(_deckSelection.ChangeRunePosition(rune.PositionInHand, runeSlotBehaviour._position))
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

            runeBehaviour._state = RuneBehaviour.State.BeingReleased;
            _heldRune = null;
        }
    }

    void Awake()
    {
        _deckSelection = new DeckSelection();
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
