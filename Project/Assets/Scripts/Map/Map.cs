using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Manager<Map>
{
	public MapTile[,] TileGrid;
	public int SizeX;
	public int SizeY;

	private MapMovementHelper m_moveHelper;

	public override string GetManagerName()
	{
		return "_map";
	}

	protected override void InitializeManager()
	{
		m_moveHelper = new MapMovementHelper(this);
	}

	public void SetSize()
	{
		SizeX = TileGrid.GetLength(0);
		SizeY = TileGrid.GetLength(1);
	}

	public MapTile GetTile(Position _position)
	{
		if(!ExistPosition(_position))
		{
			return null;
		}
		return TileGrid[_position.x, _position.y];
	}

	public Position GetPosition(MapTile _mapTile)
	{
		return new Position(_mapTile.Position.x, _mapTile.Position.y);
	}

	public MapTile[] GetWalkableTiles(Position _position, int _speed)
	{
		return m_moveHelper.GetAvailableTiles(_position, _speed);
	}

	public bool ExistPosition(Position _position)
	{
		return _position.x >= 0 && _position.x < SizeX && _position.y >= 0 && _position.y < SizeY;
	}

	public Vector3 GetWorldPosition(Position _position)
	{
		return new Vector3(_position.x - SizeX / 2, 0, _position.y - SizeY / 2);
	}

	protected override void DestroyManager()
	{

	}
}
