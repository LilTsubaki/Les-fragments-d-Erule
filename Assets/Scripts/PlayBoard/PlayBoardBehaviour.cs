using UnityEngine;
using System.Collections;

public class PlayBoardBehaviour : MonoBehaviour
{
    private Hexagon _previousHexagon;
    private HexagonBehaviour _previousHexagonBehaviour;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        Ray ray = CameraManager.GetInstance().Active.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawLine(ray.origin, ray.direction * 20);
        RaycastHit rch;
        //int layermask = (1 << LayerMask.NameToLayer("Default"));
        int layermask = LayerMask.GetMask("Hexagon");
        if (Physics.Raycast(ray, out rch, Mathf.Infinity, layermask))
        {
            HexagonBehaviour hexagonBehaviour = rch.collider.gameObject.GetComponent<HexagonBehaviour>();
            Hexagon hexa = hexagonBehaviour._hexagon;
            if (hexa != null && _previousHexagon != hexa)
            {
                if (_previousHexagon != null && _previousHexagonBehaviour != null)
                {
                    if (_previousHexagon.CurrentState == Hexagon.State.OverAccessible)
                        _previousHexagon.CurrentState = _previousHexagon.PreviousState;

                    if ((_previousHexagon.CurrentState == Hexagon.State.OverSelfTargetable || _previousHexagon.CurrentState == Hexagon.State.OverEnnemiTargetable ||
                         _previousHexagon.CurrentState == Hexagon.State.Targetable) && _previousHexagonBehaviour.FinalArea != null)
                    {
                        for (int i = 0; i < _previousHexagonBehaviour.FinalArea.Count; i++)
                        {
                            if (_previousHexagonBehaviour.FinalArea[i].CurrentState == Hexagon.State.OverSelfTargetable || _previousHexagonBehaviour.FinalArea[i].CurrentState == Hexagon.State.OverEnnemiTargetable)
                            _previousHexagonBehaviour.FinalArea[i].CurrentState = _previousHexagonBehaviour.FinalArea[i].PreviousState;
                        }
                    }
                }


                _previousHexagon = hexa;
                _previousHexagonBehaviour = hexagonBehaviour;

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
