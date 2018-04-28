using UnityEngine;

public class MapLoader : MonoBehaviour
{
	private void Start()
	{
		MapStateMachine.CreateInstance();
		MapFactory.CreateMap(6, 6);
		PlayerMapController.CreateInstance();

		GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
		PlayerPawn player = Instantiate<GameObject>(playerPrefab).GetComponent<PlayerPawn>();
		player.SetPosition(new Position(1, 1));
		player.Initialize();

		GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
		EnemyPawn enemy = Instantiate<GameObject>(enemyPrefab).GetComponent<EnemyPawn>();
		enemy.SetPosition(new Position(3, 4));
		enemy.Initialize();

		Destroy(gameObject);
	}
}
