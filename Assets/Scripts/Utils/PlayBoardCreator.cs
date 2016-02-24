using UnityEngine;
using System.Collections;

public class PlayBoardCreator : MonoBehaviour {

	public GameObject board;
	public GameObject hexagon;
	public GameObject obstacle;
	public GameObject underground;
	public GameObject cursor;
    public string boardName;
	public int width;
	public int height;
	[Range(0,50)]
	public int x;
	[Range(0,50)]
	public int y;

	public float z;


	// Use this for initialization
	void Start () {
		PlayBoardManager.GetInstance ().Init (width, height, null, null);
	}

	void FixedUpdate(){
		if (cursor != null)
			cursor.transform.position = new Vector3(0.866f * x - 0.433f * y, z, 0.75f * y);
	}

	public void BuildHexagon()
	{
		Hexagon hex = PlayBoardManager.GetInstance ().Board.CreateHexagone (x, y);
        if (hex != null && !(hex._posX < 0))
        {
            GameObject go = Instantiate<GameObject>(hexagon);
            go.transform.position = new Vector3(0.866f * x - 0.433f * y, z, 0.75f * y);
            go.transform.parent = board.transform;
            go.name = hexagon.name;//"( " + x + " , " + y + " )";
            hex.GameObject = go;
        }   
	}

    public void BuildSpawnHexagon()
    {
        Hexagon hex = PlayBoardManager.GetInstance().Board.CreateHexagone(x, y);
        if (hex != null && !(hex._posX < 0))
        {
            hex.IsSpawn = true;
            GameObject go = Instantiate<GameObject>(hexagon);
            go.transform.position = new Vector3(0.866f * x - 0.433f * y, z, 0.75f * y);
            go.transform.parent = board.transform;
            go.name = hexagon.name;
            go.layer = LayerMask.NameToLayer("Spawn");
            go.GetComponentInChildren<Renderer>().material.color = HexagonColor._spawn;
            hex.GameObject = go;
        }
    }

    public void ApplyBoost(Hexagon.Boost boost)
    {
        Hexagon hex = PlayBoardManager.GetInstance().Board.GetHexagone(x, y);
        if (hex != null && !(hex._posX < 0))
        {
            hex.BoostElement = boost;
            // Air, Earth, Fire, Metal, Water, Wood, Nothing
            switch (boost)
            {
                case Hexagon.Boost.Air:
                    hex.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._air;
                    break;
                case Hexagon.Boost.Earth:
                    hex.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._earth;
                    break;
                case Hexagon.Boost.Fire:
                    hex.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._fire;
                    break;
                case Hexagon.Boost.Metal:
                    hex.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._metal;
                    break;
                case Hexagon.Boost.Water:
                    hex.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._water;
                    break;
                case Hexagon.Boost.Wood:
                    hex.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._wood;
                    break;
                case Hexagon.Boost.Nothing:
                    break;
                default:
                    break;
            }
        }
    }

    public void RemoveBoost()
    {
        Hexagon hex = PlayBoardManager.GetInstance().Board.GetHexagone(x, y);
        if (hex != null && !(hex._posX < 0))
        {
            hex.BoostElement = Hexagon.Boost.Nothing;
            hex.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._default;
            Logger.Debug("allo");
        }
    }

    public void RemoveHexagon()
	{
		RemoveObstacle ();
		RemoveUnderground ();
		Hexagon hex = PlayBoardManager.GetInstance ().Board.RemoveHexagone(x,y);
		if (hex != null && !(hex._posX<0)) {
			if (hex.GameObject != null) {
				Destroy (hex.GameObject);
				hex.GameObject = null;
			}
		}
	}

	public void BuildObstacle()
	{
		Hexagon hex = PlayBoardManager.GetInstance ().Board.GetHexagone(x, y);
		if (hex != null && !(hex._posX<0)) {
			if (hex.GameObject != null && hex._entity==null) {
				GameObject go = Instantiate<GameObject> (obstacle);
				go.transform.position=new Vector3(0.866f*x-0.433f*y,z,0.75f*y);
				go.transform.parent = hex.GameObject.transform;
				go.name = obstacle.name;
                go.layer = LayerMask.NameToLayer("Obstacle");
				Obstacle o =new Obstacle (hex);
				o._gameobject = go;
				hex._entity = o;
			}
		}
	}

	public void RemoveObstacle()
	{
		Hexagon hex = PlayBoardManager.GetInstance ().Board.GetHexagone(x, y);
		if (hex != null && !(hex._posX<0)) {
			if (hex.GameObject != null && hex._entity!=null) {
				if (hex._entity is Obstacle) {
					Obstacle o = (Obstacle)hex._entity;
					Destroy (o._gameobject);
					hex._entity = null;
				}
			}
		}
	}

	public void BuildUnderground()
	{
		Hexagon hex = PlayBoardManager.GetInstance ().Board.GetHexagone(x, y);
		if (hex != null && !(hex._posX<0)) {
			if (hex.GameObject != null && hex.Underground==null) {
				GameObject go = Instantiate<GameObject> (underground);
				go.transform.position=new Vector3(0.866f*x-0.433f*y,z-0.2f,0.75f*y);
				go.transform.parent = hex.GameObject.transform;
				go.name = underground.name;
				hex.Underground = go;
			}
		}
	}

	public void RemoveUnderground()
	{
		Hexagon hex = PlayBoardManager.GetInstance ().Board.GetHexagone(x, y);
		if (hex != null && !(hex._posX<0)) {
			if (hex.GameObject != null && hex.Underground!=null) {
				Destroy (hex.Underground);
				hex.Underground = null;
			}
		}
	}

    public void SaveBoard()
    {
        JSONObject.BoardToJSON(boardName);
    }

    public void LoadBoard()
    {
        Destroy(board);
        PlayBoardManager.GetInstance().Board = JSONObject.JSONToBoard(ref board, boardName);
        width = PlayBoardManager.GetInstance().Board._width;
        height = PlayBoardManager.GetInstance().Board._height;
    }
}
