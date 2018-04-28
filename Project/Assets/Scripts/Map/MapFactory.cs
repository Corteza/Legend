using UnityEngine;

public class MapFactory : MonoBehaviour
{
	private const string GRASS_PATH = "Prefabs/Grass";
	private const string WATER_PATH = "Prefabs/Water";

	private static bool m_prefabsInitialized;
	private static GameObject m_grassPrefab;
	private static GameObject m_waterPrefab;

	private static int m_mapXMiddle;
	private static int m_mapYMiddle;

	private static void InitializePrefabs(int _x, int _y)
	{
		if(!m_prefabsInitialized)
		{
			m_prefabsInitialized = true;
			m_grassPrefab = Resources.Load<GameObject>(GRASS_PATH);
			m_waterPrefab = Resources.Load<GameObject>(WATER_PATH);
		}
		m_mapXMiddle = (_x / 2);
		m_mapYMiddle = (_y / 2);
	}

	public static Map CreateMap(int _x, int _y)
	{
		InitializePrefabs(_x, _y);
		Map map = Map.CreateInstance();
		map.TileGrid = new MapTile[_x, _y];
		for (int i = 0; i<_x; i++)
		{
			for(int j=0; j<_y; j++)
			{
				map.TileGrid[i, j] = CreateTile(map.transform, i, j, Random.Range(0,100));
			}
		}
		map.SetSize();
		return map;
	}

	public static MapTile CreateTile(Transform _parent, int _x, int _y, int _seed)
	{
		MapTileType type = _seed < 80 ? MapTileType.Grass : MapTileType.Water;
		GameObject tileObj = Instantiate<GameObject>(GetPrefab(type), _parent);
		tileObj.name = "_tile_" + _x + "_" + _y + "_" + type.ToString();
		tileObj.transform.position = new Vector3(_x - m_mapXMiddle, 0, _y - m_mapYMiddle);
		MapTile newTile = tileObj.AddComponent<MapTile>();
		newTile.TileType = type;
		newTile.Position = new Position(_x, _y);
		newTile.Walkable = type == MapTileType.Grass;
		return newTile;
	}

	private static GameObject GetPrefab(MapTileType _type)
	{
		switch(_type)
		{
			case MapTileType.Grass: return m_grassPrefab;
			case MapTileType.Water: return m_waterPrefab;
		}
		return null;
	}
}
