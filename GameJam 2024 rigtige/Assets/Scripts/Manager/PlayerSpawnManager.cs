using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    private void Start()
    {
        // Get the stored spawn position
        Vector3 spawnPosition = SceneTransitionManager.Instance.spawnPosition;

        // Find or spawn the player and position them
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = spawnPosition;

            // Notify the camera to temporarily stop following
            CameraFollowScript cameraScript = Camera.main.GetComponent<CameraFollowScript>();
            cameraScript.StopFollowing();

            // Re-enable camera following after a slight delay
            StartCoroutine(EnableCameraFollow(cameraScript));
        }
        else
        {
            Debug.LogError("Player object not found in the scene!");
        }
    }

    private System.Collections.IEnumerator EnableCameraFollow(CameraFollowScript cameraScript)
    {
        yield return new WaitForEndOfFrame(); // Allow one frame for the player to settle
        cameraScript.FindPlayer(); // Re-link the camera to the player
    }
}
