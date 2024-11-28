using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 fireDirection;
    private Rigidbody2D rigidbody;
    public float moveSpeed;
    private float timeExistet = 0f;
    private HPmanager hpManager; // Reference to HPmanager

    void Start()
    {
        moveSpeed = 200f;
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();

        // Find HPmanager in the scene
        if (hpManager == null)
        {
            hpManager = FindObjectOfType<HPmanager>();
            if (hpManager == null)
            {
                Debug.LogError("HPmanager could not be found in the scene!");
            }
        }
    }

    void Update()
    {
        rigidbody.velocity = fireDirection * moveSpeed * Time.deltaTime;
        timeExistet += Time.deltaTime;
        if (timeExistet > 4f)
        {
            DestroyObj();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Fireball hit the player");

            if (hpManager != null)
            {
                hpManager.TakeDamage(1); // Reduce health by 1
                DestroyObj(); // Destroy the fireball
            }
            else
            {
                Debug.LogError("HPmanager is not assigned!");
            }
        }
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
