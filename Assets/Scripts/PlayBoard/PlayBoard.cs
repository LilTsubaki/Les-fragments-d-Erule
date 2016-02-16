using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play board.
/// </summary>
public class PlayBoard  {

	/// <summary>
	/// The hexagonal grid.
	/// </summary>
	private List<List<Hexagon>> _grid;

	/// <summary>
	/// The width of board.
	/// </summary>
	public readonly int _width;

	/// <summary>
	/// The height of board.
	/// </summary>
	public readonly int _height;

    private List<Hexagon> _spawns;
    
    AStar<Hexagon> _astar;
    
    Vector3 centreTemp = new Vector3();
    float rayonTemp = 0;
    float percent = 0.6f;

    public List<Hexagon> Spawns
    {
        get
        {
            return _spawns;
        }

        set
        {
            _spawns = value;
        }
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="PlayBoard"/> class.
    /// </summary>
    /// <param name="width">Width of board</param>
    /// <param name="height">Height of board</param>
    public PlayBoard(int width, int height){
		_width = width;
		_height = height;
		_grid = new List<List<Hexagon>>();
		_grid.Capacity = (int)width;
		for (var i = 0; i < width; ++i) {
			List<Hexagon> list = new List<Hexagon> ();
			list.Capacity = (int)height;
			for (var j = 0; j < height; ++j) {
				list.Add(new Hexagon(-1,-1, this));
			}
			_grid.Add(list);
		}

        _astar = new AStar<Hexagon>();
        _spawns = new List<Hexagon>();
	}

	/// <summary>
	/// Gets the (x,y) hexagone.
	/// </summary>
	/// <returns>The (x,y) hexagone.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public Hexagon GetHexagone(int x, int y){

		if (x >= 0 && x < _width && y >= 0 && y < _height) {
			return _grid [x] [y];
		}

		return new Hexagon(-1, -1, this);
	}

	/// <summary>
	/// Sets the hexagone.
	/// </summary>
	/// <returns><c>true</c>, if hexagone was set, <c>false</c> otherwise.</returns>
	/// <param name="hex">Hex.</param>
	/// <param name="replace">If set to <c>true</c> replace.</param>
	public bool SetHexagone(Hexagon hex, bool replace=true){

		int x = hex._posX;
		int y = hex._posY;

		if (x >= 0 && x < _width && y >= 0 && y < _height) {

			if (_grid [x] [y]._posX<0) {

				_grid [x] [y] = hex;
				return true;
			} 
			else {
				if (replace) {
					_grid [x] [y] = hex;
					return true;
				}
				else {
					return false;
				}
			}

		}

		return false;
	}

	/// <summary>
	/// Creates the (x,y) hexagone.
	/// </summary>
	/// <returns>The hexagone if sucess.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="replace">If set to <c>true</c> replace.</param>
	public Hexagon CreateHexagone(int x, int y, bool replace=false){
		Hexagon hex = new Hexagon (x, y, this);
		if (SetHexagone (hex, replace)) {
			return hex;
		}
		else {
			return null;
		}
	}

	/// <summary>
	/// Removes and return the (x,y) hexagone.
	/// </summary>
	/// <returns>The hexagone removed.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public Hexagon RemoveHexagone(int x, int y){

		if (x < _width && y < _height) {
			Hexagon hex =_grid [x] [y];

			_grid [x] [y] = new Hexagon(-1,-1,this);

			return hex;
		}

