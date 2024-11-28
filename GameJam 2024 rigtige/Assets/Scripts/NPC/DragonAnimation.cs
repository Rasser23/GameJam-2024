using UnityEngine;

public class DragonAnimation : MonoBehaviour

{
   
private Animator animator;
private Rigidbody2D rb;
private Vector2 moveDirection;

private float stopTreshold = 0.05f;
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

   // Debug.Log("MoveDirection: " + moveDirection);

    if (moveDirection.magnitude > stopTreshold)

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
