using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Pawn : MonoBehaviour
{
	public Action<int, int, bool> HealthUpdatedEvent;
	public Action<Position> PositionUpdatedEvent;

	public int MaxHealth { get; protected set; }
	public int Health { get; protected set; }
	
	public int Actions { get; protected set; }
	public int CurrentActions { get; protected set; }

	public bool IsDead { get; protected set; }
	public PawnControl Control { get; protected set; }

	public int Initiative { get; protected set; }
	public int Speed { get; protected set; }

	public Position Position { get; protected set; }

	public Action MovementAction;
	public List<Action> ActionList = new List<Action>();

	virtual public void Initialize()
	{
		IsDead = false;
		MovementAction = gameObject.AddComponent<ActionMovement>();
		MovementAction.Initialize(this, new ActionData());
		ActionAttack attack = gameObject.AddComponent<ActionAttack>();
		attack.Initialize(this, new ActionData());
		ActionList.Add(attack);
	}

	public void SetPosition(Position _position)
	{
		Position = _position;
		transform.position = Map.Instance.GetWorldPosition(_position);
	}

	public void ResetTurn()
	{
		Actions = CurrentActions;
	}

	public void Takedamage(int _damage)
	{
		Health -= _damage;
		if(Health <= 0)
		{
			Health = 0;
			IsDead = true;
		}
		NotifyHealthUpdate(-_damage);
	}

	public void GainHealth(int _amount)
	{
		Health += _amount;
		if(Health >= MaxHealth)
		{
			Health = MaxHealth;
		}
		NotifyHealthUpdate(_amount);
	}

	public void Move(Position _newPosition)
	{
		Position = _newPosition;
		NotifyPositionUpdate();
	}
	
	protected void NotifyHealthUpdate(int _value)
	{
		if (HealthUpdatedEvent != null)
		{
			HealthUpdatedEvent(Health, _value, IsDead);
		}
	}

	protected void NotifyPositionUpdate()
	{
		if (PositionUpdatedEvent != null)
		{
			PositionUpdatedEvent(Position);
		}
	}
}
