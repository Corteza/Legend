using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MapStateMachine : Manager<MapStateMachine>
{
	public MapState MapState { get; private set; }

	private List<IStateObserver> m_observerList = new List<IStateObserver>();

	public override string GetManagerName()
	{
		return "_mapStateMachine";
	}

	protected override void InitializeManager()
	{
		ClearObservers();
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
		NotifyObservers();
	}

	private void SetStateWithDelay(MapState _state)
	{
		SetState(_state);
	}
	
	public void AddObserver(IStateObserver _observer, bool _notifyCurrentState = false)
	{
		if(!m_observerList.Contains(_observer))
		{
			m_observerList.Add(_observer);
			if(_notifyCurrentState)
			{
				_observer.UpdateState(MapState);
			}
		}
	}

	public void RemoveObserver(IStateObserver _observer)
	{
		if(m_observerList.Contains(_observer))
		{
			m_observerList.Remove(_observer);
		}
	}

	private void NotifyObservers()
	{
		for(int i=0; i<m_observerList.Count; i++)
		{
			m_observerList[i].UpdateState(MapState);
		}
	}

	public void ClearObservers()
	{
		m_observerList.Clear();
	}

	protected override void DestroyManager()
	{
		ClearObservers();
	}
}
