using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public Vector3 offset = new Vector3(0, 0, -10); // Offset for the camera
    public float smoothSpeed = 0.125f; // Smoothness of camera movement
    private bool followPlayer = false; // Prevent camera movement until ready

    private void Start()
    {
        FindPlayer(); // Automatically find the player when the scene loads
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Ensure the camera persists between scenes
    }

    private void LateUpdate()
    {
        if (followPlayer && player != null)
        {
            // Smoothly follow the player
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            followPlayer = true; // Allow the camera to start following the player
        }
        else
        {
            Debug.LogError("Player object not found in the scene!");
        }
    }

    public void StopFollowing()
    {
        followPlayer = false; // Temporarily stop camera movement
    }
}
