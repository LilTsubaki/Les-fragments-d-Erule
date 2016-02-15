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

    void InstantiateRune(GameObject prefab, Transform parent)
    {
        GameObject runeGO = GameObject.Instantiate(prefab);
        runeGO.transform.SetParent(parent);
        runeGO.transform.Rotate(new Vector3(0, 1, 0), 90);
        runeGO.transform.localPosition = new Vector3(0, 0.3f, 0);
    }

    void InstantiateRunes()
    {
        InstantiateRune(_airRuneAsset, _listGO.transform.GetChild(0));
        InstantiateRune(_earthRuneAsset, _listGO.transform.GetChild(1));
        InstantiateRune(_fireRuneAsset, _listGO.transform.GetChild(2));
        InstantiateRune(_metalRuneAsset, _listGO.transform.GetChild(3));
        InstantiateRune(_waterRuneAsset, _listGO.transform.GetChild(4));
        InstantiateRune(_woodRuneAsset, _listGO.transform.GetChild(5));
    }

    void Awake()
    {
        InstantiateRunes();
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
