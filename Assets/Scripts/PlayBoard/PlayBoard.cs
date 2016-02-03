using System.Collections.Generic;

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
	private int _width;

	/// <summary>
	/// The height of board.
	/// </summary>
	private int _height;

    private Character _character1;
    private Character _character2;

    AStar<Hexagon> _astar;

	/// <summary>
	/// Initializes a new instance of the <see cref="PlayBoard"/> class.
	/// </summary>
	/// <param name="width">Width of board</param>
	/// <param name="height">Height of board</param>
	public PlayBoard(int width, int height){
		_width = width;
		_height = height;
		_grid = new List<List<Hexagon>>();
		_grid.Capacity = width;
		for (var i = 0; i < width; ++i) {
			List<Hexagon> list = new List<Hexagon> ();
			list.Capacity = height;
			for (var j = 0; j < height; ++j) {
				list.Add(new Hexagon(-1,-1, this));
			}
			_grid.Add(list);
		}

        _astar = new AStar<Hexagon>();
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

		if (x >= 0 && x < _width && y >= 0 && y < _height) {
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
        List<Hexagon> hexagons = _astar.CalculateBestPath(character._position, hexagon);
        if (hexagon != null)
        {
            character._pathToFollow = hexagons;
            return true;
        }
        return false;
    }

}
