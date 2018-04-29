using UnityEngine;
using System;

public sealed class PlayerMapController : Manager<PlayerMapController>, IStateObserver
{
	public System.Action<Pawn> OnInformationUpdated;
	public System.Action<Pawn, Action> OnSelectionUpdated;
	
	public Pawn CurrentPawn;
	public Action CurrentAction;

	private bool m_playerTurnActive = true;

	private Ray m_currentMouseRay;
	private RaycastHit m_currenthit;
	private bool m_hitSuccess;

	private MapTile[] m_selection;

	public override string GetManagerName()
	{
		return "_playerMapController";
	}

	protected override void InitializeManager()
	{
		MapStateMachine.Instance.AddObserver(this);
	}

	public void UpdateState(MapState _newState)
	{
		m_playerTurnActive = false;

		switch(_newState)
		{
			case MapState.StartPlayerTurn:
				ResetPlayer();
				MapStateMachine.Instance.SetState(MapState.PlayerTurn, 0.5f);
				break;

			case MapState.PlayerTurn:
				m_playerTurnActive = true;
				break;

			case MapState.PlayerMoveSelected:
				m_playerTurnActive = true;
				break;

			case MapState.PlayerActionSelected:
				m_playerTurnActive = true;
				break;
		}
	}

	private void ResetPlayer()
	{

	}

	private void Update()
	{
		UpdateHitData();
		UpdateMouseHit();
		if (m_playerTurnActive)
		{
			UpdateMouseClick();
		}
	}

	private void UpdateHitData()
	{
		m_currentMouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		m_hitSuccess = Physics.Raycast(m_currentMouseRay, out m_currenthit, 100);
	}

	private void UpdateMouseHit()
	{
		if (m_hitSuccess)
		{
			Pawn pawn = m_currenthit.collider.GetComponentInParent<Pawn>();
			if (pawn != null)
			{
				OnPawnHit(pawn);
			}
		}
	}
	
	private void OnPawnHit(Pawn _pawn)
	{
		//Log("Hit on " + _pawn.name);
		if (OnInformationUpdated != null)
		{
			OnInformationUpdated(_pawn);
		}
	}

	private void UpdateMouseClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (m_hitSuccess)
			{
				Pawn pawn = m_currenthit.collider.GetComponentInParent<Pawn>();
				if (pawn != null)
				{
					OnSelectPawn(pawn);
				}
			}
		}
		else if(Input.GetMouseButtonDown(1))
		{
			if(CurrentPawn != null)
			{
				OnDeselectPawn(CurrentPawn);
			}
		}
	}

	private void OnSelectPawn(Pawn _pawn)
	{
		//Log("Select " + _pawn.name);
		if (_pawn.Control == PawnControl.Player)
		{
			CurrentPawn = _pawn;
			if (OnSelectionUpdated != null)
			{
				OnSelectionUpdated(CurrentPawn, CurrentAction);
			}
			m_selection = Map.Instance.GetWalkableTiles(CurrentPawn.Position, CurrentPawn.Speed);
			for(int i=0; i<m_selection.Length; i++)
			{
				m_selection[i].SelectTileActive(true, 0);
			}
		}
	}

	private void OnDeselectPawn(Pawn _pawn)
	{
		//Log("Deselect " + _pawn.name);
		if (_pawn.Control == PawnControl.Player)
		{
			CurrentPawn = null;
			CurrentAction = null;
			if (OnSelectionUpdated != null)
			{
				OnSelectionUpdated(CurrentPawn, CurrentAction);
			}
			for (int i = 0; i < m_selection.Length; i++)
			{
				m_selection[i].SelectTileActive(false, 0);
			}
			m_selection = null;
		}
	}

	protected override void DestroyManager()
	{
		if(MapStateMachine.IsSet)
		{
			MapStateMachine.Instance.RemoveObserver(this);
		}
	}
}
