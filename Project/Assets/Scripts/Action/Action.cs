using UnityEngine;

public abstract class Action : MonoBehaviour
{
	abstract public ActionType Type { get; }

	public Pawn Pawn { get; protected set; }
	public ActionData Data { get; protected set; }

	virtual public void Initialize(Pawn _pawnOwner, ActionData _data)
	{
		Pawn = _pawnOwner;
		Data = _data;
	}
}
