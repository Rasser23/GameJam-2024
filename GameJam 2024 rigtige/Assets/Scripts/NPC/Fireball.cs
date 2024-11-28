using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fireball : MonoBehaviour
{
    public Vector2 fireDirection;
    Rigidbody2D rigidbody;
    float moveSpeed;
    float timeExistet = 0f;
    void Start()
    {
        moveSpeed = 200f;
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
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
        if(other.tag == "Player") 
        {

            PlayerMovements pM = other.gameObject.GetComponent<PlayerMovements>();
            if (pM != null)
            {
                //pM.TakeDamage();... something
                DestroyObj();
            }
        }
    }
    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
