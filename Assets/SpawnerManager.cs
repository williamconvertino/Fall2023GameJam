using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private Spawner[] spawners;
    public List<GameObject> players;

    public float heightDiff = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        spawners = GetComponentsInChildren<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform top = players[0].transform;

        if (players[1].transform.position.y > top.position.y) top = players[1].transform;
        
        foreach (Spawner spawner in spawners)
        {
            spawner.active = Mathf.Abs(spawner.transform.position.y - top.transform.position.y) < heightDiff;
        }
    }
}
