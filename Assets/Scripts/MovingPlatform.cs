using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float currentTime;
    public Vector2 startLoc;
    public float fallrate;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        System.Random rand = new System.Random();
        fallrate = (float)(rand.NextDouble() / 5 + .9);
        startLoc = transform.position;
        currentTime = 0;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        currentTime += Time.deltaTime;
        transform.position = Position(startLoc, currentTime);
    }

    virtual public Vector3 Position(Vector2 start, float time)
    {
        return start - new Vector2(0, fallrate * time);
    }
}
