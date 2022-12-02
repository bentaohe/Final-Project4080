using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class GameManager : NetworkBehaviour {
    public Player playerPrefab;
    public GameObject spawnPoints;

    public override void OnNetworkSpawn()
    {
        if(IsHost)
        {
            SpawnPlayers();
        }
    }

    private void SpawnPlayers()
    {
        foreach (PlayerInfo pi in GameData.Instance.allPlayers) {
            Player playerSpawn = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            playerSpawn.GetComponent<NetworkObject>().SpawnWithOwnership(pi.clientId);
            playerSpawn.PlayerColor.Value = pi.color;
        }
    }
}