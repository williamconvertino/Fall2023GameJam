using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private GameObject _player;
    private BoxCollider2D _playerColliderHitbox;
    private Rigidbody2D _rb2d;
    private void Start()
    {
        _player = transform.parent.gameObject;
        BoxCollider2D parentCollider = _player.GetComponent<BoxCollider2D>();
        _playerColliderHitbox = gameObject.AddComponent<BoxCollider2D>();
        _playerColliderHitbox.size = parentCollider.size;
        _playerColliderHitbox.offset = parentCollider.offset;
        _playerColliderHitbox.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherPlayerCollider = other.gameObject;
        if (otherPlayerCollider != null && otherPlayerCollider.CompareTag("PlayerCollider"))
        {
            
        }
    }
}
