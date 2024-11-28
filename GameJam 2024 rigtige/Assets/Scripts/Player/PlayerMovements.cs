using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D myBody;
    private Animator myAnimator;
    public Sword sword;

    [SerializeField] private float speed = 2f; // Movement speed

    private Vector3 startingPosition; // Player's starting position
    public static PlayerMovements Instance; // Singleton instance

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBody.interpolation = RigidbodyInterpolation2D.Interpolate; // Smooth movement
        myBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Prevent tunneling
        DontDestroyOnLoad(gameObject); // Prevents this object from being destroyed when loading a new scene

        // Save the player's starting position
        startingPosition = transform.position;


        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist the player across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate player objects
        }
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>().normalized; // Normalize movement for consistent speed

        if (movement.magnitude > 0)
        {
            myAnimator.SetFloat("X", movement.x);
            myAnimator.SetFloat("Y", movement.y);
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }
    }

    private void FixedUpdate()
    {
        myBody.linearVelocity = movement * speed; // Apply movement
    }


    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            sword.GiveDamage();
        }
    }


    public void ResetPosition()
    {
        // Reset the player's position and stop movement
        transform.position = startingPosition;
        movement = Vector2.zero;
        myAnimator.SetBool("isWalking", false); // Reset animation state
    }
}
