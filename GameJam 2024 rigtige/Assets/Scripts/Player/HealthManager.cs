using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Sprite[] heartSprites; // Array of heart sprites
    private Image heartImage;
    public int currentHealth;
    public int maxHealth = 6;

    [Header("Game Over UI")]
    public GameObject gameOverScreen; // Reference to the Game Over screen
    public Button resetButton;        // Reference to the reset button

    private PlayerMovements playerMovements; // Reference to the player's movement script

    void Start()
    {
        // Get the Image component attached to this GameObject
        heartImage = GetComponent<Image>();

        // Set the initial health to maxHealth and update the heart sprite
        currentHealth = maxHealth;

        // Ensure the first heart sprite is displayed initially
        if (heartSprites.Length > 0)
        {
            heartImage.sprite = heartSprites[0];
        }

        // Hide the Game Over screen and reset button initially
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        if (resetButton != null)
        {
            resetButton.gameObject.SetActive(false);
            resetButton.onClick.AddListener(RestartGame);
        }

        // Find the player's movement script
        playerMovements = FindObjectOfType<PlayerMovements>();
        if (playerMovements == null)
        {
            Debug.LogError("PlayerMovements script not found in the scene!");
        }
    }

    public void TakeDamage(int damage)
    {
        // Reduce health but not below 0
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        // Update the heart sprite
        UpdateHeartSprite();

        // Check if health is 0
        if (currentHealth == 0)
        {
            GameOver();
        }
    }

    public void UpdateHeartSprite()
    {
        // Update UI sprite logic
        int spriteIndex = maxHealth - currentHealth;
        spriteIndex = Mathf.Clamp(spriteIndex, 0, heartSprites.Length - 1);
        heartImage.sprite = heartSprites[spriteIndex];
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");

        // Show the Game Over screen and reset button
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        if (resetButton != null)
        {
            resetButton.gameObject.SetActive(true);
        }

        // Stop the game
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        Debug.Log("Restarting Game...");

        // Hide Game Over screen and reset button
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        if (resetButton != null)
        {
            resetButton.gameObject.SetActive(false);
        }

        // Reset time scale
        Time.timeScale = 1f;

        // Reset player position and state
        if (playerMovements != null)
        {
            playerMovements.ResetPosition(); // Ensure this is called
        }

        // Reset health
        currentHealth = maxHealth;
        UpdateHeartSprite();

        // Load the desired starting scene 
        string startingScene = "SampleScene"; 
        SceneManager.LoadScene(startingScene);

        Debug.Log("Game Reset Complete");
    }

}
