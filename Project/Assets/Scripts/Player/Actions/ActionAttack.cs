using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : Action
{
	public override ActionType Type { get { return ActionType.Attack; } }

	public override void Initialize(Pawn _pawnOwner, ActionData _data)
	{
		base.Initialize(_pawnOwner, _data);
	}

	virtual protected int GetDamage()
	{
		return 1;
	}

	virtual protected int GetDistance()
	{
		return 1;
	}
}
