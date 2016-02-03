using UnityEngine;
using System.Collections;

public class PlayBoardCreator : MonoBehaviour {

	public GameObject board;
	public GameObject hexagon;
	public uint width;
	public uint height;
	public uint x;
	public uint y;

	public float z;


	// Use this for initialization
	void Start () {
		PlayBoardManager.GetInstance ().Init (width, height, null, null);
	}

	public void BuildHexagone()
	{
		Hexagon hex = PlayBoardManager.GetInstance ().Board.CreateHexagone (x, y);
		if (hex != null) {

			GameObject go = Instantiate<GameObject> (hexagon);
			go.transform.position=new Vector3(0.75f*x,0.666f*y-0.433f*x,z);
			go.transform.parent = board.transform;
			hex._gameObject = go;
		}
	}

}
