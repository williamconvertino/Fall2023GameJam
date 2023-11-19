using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public GameObject brownLeaf;
    public GameObject yellowLeaf;
    public GameObject orangeLeaf;
    public GameObject redLeaf;
    public List<GameObject> hazzards;

    public float brownRate;
    public float yellowRate;
    public float orangeRate;
    public float redRate;
    
    public float hazzardRate;

    private System.Random rand;

    public float leftbound;
    public float rightbound;

    private void Start()
    {
        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rand.NextDouble() <= brownRate * Time.deltaTime) SpawnLeaf(brownLeaf);
        if (rand.NextDouble() <= yellowRate * Time.deltaTime) SpawnLeaf(yellowLeaf);
        if (rand.NextDouble() <= orangeRate * Time.deltaTime) SpawnLeaf(orangeLeaf);
        if (rand.NextDouble() <= redRate * Time.deltaTime) SpawnLeaf(redLeaf);
    }

    void SpawnLeaf(GameObject leafPrefab) {
        MovingPlatform leaf = Instantiate(leafPrefab, transform).GetComponent<MovingPlatform>();
        leaf.startLoc = new Vector2(leftbound + (float)rand.NextDouble() * (rightbound - leftbound), transform.position.y);
    }

    void SpawnHazard() { }
}
