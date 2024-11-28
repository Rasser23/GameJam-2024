using UnityEngine;

public class DragonBehavior : Enemy
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
    public float health;
    private Animator animator; //Rasmine

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Assumes the player is tagged "Player"
        animator = GetComponent<Animator>(); // Rasmine
        health = 10f;
    }

    public override void OnUpdate()
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
            GameObject fireball = Instantiate(fireballPrefab, this.transform.position, Quaternion.identity);
            Vector2 fireDirection = (player.transform.position - this.transform.position).normalized;
        
            fireball.GetComponent<Fireball>().fireDirection = fireDirection;
           

             animator.SetTrigger("Fireball");
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
}
