using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    
    private BoxCollider2D _boxCollider2D;
    private Bounds _bounds;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _bounds = _boxCollider2D.bounds;
        _boxCollider2D.enabled = false;
    }

    public void SpawnPlayers(List<GameObject> players)
    {
        float interval = (_bounds.max.x - _bounds.min.x) / (players.Count + 1);
        
        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = players[i];
            player.SetActive(true);
            player.transform.position = new Vector3(_bounds.min.x+(interval*(i+1)),(_bounds.max.y + _bounds.min.y) / 2.0f,0);
        }
    }
}
