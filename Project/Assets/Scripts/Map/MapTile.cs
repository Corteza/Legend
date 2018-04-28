using System.Collections.Generic;
using UnityEngine;

public enum MapTileType
{
	Water,
	Grass,
}

public class MapTile : MonoBehaviour
{
	public Position Position;
	public MapTileType TileType;

	public bool Walkable;

	protected TileVisualHelper m_visualHelper;

	public List<ITileElement> ElementsInTile { get; protected set; }

	private void Awake()
	{
		m_visualHelper = gameObject.AddComponent<TileVisualHelper>();
	}

	public void AddElement(ITileElement _element)
	{
		if(!ElementsInTile.Contains(_element))
		{
			ElementsInTile.Add(_element);
		}
	}

	public void RemoveElement(ITileElement _element)
	{
		if (ElementsInTile.Contains(_element))
		{
			ElementsInTile.Remove(_element);
		}
	}

	public Pawn GetPawnInTile()
	{
		for(int i=0; i< ElementsInTile.Count; i++)
		{
			if(ElementsInTile[i].Type == TileElementType.Pawn && ElementsInTile[i] is Pawn)
			{
				return ElementsInTile[i] as Pawn;
			}
		}
		return null;
	}

	public void SelectTileActive(bool _active, int _num)
	{
		m_visualHelper.SetSelectionActive(_active, _num);
	}
}
