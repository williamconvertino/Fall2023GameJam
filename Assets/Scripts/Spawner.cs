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

    public bool active = true;

    public float leftbound;
    public float rightbound;

    private float prevHeight;

    private void Start()
    {
        rand = new System.Random();
        Collider2D collider = GetComponent<BoxCollider2D>();
        if (collider == null) return;

        Bounds b = collider.bounds;
        leftbound = b.min.x;
        rightbound = b.max.x;
        prevHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        float weight = 1 + 5*(transform.position.y - prevHeight) * Time.deltaTime;
        if (rand.NextDouble()*weight <= brownRate * Time.deltaTime) SpawnLeaf(brownLeaf);
        if (rand.NextDouble() * weight <= yellowRate * Time.deltaTime) SpawnLeaf(yellowLeaf);
        if (rand.NextDouble() * weight <= orangeRate * Time.deltaTime) SpawnLeaf(orangeLeaf);
        if (rand.NextDouble() * weight <= redRate * Time.deltaTime) SpawnLeaf(redLeaf);
    }

    void SpawnLeaf(GameObject leafPrefab) {
        MovingPlatform leaf = Instantiate(leafPrefab, transform).GetComponent<MovingPlatform>();
        leaf.startLoc = new Vector2(leftbound + (float)rand.NextDouble() * (rightbound - leftbound), transform.position.y);
    }

    void SpawnHazard() { }
}
