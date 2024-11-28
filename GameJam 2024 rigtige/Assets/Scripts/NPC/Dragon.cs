using UnityEngine;

public class DragonBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Speed of movement
    [SerializeField] private float detectionRadius = 5f; // Maximum detection radius
    [SerializeField] private float minimumDistance = 1.5f; // Minimum distance to move away
    [SerializeField] private Transform firePoint; // Point from where the fireball spawns
    [SerializeField] private GameObject fireballPrefab; // Fireball prefab
    [SerializeField] private float fireRate = 2f; // Time interval between fireball shots

    private GameObject player; // Reference to the player
    private bool movingRight = true; // Direction of movement
    private float nextFireTime = 0f; // Time until the dragon can fire again

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Assumes the player is tagged "Player"
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            ManageMovement(distanceToPlayer);
            FireAtPlayer();
        }
    }

    private void ManageMovement(float distanceToPlayer)
    {
        if (distanceToPlayer < minimumDistance)
        {
            // Move away from the player
            MoveDragon(false);
        }
        else
        {
            // Continue walking in the current direction (through the player)
            MoveDragon(true);
        }
    }

    private void MoveDragon(bool walkThrough)
    {
        float moveStep = moveSpeed * Time.deltaTime;

        if (walkThrough)
        {
            // Continue walking in the current direction
            transform.Translate((movingRight ? Vector2.right : Vector2.left) * moveStep);

            // If close to the bounds of detection, flip direction
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > detectionRadius)
            {
                movingRight = !movingRight; // Flip direction
            }
        }
        else
        {
            // Move away from the player
            if (player.transform.position.x > transform.position.x)
            {
                // Player is to the right, move left
                transform.Translate(Vector2.left * moveStep);
                movingRight = false;
            }
            else
            {
                // Player is to the left, move right
                transform.Translate(Vector2.right * moveStep);
                movingRight = true;
            }
        }

        // Flip the dragon's Y-axis based on direction
        transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
    }

    private void FireAtPlayer()
    {
        // Fire a fireball if the cooldown has elapsed
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            // Instantiate fireball and set its direction
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            Vector2 fireDirection = (player.transform.position - firePoint.position).normalized;

            // Assuming the fireball has a Rigidbody2D for movement
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            rb.linearVelocity = fireDirection * moveSpeed;

            Debug.Log("Fireball launched!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the detection and minimum distance radii in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minimumDistance);
    }

public class DragonAnimation : MonoBehaviour
{


private Animator animator;
private Rigidbody2D rb;
private Vector2 moveDirection;

void Start()
{
    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();

    if (animator == null)
    {
        Debug.LogError("Animator er ikke tilknyttet til gameobject");
    }
}

void Update()

{
    float moveX = Input.GetAxis("Horizontal");
    float moveY = Input.GetAxis("Vertical");

    moveDirection = new Vector2(moveX, moveY).normalized;

    Debug.Log("MoveDirection: " + moveDirection);

    if (moveDirection.magnitude > 0.1f)

    {
        animator.SetBool("isMoving", true);

    }
else 

    {
        animator.SetBool("isMoving", false);
    }
}

void FixedUpdate()

{
    rb.MovePosition(rb.position + moveDirection * 5f * Time.fixedDeltaTime);
}



}

}
