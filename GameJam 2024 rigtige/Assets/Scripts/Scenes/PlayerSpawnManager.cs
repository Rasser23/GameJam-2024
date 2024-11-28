using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab; // Assign the player prefab in the Inspector

    private void Start()
    {
        // Get the stored spawn position
        Vector3 spawnPosition = SceneTransitionManager.Instance.spawnPosition;

        // Check if a player already exists
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");

        if (existingPlayer == null)
        {
            // Spawn a new player if none exists
            Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // Move the existing player to the spawn position
            existingPlayer.transform.position = spawnPosition;
        }

        // Notify the camera to temporarily stop following
        CameraFollowScript cameraScript = Camera.main.GetComponent<CameraFollowScript>();
        if (cameraScript != null)
        {
            cameraScript.StopFollowing();

            // Re-enable camera following after a slight delay
            StartCoroutine(EnableCameraFollow(cameraScript));
        }
        else
        {
            Debug.LogError("CameraFollowScript not found on the main camera!");
        }
    }

    private System.Collections.IEnumerator EnableCameraFollow(CameraFollowScript cameraScript)
    {
        yield return new WaitForEndOfFrame(); // Allow one frame for the player to settle
        cameraScript.FindPlayer(); // Re-link the camera to the player
    }
}
