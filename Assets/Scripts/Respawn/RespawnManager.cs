using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{

    public float maxDistance = 10.0f;
    public float maxRespawnPointDistance = 5.0f;
    
    public List<GameObject> players;
    private List<GameObject> deadPlayers;

    public List<RespawnPoint> respawnPoints;
    private RespawnPoint currentRespawnPoint;
    private int respawnIndex = 1;

    private PointManager _pointManager;

    private void Start()
    {
        _pointManager = GetComponent<PointManager>();
        deadPlayers = new List<GameObject>();

        GameObject[] branches = GameObject.FindGameObjectsWithTag("Branch");
        
        foreach (GameObject branch in branches)
        {
            RespawnPoint point = branch.GetComponent<RespawnPoint>();
            respawnPoints.Add(point);    
        }
        
        if (respawnPoints.Count == 0) return;
        respawnPoints.Sort((a,b) => (int)(a.transform.position.y - b.transform.position.y));
        currentRespawnPoint = respawnPoints[0];
    }

    private void Update()
    {
        UpdatePlayerDeath();
        UpdateRespawnPoints();
        //if (deadPlayers.Count == players.Count) RespawnPlayers();
        RespawnPlayers();
    }

    private void UpdatePlayerDeath()
    {
        
        GameObject topPlayer = players[0];
        foreach (GameObject player in players)
        {
            if (!player.activeSelf) continue;
            if (player.transform.position.y > topPlayer.transform.position.y) topPlayer = player;
        }

        foreach (GameObject player in players)
        {
            if (!player.activeSelf) continue;
            if (topPlayer.transform.position.y - player.transform.position.y > maxDistance)
            {
                //Kill(player);
            }
            if (currentRespawnPoint.transform.position.y - player.transform.position.y > maxRespawnPointDistance)
            {
                Kill(player);
            }
        }
        
    }

    public void Kill(GameObject player)
    {
        deadPlayers.Add(player);
        player.SetActive(false);
        player.transform.SetParent(null);
    }

    private void UpdateRespawnPoints()
    {
        foreach (GameObject player in players)
        {
            if (respawnPoints.Count <= respawnIndex) return;

            if (!player.activeSelf) continue;
            if (player.transform.position.y > respawnPoints[respawnIndex].transform.position.y)
            {
                currentRespawnPoint = respawnPoints[respawnIndex];
                currentRespawnPoint.TurnOff();
                respawnIndex++;
                _pointManager.givePoint(player);
                PlayerInfo info = player.GetComponent<PlayerInfo>();
                print(info.playerName + " has scored a point! They now have " + _pointManager.getPoints(player) + " points");
                RespawnPlayers();
            }
            
            if (respawnPoints.Count <= respawnIndex) _pointManager.getWinner();
        }

    }

    private void RespawnPlayers()
    {
        if (currentRespawnPoint == null) return;
        if (deadPlayers.Count == 0) return;
        currentRespawnPoint.SpawnPlayers(deadPlayers);
        deadPlayers.Clear();
    }
    
}