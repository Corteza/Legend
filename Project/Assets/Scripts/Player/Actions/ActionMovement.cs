using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMovement : Action
{
	public override ActionType Type { get { return ActionType.Movement; } }

	public override void Initialize(Pawn _pawnOwner, ActionData _data)
	{
		base.Initialize(_pawnOwner, _data);
	}

	virtual protected int GetMovement()
	{
		return Pawn.Speed;
	}
}
