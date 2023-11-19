using UnityEngine;

public class Death : MonoBehaviour
{
    GameManager manager;
    public bool movingUpward = true;
    public float movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        manager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // move upwards at steady speed
        float newY = transform.position.y + movementSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            manager.PlayerDeath(player);
        }
    }
}
