using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class MapStateMachine : Manager<MapStateMachine>
{
	public Action<MapState> StateUpdatedEvent;
	
	public MapState MapState { get; private set; }

	public override string GetManagerName()
	{
		return "_mapStateMachine";
	}

	protected override void InitializeManager()
	{
		MapState = MapState.Start;
	}

	public void SetState(MapState _state, float _delay = 0f)
	{
		if(MapState == _state)
		{
			return;
		}
		if(_delay >= 0)
		{
			Invoke("SetStateWithDelay", _delay);
			return;
		}
		MapState = _state;
		Log("Selected new Map State " + MapState.ToString());
		UpdateState();
		NotifyState();
	}

	private void SetStateWithDelay(MapState _state)
	{
		SetState(_state);
	}

	private void UpdateState()
	{
		switch (MapState)
		{
			case MapState.Start:
				OnStart();
				break;

			case MapState.PlayerTurn:
				OnPlayerTurn();
				break;

			case MapState.EnemyTurn:
				OnEnemyTurn();
				break;

			case MapState.End:
				OnEnd();
				break;
		}
	}

	private void OnStart() { }

	private void OnPlayerTurn() { }

	private void OnEnemyTurn() { }

	private void OnEnd() { }

	private void NotifyState()
	{
		if(StateUpdatedEvent != null)
		{
			StateUpdatedEvent(MapState);
		}
	}

	protected override void DestroyManager()
	{
		StateUpdatedEvent = null;
	}
}
