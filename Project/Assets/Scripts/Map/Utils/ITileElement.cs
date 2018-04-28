
public interface ITileElement
{
	TileElementType Type { get; }

	MapTile Tile { get; }
	Position Position { get; }

	void SetPosition(Position _position);
	void Move(Position _position);
}