		return null;
	}

    /// <summary>
    /// Register the path a character can follow later. The path is registered in character._pathToFollow
    /// </summary>
    /// <param name="character">The character to move</param>
    /// <param name="hexagon">The hexagon the character will try to reach</param>
    /// <returns>If a path exists</returns>
    public bool FindPathForCharacter(Character character, Hexagon hexagon)
    {
        _astar.reset();
        List<Hexagon> hexagons = _astar.CalculateBestPath(character.Position, hexagon);
        int lengthOfPath = hexagons.Count;
        Logger.Debug(lengthOfPath);
        if (hexagons != null && PlayBoardManager.GetInstance().GetCurrentPlayer().CurrentActionPoints >= lengthOfPath)
        {
            character.PathToFollow = hexagons;
            PlayBoardManager.GetInstance().GetCurrentPlayer().CurrentActionPoints -= lengthOfPath;
            ServerManager.GetInstance()._server.UpdateCharacter(PlayBoardManager.GetInstance().GetCurrentPlayer());
            return true;
        }
        return false;
    }

    public List<List<Hexagon>> GetGrid()
    {
        return _grid;
    }

    public List<Hexagon> GetRange(Range r, Hexagon source)
    {
        int valueRange; 
        if (r.Orientation == Orientation.EnumOrientation.Diagonal)
        {
            valueRange = r.MaxRange + PlayBoardManager.GetInstance().GetCurrentPlayer().RangeModifier * 2;
        }
        else
        {
            valueRange = r.MaxRange + PlayBoardManager.GetInstance().GetCurrentPlayer().RangeModifier;
        }

        if (valueRange < r.MinRange)
            valueRange = r.MinRange;

        List<Hexagon> hexas = new List<Hexagon>();
        hexas.Add(source);
        if (r.Orientation == Orientation.EnumOrientation.Any)
        {
            for(int i = 0; i < _width; i++)
            {
                for(int j = 0; j < _height;j++)
                {
                    if(source.Distance(_grid[i][j]) >= r.MinRange && source.Distance(_grid[i][j]) <= valueRange && source.Distance(_grid[i][j]) > 0)
                    {
                        hexas.Add(_grid[i][j]);
                    }
                }
            }
        }
        else if (r.Orientation == Orientation.EnumOrientation.Line)
        {
            List<Direction.EnumDirection> dirs = Direction.GetLineEnum();
            foreach (Direction.EnumDirection dir in dirs)
            {
                List<Direction.EnumDirection> directions = new List<Direction.EnumDirection>();
                for (int i = 1; i < r.MinRange; i++)
                {
                    directions.Add(dir);
                }
                for (int i = r.MinRange; i <= valueRange; i++)
                {
                    directions.Add(dir);
                    hexas.Add(source.GetTarget(directions));
                }
            }
        }
        else if (r.Orientation == Orientation.EnumOrientation.Diagonal)
        {
            List<Direction.EnumDirection> dirs = Direction.GetDiagonalEnum();
            foreach (Direction.EnumDirection dir in dirs)
            {
                List<Direction.EnumDirection> directions = new List<Direction.EnumDirection>();
                for (int i = 2; i < r.MinRange; i+=2)
                {
                    directions.Add(dir);
                }
                for (int i = r.MinRange; i <= valueRange; i+=2)
                {
                    directions.Add(dir);
                    hexas.Add(source.GetTarget(directions));
                }
            }
        }
        return hexas;
    }

    public void ResetBoard()
    {
        for(int i = 0; i < _width; i++)
        {
            for(int j = 0; j < _height; j++)
            {
                Hexagon hexa = _grid[i][j];
                if (hexa._posX !=-1)
                {
                    hexa.GameObject.GetComponentInChildren<Renderer>().material.color = hexa.DefaultColor;
                    hexa.PreviousColor = hexa.DefaultColor;
                    hexa.Targetable = false;
                }        
            }
        }
    }

    public bool fieldOfView(Hexagon source, Hexagon destination)
    {
        Vector3 coll1 = new Vector3(source.GameObject.transform.position.x, source.GameObject.transform.position.y + 0.5f, source.GameObject.transform.position.z);
        Vector3 coll2 = new Vector3(destination.GameObject.transform.position.x, destination.GameObject.transform.position.y + 0.5f, destination.GameObject.transform.position.z);
        float rayonHexaCarre = (destination.GameObject.transform.localScale.x / 2.0f) * (destination.GameObject.transform.localScale.x / 2.0f) - (destination.GameObject.transform.localScale.x / 4.0f) * (destination.GameObject.transform.localScale.x / 4.0f);

        centreTemp = coll2;
        rayonTemp = Mathf.Sqrt(rayonHexaCarre);

        //centre de nouveau cercle
        Vector3 o1 = coll1 + (coll2 - coll1) * 0.5f;
        float rayon = Vector3.Distance(o1, coll1);

        float xbMoinsXa = destination.GameObject.transform.position.x - o1.x;
        float zbMoinsza = destination.GameObject.transform.position.z - o1.z;

        float a = 2.0f * xbMoinsXa;
        float b = 2.0f * zbMoinsza;
        float c = xbMoinsXa * xbMoinsXa + zbMoinsza * zbMoinsza - rayonHexaCarre + rayon * rayon;
        float delta = (2.0f * a * c) * (2.0f * a * c) - 4.0f * (a * a + b * b) * (c * c - (b * b) * (rayon * rayon));

        Vector3 inter1 = new Vector3();
        Vector3 inter2 = new Vector3();

        inter1.x = o1.x + ((2.0f * a * c - Mathf.Sqrt(delta)) / (2.0f * (a * a + b * b)));
        inter2.x = o1.x + ((2.0f * a * c + Mathf.Sqrt(delta)) / (2.0f * (a * a + b * b)));
        inter1.y = 0.5f;
        inter2.y = 0.5f;

        if (b != 0)
        {
            inter1.z = o1.z + ((c - a * (inter1.x - o1.x)) / b);
            inter2.z = o1.z + ((c - a * (inter2.x - o1.x)) / b);
        }
        else
        {
            float temp = (((2.0f * c) - (a * a)) / (2.0f * a)) * (((2.0f * c) - (a * a)) / (2.0f * a));
            inter1.z = o1.z + b / 2.0f + Mathf.Sqrt(rayonHexaCarre - temp);
            inter2.z = o1.z + b / 2.0f - Mathf.Sqrt(rayonHexaCarre - temp);
        }

        int nbRayon = 20;
        int cptHit = 0;
        //Debug.DrawRay(coll1, (inter1-coll1) * 20, Color.red, 10);
        //Debug.DrawRay(coll1, (inter2 - coll1) * 20, Color.red, 10);
        for (float i = 0; i < 1f + 1f / nbRayon; i += 1f / nbRayon)
        {
            Ray rayTest = new Ray(coll1, (inter1 * i + inter2 * (1 - i)) - coll1);
            //Debug.DrawRay(rayTest.origin, rayTest.direction * 20, Color.cyan, 10);
            RaycastHit rch;
            int layermask = LayerMask.GetMask("Obstacle");
            if (Physics.Raycast(rayTest, out rch, Vector3.Distance(coll1, coll2), layermask))
            {
                //Logger.Error(rch.transform.gameObject.layer);
                if(rch.transform.parent != null)
                {
                    if (destination == rch.transform.parent.GetComponent<HexagonBehaviour>()._hexagon)
                    {
                        ++cptHit;
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if(destination == rch.transform.GetComponent<CharacterBehaviour>()._character.Position)
                    {
                        ++cptHit;
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            ++cptHit;
        }

        //Debug.Log(cptHit);
        return ((float)cptHit) / nbRayon > percent;
    }
}
