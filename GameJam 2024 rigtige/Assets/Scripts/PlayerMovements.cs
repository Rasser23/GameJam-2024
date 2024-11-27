using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;


public class PlayerMovements : MonoBehaviour
{
    private Vector2 movement; 
    private Rigidbody2D myBody;

    private Animator myAnimator;
    
    [SerializeField] private int speed = 5; // Hastigheden som myBody skal bevæge sig i 
    
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.x != 0 || movement.y !=0) {

            myAnimator.SetFloat("X", movement.x);
            myAnimator.SetFloat("Y", movement.y);

            myAnimator.SetBool("isWalking", true);
        } 
        else 
        {
            myAnimator.SetBool("isWalking", false);
        }


    }

    private void Update()
    {
        myBody.linearVelocity = movement * speed * Time.deltaTime * 300;
    }
    
    

}
