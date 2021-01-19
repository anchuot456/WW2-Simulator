using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNetworkManager : NetworkManager
{
    GameObject army;
    GameObject selection;
    Vector3[] spawnPositionA = {new Vector3(-19.75f, 9.164f, 0),
                                new Vector3(-19.75f, 3.054f, 0),
                                new Vector3(-19.75f, -3.054f, 0),
                                new Vector3(-19.75f, -9.164f, 0), };
    Vector3[] spawnPositionB = {new Vector3(19.75f, 9.164f, 0),
                                new Vector3(19.75f, 3.054f, 0),
                                new Vector3(19.75f, -3.054f, 0),
                                new Vector3(19.75f, -9.164f, 0), };

    //Server
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        selection = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, selection);
        //base.OnServerAddPlayer(conn);
        if (numPlayers == 1)
        {
            foreach(var pos in spawnPositionA)
            {
                army = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "PlayerA"), pos, new Quaternion());
                NetworkServer.Spawn(army, conn);
            }
            

        }
        else
        {
            foreach (var pos in spawnPositionB)
            {
                army = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "PlayerB"), pos, new Quaternion());
                NetworkServer.Spawn(army, conn);
            }
        }
        
    }


    //Client


    //Both
}
