using System.Collections.Generic;
using UnityEngine;

public class MapMovementHelper
{
	private Map m_map;

	public MapMovementHelper(Map _map)
	{
		m_map = _map;
	}

	public MapTile[] GetAvailableTiles(MapTile _mapTile, int _speed)
	{
		return GetAvailableTiles(_mapTile.Position, _speed);
	}

	public MapTile[] GetAvailableTiles(Pawn _pawn, int _speed)
	{
		return GetAvailableTiles(_pawn.Position, _speed);
	}

	public MapTile[] GetAvailableTiles(Position _position, int _speed)
	{
		List<MapTile> availableTiles = new List<MapTile>();
		GetAvailableTiles(ref availableTiles, _position, _speed);
		return availableTiles.ToArray();
	}

	private void GetAvailableTiles(ref List<MapTile> _mapTileList, Position _position, int _speed)
	{
		if(_speed > 0)
		{
			AddTile(ref _mapTileList, _position, _speed, Direction.North);
			AddTile(ref _mapTileList, _position, _speed, Direction.East);
			AddTile(ref _mapTileList, _position, _speed, Direction.South);
			AddTile(ref _mapTileList, _position, _speed, Direction.West);
		}
	}

	private void AddTile(ref List<MapTile> _mapTileList, Position _position, int _speed, Direction _direction)
	{
		MapTile newTile = m_map.GetTile(GetPositionInDirection(_position, _direction));
		if (newTile != null && newTile.Walkable)
		{
			if(!_mapTileList.Contains(newTile))
			{
				_mapTileList.Add(newTile);
			}
			_speed--;
			GetAvailableTiles(ref _mapTileList, newTile.Position, _speed);
		}
	}

	private Position GetPositionInDirection(Position _position, Direction _direction)
	{
		switch(_direction)
		{
			case Direction.North: return new Position(_position.x + 1, _position.y);
			case Direction.East: return new Position(_position.x, _position.y + 1);
			case Direction.South: return new Position(_position.x - 1, _position.y);
			case Direction.West: return new Position(_position.x, _position.y - 1);
		}
		return _position;
	}
}
