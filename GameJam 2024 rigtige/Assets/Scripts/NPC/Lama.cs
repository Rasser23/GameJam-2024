using System.IO.Pipes;
using UnityEngine;

public class Lama : Enemy
{
    private GameObject player;
    float distance;
    float detectRadius;
    Rigidbody2D rb;
    float moveSpeed;
    float damageSpeed;
    bool hittingPlayer = false;
    float hitTimer = 2f;
     public override void OnStart()
    {
        moveSpeed = 300;
        health = 1;
        detectRadius = 10f;
        damageSpeed = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public override void OnUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < detectRadius)
        {
            Vector2 direction = (player.transform.position - this.transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        if (hittingPlayer && hitTimer >= damageSpeed)
        {
            hitTimer = 0f;
            GiveDamage(1);
            Debug.Log("gives damage");
        }
        else if (hitTimer < damageSpeed)
        {
            hitTimer += Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
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
