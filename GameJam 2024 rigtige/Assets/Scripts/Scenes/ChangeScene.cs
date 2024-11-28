using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string targetScene; // The name of the target scene to load
    public Vector3 newSpawnPosition; // Spawn position in the target scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the player triggered the exit
        {
            // Save the new spawn position for the next scene
            if (SceneTransitionManager.Instance != null)
            {
                SceneTransitionManager.Instance.SetSpawnPosition(newSpawnPosition);
            }
            else
            {
                Debug.LogError("SceneTransitionManager instance is missing!");
            }

            // Load the target scene
            if (!string.IsNullOrEmpty(targetScene))
            {
                SceneManager.LoadScene(targetScene);
            }
            else
            {
                Debug.LogError("Target scene is not set!");
            }
        }
    }
}
