using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectorHelper : IStateObserver
{
	public MapTile CurrentTileOptions;
	public MapTile CurrentTileOnFocus;

	public Pawn CurrentPawnOnFocus;

	public void Initialize()
	{
		MapStateMachine.Instance.AddObserver(this);
	}

	public void UpdateState(MapState _newState)
	{

	}

	public void Clear()
	{
		MapStateMachine.Instance.RemoveObserver(this);
	}
}
