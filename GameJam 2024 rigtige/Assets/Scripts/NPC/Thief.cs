using System.IO.Pipes;
using UnityEngine;

public class Thief : Enemy
{
    public Animator anim;
    private GameObject player;
    float distance;
    float detectRadius;
    Rigidbody2D rb;
    float moveSpeed;
    float damageSpeed;
    bool hittingPlayer = false;
    float hitTimer = 2f;
    private bool movingRight = false;
    void Start()
    {

        moveSpeed = 100;
        health = 1;
        detectRadius = 5f;
        damageSpeed = 2f;
        player = GameObject.FindGameObjectWithTag("Player"); 
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public override void OnUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
       
        if(distance<detectRadius)
        {
            Vector2 direction = (player.transform.position - this.transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed*Time.deltaTime;
            anim.SetBool("isWalking",true);

            if ((transform.position.x > player.transform.position.x) && movingRight || (transform.position.x < player.transform.position.x) && !movingRight)
            {
                movingRight = !movingRight; // Flip direction
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        } else if(anim.GetBool("isWalking") == true)
        {
            anim.SetBool("isWalking", false);
            rb.linearVelocity = Vector2.zero;
        }
        if (hittingPlayer && hitTimer >= damageSpeed)
        {
            hitTimer = 0f;
            Debug.Log("gives damage");
         } 
        else if (hitTimer<damageSpeed)
        {
            hitTimer += Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            hittingPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            hittingPlayer = false;
        }
    }
}
