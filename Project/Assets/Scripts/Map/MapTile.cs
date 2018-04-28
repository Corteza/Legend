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
	private GameObject m_selection;

	private void Awake()
	{
		m_selection = transform.Find("Selection").gameObject;
		m_selection.SetActive(false);
	}

	public void SelectTileActive(bool _active)
	{
		m_selection.SetActive(_active);
	}
}
