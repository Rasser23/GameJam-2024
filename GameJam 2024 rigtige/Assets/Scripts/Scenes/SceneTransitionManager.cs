using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public Vector3 spawnPosition; // Stores the position where the player should appear

    private void Awake()
    {
        // Singleton pattern to ensure one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    public void SetSpawnPosition(Vector3 position)
    {
        spawnPosition = position; // Update the spawn position for the next scene
    }
}
