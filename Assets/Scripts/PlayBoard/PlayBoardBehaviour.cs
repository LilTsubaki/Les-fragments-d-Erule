using UnityEngine;
using System.Collections;

public class PlayBoardBehaviour : MonoBehaviour
{
    private Hexagon _previousHexagon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, ray.direction * 20);
        RaycastHit rch;
        //int layermask = (1 << LayerMask.NameToLayer("Default"));
        int layermask = LayerMask.GetMask("Hexagon");
        if (Physics.Raycast(ray, out rch, Mathf.Infinity, layermask))
        {
            HexagonBehaviour hexagonBehaviour = rch.collider.gameObject.GetComponent<HexagonBehaviour>();
            Hexagon hexa = hexagonBehaviour._hexagon;
            if (hexa != null && _previousHexagon != hexa)
            {
                if (_previousHexagon != null)
                {
                    if (_previousHexagon.CurrentState == Hexagon.State.OverAccessible)
                        _previousHexagon.CurrentState = _previousHexagon.PreviousState;

                    if ((_previousHexagon.CurrentState == Hexagon.State.OverSelfTargetable || _previousHexagon.CurrentState == Hexagon.State.OverEnnemiTargetable ||
                         _previousHexagon.CurrentState == Hexagon.State.Targetable) && hexagonBehaviour.FinalArea != null)
                    {
                        for (int i = 0; i < hexagonBehaviour.FinalArea.Count; i++)
                        {
                            if (hexagonBehaviour.FinalArea[i].CurrentState == Hexagon.State.OverSelfTargetable || hexagonBehaviour.FinalArea[i].CurrentState == Hexagon.State.OverEnnemiTargetable)
                                hexagonBehaviour.FinalArea[i].CurrentState = hexagonBehaviour.FinalArea[i].PreviousState;
                        }
                    }
                }


                _previousHexagon = hexa;

                //on mouse enter new hexa
                if (hexa.CurrentState == Hexagon.State.Targetable)
                {
                    hexagonBehaviour.Make_finalArea();
                }
                if (hexa.CurrentState == Hexagon.State.Accessible)
                    hexa.CurrentState = Hexagon.State.OverAccessible;
            }
        }
    }
}
