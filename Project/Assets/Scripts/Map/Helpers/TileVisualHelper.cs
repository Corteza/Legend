using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisualHelper : MonoBehaviour
{
	public MapTile Tile;
	public GameObject[] SelectionList;

	private void Awake()
	{
		Tile = GetComponent<MapTile>();
		SelectionList = new GameObject[2];

		SelectionList[0] = transform.Find("Selection").gameObject;
		SelectionList[0].SetActive(false);

		SelectionList[1] = transform.Find("FinalSelection").gameObject;
		SelectionList[1].SetActive(false);
	}

	public void SetSelectionActive(bool _active, int _num)
	{
		SelectionList[_num].SetActive(_active);
	}
}
