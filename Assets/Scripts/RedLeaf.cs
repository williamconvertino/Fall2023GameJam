using Player.Input.ButtonPress;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class RedLeaf : MovingPlatform
{
    public int numJumps;
    public override void Update()
    {
        currentTime += Time.deltaTime;
        transform.position = Position(startLoc, currentTime);

        // check if object has child
        if (transform.childCount > 0) {
            // fades and then destroys the leaf
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade() {
        Color c = GetComponent<Renderer>().material.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.01f)
        {
            c.a = alpha;
            GetComponent<Renderer>().material.color = c;
            if (alpha <= 0.1f) {
                // reparent children
                foreach (Transform child in transform) {
                    child.parent = transform.parent;
                }

                // destroy leaf
                Destroy(gameObject);  
            }
            yield return null;
        }
    }
}
