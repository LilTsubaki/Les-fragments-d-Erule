using System.Collections.Generic;

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

	public Hexagon GetTarget(List<Direction> directions){
		int x = 0;
		int y = 0;

		foreach (var direction in directions) {
			switch (direction)
			{
			case Direction.EnumDirection.East:
				x += 1;
				break;
			case Direction.EnumDirection.DiagonalSouthEast:
				x += 1;
				y -= 1;
				break;
			case Direction.EnumDirection.SouthEast:
				y -= 1;
				break;
			case Direction.EnumDirection.DiagonalSouth:
				x -= 1;
				y -= 2;
				break;
			case Direction.EnumDirection.SouthWest:
				x -= 1;
				y -= 1;
				break;
			case Direction.EnumDirection.DiagonalSouthWest:
				x -= 2;
				y -= 1;
				break;
			case Direction.EnumDirection.West:
				x -= 1;
				break;
			case Direction.EnumDirection.DiagonalNorthWest:
				x -= 1;
				y += 1;
				break;
			case Direction.EnumDirection.NorthWest:
				y += 1;
				break;
			case Direction.EnumDirection.DiagonalNorth:
				x += 1;
				y += 2;
				break;
			case Direction.EnumDirection.NorthEast:
				x += 1;
				y += 1;
				break;
			case Direction.EnumDirection.DiagonalNorthEast:
				x += 2;
				y += 1;
				break;
			default:
				break;
			}
		}

		return _board.GetHexagone (_posX + x, _posY + y);
	}

	public Hexagon GetEast(){
		return _board.GetHexagone (_posX + 1, _posY);
	}

	public Hexagon GetDiagonalSouthEast(){
		return _board.GetHexagone (_posX + 1, _posY - 1);
	}

	public Hexagon GetSouthEast(){
		return _board.GetHexagone (_posX, _posY - 1);
	}

	public Hexagon GetDiagonalSouth(){
		return _board.GetHexagone (_posX - 1, _posY - 2);
	}

	public Hexagon GetSouthWest(){
		return _board.GetHexagone (_posX - 1, _posY - 1);
	}

	public Hexagon GetDiagonalSouthWest(){
		return _board.GetHexagone (_posX - 2, _posY - 1);
	}

	public Hexagon GetWest(){
		return _board.GetHexagone (_posX - 1, _posY);
	}

	public Hexagon GetDiagonalNorthWest(){
		return _board.GetHexagone (_posX - 1, _posY + 1);
	}

	public Hexagon GetNorthWest(){
		return _board.GetHexagone (_posX, _posY + 1);
	}

	public Hexagon GetDiagonalNorth(){
		return _board.GetHexagone (_posX + 1, _posY + 2);
	}

	public Hexagon GetNorthEast(){
		return _board.GetHexagone (_posX + 1, _posY + 1);
	}

	public Hexagon GetDiagonalNorthEast(){
		return _board.GetHexagone (_posX + 2, _posY + 1);
	}

	public Hexagon GetHexa(Direction dir)
	{
		switch (dir)
		{
		case Direction.EnumDirection.East:
			return GetEast();
		case Direction.EnumDirection.DiagonalSouthEast:
			return GetDiagonalSouthEast();
		case Direction.EnumDirection.SouthEast:
			return GetSouthEast();
		case Direction.EnumDirection.DiagonalSouth:
			return GetDiagonalSouth();
		case Direction.EnumDirection.SouthWest:
			return GetSouthWest();
		case Direction.EnumDirection.DiagonalSouthWest:
			return GetDiagonalSouthWest();
		case Direction.EnumDirection.West:
			return GetWest();
		case Direction.EnumDirection.DiagonalNorthWest:
			return GetDiagonalNorthWest();
		case Direction.EnumDirection.NorthWest:
			return GetNorthWest();
		case Direction.EnumDirection.DiagonalNorth:
			return GetDiagonalNorth();
		case Direction.EnumDirection.NorthEast:
			return GetNorthEast();
		case Direction.EnumDirection.DiagonalNorthEast:
			return GetDiagonalNorthEast();
		default:
			return null;
		}
		return null;
	}

}