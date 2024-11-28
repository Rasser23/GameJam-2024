using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    public string targetScene; // Name of the scene to load
    public Vector3 newSpawnPosition; // Position in the new scene where the player will appear

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set the spawn position before loading the new scene
            SceneTransitionManager.Instance.SetSpawnPosition(newSpawnPosition);

            // Load the target scene
            SceneManager.LoadScene(targetScene);
        }
    }
}
