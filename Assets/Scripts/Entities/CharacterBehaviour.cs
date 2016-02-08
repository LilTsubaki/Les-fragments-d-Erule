using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour
{
    public Character _character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(1) && TurnManager.GetInstance().isMyTurn(_character) && _character._state != Character.State.Moving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, ray.direction * 20);
            RaycastHit rch;
            //int layermask = (1 << LayerMask.NameToLayer("Default"));
            int layermask = LayerMask.GetMask("Hexagon");
           
            if (Physics.Raycast(ray, out rch, Mathf.Infinity, layermask))
            {
                Hexagon hexa = rch.collider.gameObject.GetComponent<HexagonBehaviour>()._hexagon;
                if (hexa != null)
                {
                    
                    PlayBoardManager.GetInstance().Board.FindPathForCharacter(_character, hexa);
                    _character.CurrentStep = 0;

                    if (_character.PathToFollow != null && _character.PathToFollow.Count > 0)
                        _character._state = Character.State.Moving;
                }
            }
        }
        if (_character._state == Character.State.Moving)
        {
            Move();
        }

    }

    bool goTo(Hexagon hexa)
    {
        //Debug.Log("go to : " + hexa.x + "  " + hexa.y);
        Vector3 temp = new Vector3(0.0f, 0.5f, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, hexa.GameObject.transform.position + temp, 0.05f);
        return Mathf.Approximately(Vector3.SqrMagnitude(hexa.GameObject.transform.position + temp  - transform.position), 0);
    }

    void Move()
    {
        if(_character.PathToFollow != null && _character.PathToFollow.Count > 0)
        {
            if(_character.CurrentStep == 0)
            {
                PlayBoardManager.GetInstance().Board.ResetBoard();
            }
            if (_character.CurrentStep <= _character.PathToFollow.Count && goTo(_character.PathToFollow[_character.PathToFollow.Count -1 - _character.CurrentStep]))
            {
                //_character.PathToFollow[_character.PathToFollow.Count - 1 - _character.CurrentStep].onPlayerEnter(this);
                _character.CurrentStep++;
                if(_character.CurrentStep == _character.PathToFollow.Count)
                {
                    //fieldOfView();
                    _character.Position = _character.PathToFollow[0];
                    _character._state = Character.State.Waiting;
                }
            }
        }
    }
}
