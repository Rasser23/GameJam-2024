using System.IO.Pipes;
using UnityEngine;

public class LamafollowMouse : MonoBehaviour
{
   
    float distance;
    float detectRadius;
    Rigidbody2D rb;
    float moveSpeed;
    
   
    void Start()
    {
        moveSpeed = 700;
       
        detectRadius = 10f;
       
       
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(transform.position, mouseWorldPos);

        if (distance > 1)
        {
            Vector2 direction = (mouseWorldPos - this.transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        
    }
    
}
