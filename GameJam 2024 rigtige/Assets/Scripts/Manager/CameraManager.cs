using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the camera between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Ensure the camera is always following the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GetComponent<CameraFollowScript>().player = player.transform;
        }
    }
}
