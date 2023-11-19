using Player.Input.ButtonPress;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class RedLeaf : MovingPlatform
{
    public float stand_time;
    public int num_children;
    public override void Update()
    {
        base.Update();

        if (transform.childCount > 0)
        {
            stand_time += Time.deltaTime;
        }
        else stand_time = 0;

        // check if object has child
        if (stand_time > 0.1f) { 
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
