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

    private Vector3 GeneratePosition()
    {
        Vector3 finalPos = Vector3.zero;
        finalPos.x = Random.Range(_bounds.min.x, _bounds.max.x);
        finalPos.y = (_bounds.max.y + _bounds.min.y) / 2.0f;
        return finalPos;
    }
    
    public void SpawnPlayer(GameObject player)
    {
        player.transform.position = GeneratePosition();
        player.SetActive(true);
    }
}
