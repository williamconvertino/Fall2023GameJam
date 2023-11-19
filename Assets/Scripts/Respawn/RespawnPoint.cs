using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    
    private BoxCollider2D _boxCollider2D;
    private Bounds _bounds;
    public float respawnOffset = 1.0f;

    private Spawner _spawner = null;
    
    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _bounds = _boxCollider2D.bounds;
        _spawner = GetComponent<Spawner>();
    }

    public void TurnOff()
    {
        if (_spawner == null) return;
        _spawner.active = false;
    }

    public void SpawnPlayers(List<GameObject> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = players[i];
            player.SetActive(true);
            float x = Random.Range(_bounds.min.x, _bounds.max.x);
            float y = _bounds.max.y + respawnOffset;
            player.transform.position = new Vector3(x, y,0);
        }
    }
}
