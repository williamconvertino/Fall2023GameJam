using Player.Input.ButtonPress;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Acorns : MovingPlatform
{
    public float rotationSpeed = 1;

    // Update is called once per frame
    public override void Update()
    {
        currentTime += Time.deltaTime;
        transform.position = Position(startLoc, currentTime);

        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

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

            Destroy(this.gameObject);
        }
    }
}
