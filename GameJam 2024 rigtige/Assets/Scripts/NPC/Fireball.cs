using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 fireDirection;
    private Rigidbody2D rigidbody;
    private float moveSpeed;
    private float timeExistet = 0f;
    public HPmanager hpManager; // Reference to HPmanager

    void Start()
    {
        moveSpeed = 200f;
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();

        // Optionally, find HPmanager if not assigned in the Inspector
        if (hpManager == null)
        {
            hpManager = FindObjectOfType<HPmanager>();
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
            if (hpManager != null)
            {
                Debug.Log("FIIIIRREEEE");
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
