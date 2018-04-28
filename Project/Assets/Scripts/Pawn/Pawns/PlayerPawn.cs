using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : Pawn
{
	public override void Initialize()
	{
		base.Initialize();
		Control = PawnControl.Player;
		Speed = 4;
	}
}
