using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Sprite[] heartSprites; // Array of heart sprites (each representing a health stage)
    private Image heartImage;     // UI Image component for the heart
    private int currentHealth;    // Tracks the player's current health
    public int maxHealth = 6;     // Maximum health (6 hearts in this case)

    [Header("Game Over UI")]
    public GameObject gameOverScreen; // Reference to the Game Over screen
    public Button resetButton;        // Reference to the reset button

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

        // Hide the Game Over screen initially
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Add listener to reset button
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(RestartGame);
        }
    }

    public void TakeDamage(int damage)
    {
        // Reduce health but not below 0
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        // Update the heart sprite
        UpdateHeartSprite();

        // Check if health is 0
        if (currentHealth == 0)
        {
            GameOver();
        }
    }

    public void Heal(int healAmount)
    {
        // Increase health but not above maxHealth
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        Debug.Log($"Player healed {healAmount}. Current health: {currentHealth}");

        // Update the heart sprite
        UpdateHeartSprite();
    }

    private void UpdateHeartSprite()
    {
        // Calculate the sprite index based on the current health
        int spriteIndex = maxHealth - currentHealth;

        // Clamp the sprite index to ensure it stays within bounds
        spriteIndex = Mathf.Clamp(spriteIndex, 0, heartSprites.Length - 1);

        // Update the heart sprite
        heartImage.sprite = heartSprites[spriteIndex];

        Debug.Log($"Heart sprite updated. Current health: {currentHealth}, Sprite index: {spriteIndex}");
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");

        // Show the Game Over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Stop player movement or any other gameplay mechanics here if necessary
        Time.timeScale = 0f; // Pause the game
    }

    private void RestartGame()
    {
        // Restart the game by reloading the current scene
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
