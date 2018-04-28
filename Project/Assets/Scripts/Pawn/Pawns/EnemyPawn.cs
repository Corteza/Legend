using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn
{
	public override void Initialize()
	{
		base.Initialize();
		Control = PawnControl.Computer;
		Speed = 2;
	}
}
