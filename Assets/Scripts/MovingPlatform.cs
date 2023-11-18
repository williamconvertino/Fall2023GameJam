using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float currentTime;
    public Vector2 startLoc;
    // Start is called before the first frame update
    void Awake()
    {
        startLoc = transform.position;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        transform.position = Position(startLoc, currentTime);
    }

    virtual public Vector3 Position(Vector2 start, float time)
    {
        return start - new Vector2(0, time);
    }
}
