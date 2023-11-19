using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // *** Track players, scores, & checkpoints
    // *** Handle player deaths & respawns

    GameObject[] playerArray;
    int[] playerScores;
    int topPlayerIndex;

    
    public Vector3 startPosition;
    Vector3 currCheckpoint;
    public float respawnPositionDist = 2;
    float respawnXOffset;

    GameObject deathObject;
    public float resetDeathYOffset = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerArray = GameObject.FindGameObjectsWithTag("Player");
        playerScores = new int[playerArray.Length];
        topPlayerIndex = 0;
        currCheckpoint = startPosition;
        deathObject = GameObject.FindGameObjectWithTag("Death");

        respawnXOffset = 0 - respawnPositionDist / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // find current top player
        float topHeight = -9999999;
        for (int i = 0; i < playerArray.Length; i++)
        {
            float playerHeight = playerArray[i].transform.position.y;
            if (playerHeight > topHeight)
            {
                topHeight = playerHeight;
                topPlayerIndex = i;
            }
        }

        // TODO: update current checkpoint if top player has passed a new checkpoint
    }

    public void PlayerDeath(GameObject player)
    {
        Debug.Log("Player died");

        // increase top player's score by 1
        playerScores[topPlayerIndex] += 1;

        // respawn all players at current checkpoint
        for (int i = 0; i < playerArray.Length; i++)
        {
            GameObject p = playerArray[i];
            float newX = currCheckpoint.x + respawnXOffset + respawnPositionDist * i;
            p.transform.position = new Vector3(newX, currCheckpoint.y, 0);
        }

        // TODO: this part isn't working
        //reset death line to be below checkpoint
        float deathNewY = deathObject.transform.position.y - resetDeathYOffset;
        deathObject.transform.position = new Vector3(deathObject.transform.position.x, deathNewY, 0);
    }

}
