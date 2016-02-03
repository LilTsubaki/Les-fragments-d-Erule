using UnityEngine;
using System.Collections;

public class PlayBoardCreator : MonoBehaviour {

	public GameObject board;
	public GameObject hexagon;
	public GameObject obstacle;
	public uint width;
	public uint height;
	[Range(0,50)]
	public uint x;
	[Range(0,50)]
	public uint y;

	public float z;


	// Use this for initialization
	void Start () {
		PlayBoardManager.GetInstance ().Init (width, height, null, null);
	}

	public void BuildHexagon()
	{
		Hexagon hex = PlayBoardManager.GetInstance ().Board.CreateHexagone (x, y);
		if (hex != null && !(hex._posX<0)) {

			GameObject go = Instantiate<GameObject> (hexagon);
			go.transform.position=new Vector3(0.866f*x-0.433f*y,z,0.75f*y);
			go.transform.parent = board.transform;
            go.name = hexagon.name;//"( " + x + " , " + y + " )";
			hex._gameObject = go;
		}
	}

	public void RemoveHexagon()
	{
		RemoveObstacle ();
		Hexagon hex = PlayBoardManager.GetInstance ().Board.RemoveHexagone(x,y);
		if (hex != null && !(hex._posX<0)) {
			if (hex._gameObject != null) {
				Destroy (hex._gameObject);
				hex._gameObject = null;
			}
				
		}
	}

	public void BuildObstacle()
	{
		Hexagon hex = PlayBoardManager.GetInstance ().Board.GetHexagone((int)x,(int)y);
		if (hex != null && !(hex._posX<0)) {
			if (hex._gameObject != null && hex._entity==null) {
				GameObject go = Instantiate<GameObject> (obstacle);
				go.transform.position=new Vector3(0.866f*x-0.433f*y,z+0.5f,0.75f*y);
				go.transform.parent = hex._gameObject.transform;
				go.name = obstacle.name;
				Obstacle o =new Obstacle (hex);
				o._gameobject = go;
				hex._entity = o;

			}

		}
	}

	public void RemoveObstacle()
	{
		Hexagon hex = PlayBoardManager.GetInstance ().Board.GetHexagone((int)x,(int)y);
		if (hex != null && !(hex._posX<0)) {
			if (hex._gameObject != null && hex._entity!=null) {
				if (hex._entity is Obstacle) {
					Obstacle o = (Obstacle)hex._entity;
					Destroy (o._gameobject);
					hex._entity = null;
				}

			}

		}
	}


}
