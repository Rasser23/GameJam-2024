using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    void OnGUI()
    {
        // Create a button that loads the game
        if (GUI.Button(new Rect(1920/2 - 300, 1080/2, 500, 100), "Play"))
        {
            Debug.Log("Play Button Pressed");
            // Load your game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }

        // Create a button that quits the game
        if (GUI.Button(new Rect(1920 / 2 - 300, (1080 / 2)+200, 500, 100), "Quit"))
        {
            Debug.Log("Quit Button Pressed");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // For Editor
            #else
                Application.Quit(); // For Build
            #endif
        }
    }
}
