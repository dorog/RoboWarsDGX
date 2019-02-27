using UnityEngine;

public interface IGameMode
{
    SpawnedPlayerData SpawnPlayer(Vector3 spawnPoint);
}
