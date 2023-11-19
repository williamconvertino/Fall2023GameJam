using Player.Input.ButtonPress;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Acorns : MovingPlatform
{
    // Check collision with player and then kill
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Find Game Manager in scene
            GameObject gameManager = GameObject.Find("Game Manager");
            RespawnManager respawnManager = gameManager.GetComponent<RespawnManager>();

            // Kill player
            respawnManager.Kill(collision.gameObject);
        }
    }
}
