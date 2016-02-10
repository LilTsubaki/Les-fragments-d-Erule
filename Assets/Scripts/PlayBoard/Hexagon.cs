using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hexagon.
/// </summary>
public class Hexagon : IAStar<Hexagon>
{
	public readonly int _posX;
	public readonly int _posY;

	public readonly PlayBoard _board;

    public Entity _entity;

    public Dictionary<uint, GroundOnTimeAppliedEffect> _onTimeEffects;

    private bool _targetable;
    private Color _defaultColor;
    private Color _previousColor;

    private GameObject _gameObject;
	public GameObject GameObject
    {
        get { return _gameObject; }
        set {
            _gameObject = value;
            _gameObject.layer = LayerMask.NameToLayer("Hexagon");
            if (_gameObject.GetComponent<HexagonBehaviour>() == null)
            {
                _gameObject.AddComponent<HexagonBehaviour>();
            }
            _gameObject.GetComponent<HexagonBehaviour>()._hexagon = this;
        }
    }

	private GameObject _underground;

	public GameObject Underground {
		get {
			return _underground;
		}
		set {
			_underground = value;
		}
	}

    public bool Targetable
    {
        get
        {
            return _targetable;
        }

        set
        {
            _targetable = value;
        }
    }

    public Color PreviousColor
    {
        get
        {
            return _previousColor;
        }

        set
        {
            _previousColor = value;
        }
    }

    public Color DefaultColor
    {
        get
        {
            return _defaultColor;
        }

        set
        {
            _defaultColor = value;
        }
    }

    public static bool isHexagonSet(Hexagon hex){
		return hex != null && hex._posX >= 0 && hex._posY >= 0;
	}

	public Hexagon (int x, int y, PlayBoard board)
	{
        Targetable = false;
        _posX = x;
		_posY = y;
		_board = board;
        _onTimeEffects = new Dictionary<uint, GroundOnTimeAppliedEffect>();
	}

    public bool hasValidPosition()
    {
        if (_posX < 0 || _posY < 0)
            return false;
        return true;
    }

	public Hexagon GetTarget(List<Direction.EnumDirection> directions){
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

	public Hexagon GetHexa(Direction.EnumDirection dir)
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
	}

    public bool isVisible()
    {
        if(_entity != null)
        {
            if ((PlayBoardManager.GetInstance().Character1.Position == this || PlayBoardManager.GetInstance().Character2.Position == this) && hasValidPosition())
                return true;
            else
                return false;
        }
        else
        {
            if (hasValidPosition())
                return true;
            else
                return false;
        }
    }

    public bool isReachable()
    {
        return _entity == null && hasValidPosition();
    }

    public List<Hexagon> GetNeighbours()
    {
        List<Hexagon> neighbours = new List<Hexagon>();

        Hexagon E = GetEast();
        Hexagon NE = GetNorthEast();
        Hexagon SE = GetSouthEast();
        Hexagon W = GetWest();
        Hexagon NW = GetNorthWest();
        Hexagon SW = GetSouthWest();

        if (E.isReachable())
            neighbours.Add(E);

        if (NE.isReachable())
            neighbours.Add(NE);

        if (SE.isReachable())
            neighbours.Add(SE);

        if (W.isReachable())
            neighbours.Add(W);

        if (NW.isReachable())
            neighbours.Add(NW);

        if (SW.isReachable())
            neighbours.Add(SW);

        return neighbours;
    }

    public int Distance(Hexagon t)
    {
        int diffX = t._posX - _posX;
        int diffY = t._posY - _posY;
        if ((diffX >= 0 && diffY >= 0) || (diffX <= 0 && diffY <= 0))
            return Math.Max(Math.Abs(diffX), Math.Abs(diffY));

        return Math.Abs(diffX) + Math.Abs(diffY);
    }

    public int Cost()
    {
        return 1;
    }

    /// <summary>
    /// Adds an effect to the Hexagon. If already registered, refreshed it.
    /// </summary>
    /// <param name="effect">The effect to add to the Hexagon.</param>
    public void AddOnTimeEffect(GroundOnTimeAppliedEffect effect)
    {
        _onTimeEffects[effect.GetId()] = effect;
    }

    /// <summary>
    /// Removes an effect from the Hexagon.
    /// </summary>
    /// <param name="effect">The effect to remove.</param>
    public void RemoveOnTimeEffect(GroundOnTimeAppliedEffect effect)
    {
        _onTimeEffects.Remove(effect.GetId());
    }

    /// <summary>
    /// Apply every effect affecting the Hexagon.
    /// </summary>
    public void ApplyOnTimeEffects()
    {
        List<Hexagon> list = new List<Hexagon>();
        list.Add(this);
        foreach (GroundOnTimeAppliedEffect effect in _onTimeEffects.Values)
        {
            effect.ApplyEffect(list, this, effect.GetCaster());
        }
    }

    /// <summary>
    /// Reduces the remaining time of the effects casted by a Character. If on have no turn remaining, removes it.
    /// </summary>
    /// <param name="c">The caster we have to decrease the effects number of turn.</param>
    public void ReduceOnTimeEffectsCastedBy(Character c)
    {
        foreach(GroundOnTimeAppliedEffect effect in _onTimeEffects.Values)
        {
            if(effect.GetCaster() == c)
            {
                effect.ReduceNbTurn(this);
            }
        }
    }

    public JSONObject HexaToJSON()
    {
        JSONObject hexa = new JSONObject(JSONObject.Type.OBJECT);
        hexa.AddField("posX", _posX);
        hexa.AddField("posY", _posY);
        hexa.AddField("posZ", _gameObject.transform.localPosition.y);
        hexa.AddField("gameObject", _gameObject.name);

		if(_underground!=null)
			hexa.AddField("underground", _underground.name);

        if(_entity!=null && _entity is Obstacle)
        {
            hexa.AddField("obstacle", ((Obstacle)_entity)._gameobject.name);
        }

        return hexa;
    }
}