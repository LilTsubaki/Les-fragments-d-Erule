using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunicBoardBehaviour : MonoBehaviour {
    private RunicBoard _board;

    public GameObject _handGO;
    public GameObject _boardGO;

    public GameObject _airRuneAsset;
    public GameObject _earthRuneAsset;
    public GameObject _fireRuneAsset;
    public GameObject _metalRuneAsset;
    public GameObject _woodRuneAsset;
    public GameObject _waterRuneAsset;

    private GameObject _heldRune;

    private List<GameObject> _runesGO;

    public RunicBoard Board
    {
        get
        {
            return _board;
        }

        set
        {
            _board = value;
        }
    }

    private void DisplayRunesInHand()
    {
        foreach(KeyValuePair<uint, Rune> kvp in Board.RunesInHand)
        {
            Transform parent = _handGO.transform.GetChild((int)kvp.Key);
            GameObject rune = new GameObject();

            switch (kvp.Value.Element._name)
            {
                case "Fire":
                    rune = GameObject.Instantiate(_fireRuneAsset);
                    break;
                case "Water":
                    rune = GameObject.Instantiate(_waterRuneAsset);
                    break;
                case "Air":
                    rune = GameObject.Instantiate(_airRuneAsset);
                    break;
                case "Earth":
                    rune = GameObject.Instantiate(_earthRuneAsset);
                    break;
                case "Wood":
                    rune = GameObject.Instantiate(_woodRuneAsset);
                    break;
                case "Metal":
                    rune = GameObject.Instantiate(_metalRuneAsset);
                    break;
                default:
                    break;
            }

            _runesGO.Add(rune);

            // Set transformation
            rune.GetComponent<RuneBehaviour>()._initialParent = parent;
            rune.transform.SetParent(parent);
            rune.transform.localPosition = new Vector3(0, 0.3f, 0);
            rune.transform.Rotate(new Vector3(0, 1, 0), 90);

            // Associate Rune object and gameObject
            rune.GetComponent<RuneBehaviour>()._rune = kvp.Value;
        }
    }

    public void ResetRunes()
    {
        _board.RemoveAllRunes();
        for (int i = 0; i < _runesGO.Count; i++)
        {
            _runesGO[i].transform.SetParent(_runesGO[i].GetComponent<RuneBehaviour>()._initialParent);
            _runesGO[i].transform.localPosition = new Vector3(0, 0.3f, 0);
        }
    }

    void InputUpdate()
    {
        // If mouse is pressed, check if a rune is underneath. If a rune is found, put his gameObject in _heldRune.
        if (Input.GetMouseButtonDown(0))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(camRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Runes")))
            {
                _heldRune = hitInfo.collider.gameObject;
                RuneBehaviour runeBehaviour = hitInfo.collider.gameObject.GetComponent<RuneBehaviour>();
                if (runeBehaviour != null)
                {
                    runeBehaviour._state = RuneBehaviour.State.Held;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && _heldRune != null)
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            RuneBehaviour runeBehaviour = _heldRune.GetComponent<RuneBehaviour>();

            bool hasBeenMoved = false;

            if (Physics.Raycast(camRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Runes Slot")))
            {
                RuneSlotBehaviour runeSlotBehaviour = hitInfo.collider.gameObject.GetComponent<RuneSlotBehaviour>();
                if (runeSlotBehaviour != null)
                {
                    Rune rune = _heldRune.GetComponent<RuneBehaviour>()._rune;
                    int slotPosition = runeSlotBehaviour._position;
                    if (rune.IsOnBoard())
                    {
                        if(Board.ChangeRunePosition((uint)rune.PositionOnBoard, (uint)slotPosition))
                        {
                            _heldRune.transform.SetParent(hitInfo.collider.transform);
                            _heldRune.transform.localPosition = new Vector3(0, 0.3f, 0);
                            hasBeenMoved = true;
                        }
                        else
                        {
                            hasBeenMoved = false;
                        }
                    }
                    else
                    {
                        int newPositionOnBoard = Board.PlaceRuneOnBoard(rune.PositionInHand, (uint)slotPosition);
                        if (newPositionOnBoard >= 0)
                        {
                            hasBeenMoved = true;
                            Transform parent;
                            if (newPositionOnBoard == 12)
                            {
                                parent = _boardGO.transform.GetChild(9).transform;
                            }
                            else
                            {
                                parent = hitInfo.collider.transform;
                            }
                            _heldRune.transform.SetParent(parent);
                            _heldRune.transform.localPosition = new Vector3(0, 0.3f, 0);
                        }
                        else
                        {
                            hasBeenMoved = false;
                        }
                    }

                }
            }

            if (hasBeenMoved)
            {
                runeBehaviour._state = RuneBehaviour.State.Static;
            }
            else
            {
                runeBehaviour._state = RuneBehaviour.State.BeingReleased;
            }

            _heldRune = null;
        }
    }

    void Awake()
    {
        _runesGO = new List<GameObject>();
        Dictionary<uint, Rune> hand = new Dictionary<uint, Rune>();
        Rune r1 = new Rune(Element.GetElement(0), -1, 0);
        hand.Add(r1.PositionInHand, r1);
        Rune r2 = new Rune(Element.GetElement(1), -1, 1);
        hand.Add(r2.PositionInHand, r2);
        Rune r3 = new Rune(Element.GetElement(3), -1, 2);
        hand.Add(r3.PositionInHand, r3);
        Rune r4 = new Rune(Element.GetElement(3), -1, 3);
        hand.Add(r4.PositionInHand, r4);
        Rune r5 = new Rune(Element.GetElement(4), -1, 4);
        hand.Add(r5.PositionInHand, r5);
        Rune r6 = new Rune(Element.GetElement(5), -1, 5);
        hand.Add(r6.PositionInHand, r6);

        Board = new RunicBoard(hand);
        RunicBoardManager.GetInstance().RegisterBoard(Board);

        DisplayRunesInHand();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        InputUpdate();
	}
}
