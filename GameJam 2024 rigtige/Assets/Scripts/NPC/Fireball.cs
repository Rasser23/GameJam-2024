using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 fireDirection;
    private Rigidbody2D rigidbody;
    private float moveSpeed;
    private float timeExistet = 0f;
    private HealthManager healthManager; // Reference to HPmanager
    float angle;
    void Start()
    {
        moveSpeed = 200f;
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        angle = Vector2.Angle(fireDirection,new Vector2(1,0));
        transform.eulerAngles = new Vector3(0, 0, angle-90);
        // Find HealthManager in the scene
        healthManager = FindObjectOfType<HealthManager>();
        if (healthManager == null)
        {
            Debug.LogError("HealthManager could not be found in the scene!");
        }
    }

    void Update()
    {
        rigidbody.linearVelocity = fireDirection * moveSpeed * Time.deltaTime;
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
            if (healthManager != null)
            {
                healthManager.TakeDamage(1); // Reduce health by 1
                DestroyObj(); // Destroy the fireball
            }
            else
            {
                Debug.LogError("healthManager is not assigned!");
            }
        }
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
