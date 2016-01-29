/// <summary>
/// Hexagon.
/// </summary>
public class Hexagon
{
	public readonly int _posX;
	public readonly int _posY;

	public readonly PlayBoard _board;

	public Hexagon (int x, int y, PlayBoard board)
	{
		_posX = x;
		_posY = y;
		_board = board;
	}
}

