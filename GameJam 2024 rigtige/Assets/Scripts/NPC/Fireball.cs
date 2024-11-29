using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 fireDirection;
    private Rigidbody2D rigidbody;
    private float moveSpeed;
    private float timeExistet = 0f;
    private HealthManager healthManager; // Reference to HPmanager
    float angle;
    bool hit = false;
    void Start()
    {
        moveSpeed = 200f;
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        angle = GetAngleFromVector2(fireDirection);
        transform.eulerAngles = new Vector3(0, 0, angle);
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
        if (other.CompareTag("Player") && !hit)
        {
            if (healthManager != null)
            {
                healthManager.TakeDamage(1); // Reduce health by 1
                hit = true;
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
    float GetAngleFromVector2(Vector2 vector)
    {
        // Use Mathf.Atan2 to calculate the angle in radians
        float angleInRadians = Mathf.Atan2(vector.y, vector.x);

        // Convert radians to degrees
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // Ensure the angle is positive (0 to 360 degrees)
        if (angleInDegrees < 0)
            angleInDegrees += 360;

        return angleInDegrees;
    }
}
