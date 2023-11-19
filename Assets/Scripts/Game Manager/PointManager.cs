using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public List<GameObject> players;
    private Dictionary<GameObject, int> scores = new Dictionary<GameObject, int>();

    void Start()
    {
        foreach (var pl in players)
        {
            scores[pl] = 0;
        }
    }
    public void givePoint(GameObject player)
    {
        if (!scores.ContainsKey(player)) scores[player] = 0;
        scores[player]++;
    }

    public int getPoints(GameObject player)
    {
        if (!scores.ContainsKey(player)) return 0;
        return (scores[player]);
    }

    public void getWinner()
    {
        GameObject topDog = null;
        bool tie = false;
        foreach (GameObject player in scores.Keys)
        {
            if (topDog == null || scores[player] > scores[topDog])
            {
                topDog = player;
                tie = false;
            } else if (scores[player] == scores[topDog])
            {
                tie = true;
            }
        }

        print("Final scores:");
        foreach (var player in scores.Keys)
        {
            PlayerInfo pInfo = player.GetComponent<PlayerInfo>();
            print(pInfo.playerName + ": " + scores[player] + " points");
        }
        
        if (tie)
        {
            print("There was a tie!");
            return;
        }
        if (topDog == null)
        {
            print("No winners could be found.");
            return;
        }

        PlayerInfo info = topDog.GetComponent<PlayerInfo>();
        
        print(info.playerName + " Wins!");

    }
}
