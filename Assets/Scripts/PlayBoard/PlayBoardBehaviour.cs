using UnityEngine;
using System.Collections;

public class PlayBoardBehaviour : MonoBehaviour
{
    private Hexagon _previousHexagon;
    private HexagonBehaviour _previousHexagonBehaviour;
    private double _timedOut = 3;
    private double _currentTime = 0;

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
            if (hexa != null)
            {
                if(_previousHexagon != hexa)
                {
                    _currentTime = 0;

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
                        hexagonBehaviour.MakeFinalArea();
                    }
                    if (hexa.CurrentState == Hexagon.State.Accessible)
                        hexa.CurrentState = Hexagon.State.OverAccessible;
                }
                else
                {
                    _currentTime += Time.fixedDeltaTime;
                    if(_currentTime > _timedOut)
                    {
                        MakeSpell(hexagonBehaviour);
                    }
                }
            }
            else
                _previousHexagon.CurrentState = _previousHexagon.PreviousState;
        }
    }


    public void MakeSpell(HexagonBehaviour hexagonBehaviour)
    {
        Hexagon hexa = hexagonBehaviour._hexagon;
        if (hexa != null && (hexa.CurrentState == Hexagon.State.OverEnnemiTargetable || hexa.CurrentState == Hexagon.State.OverSelfTargetable
            || hexa.CurrentState == Hexagon.State.Targetable))
        {
            if (hexagonBehaviour.FinalArea == null)
            {
                hexagonBehaviour.MakeFinalArea();
            }
            SpellManager.getInstance().ApplyEffects(hexagonBehaviour.FinalArea, hexa);
            PlayBoardManager.GetInstance().Board.ResetBoard();
        }
    }
}
