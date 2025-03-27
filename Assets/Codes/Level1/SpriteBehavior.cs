using UnityEngine;

/// <summary>
/// Handles sprite interactions. Clicking a sprite will update points based on whether it's good or bad.
/// Prevents interactions if the game is paused.
/// </summary>
public class SpriteBehavior : MonoBehaviour
{
    public bool isBadSprite = false; // Determines whether the sprite is "bad" or "good"

    private void OnMouseDown()
    {
        // Prevent interaction if the game is paused or inactive.
        if (GameManager.instance == null || !GameManager.instance.IsGameActive)
            return;

        Debug.Log("Sprite clicked!");

        if (isBadSprite)
        {
            Debug.Log("Bad sprite clicked! Minus points.");
            // Deduct points for a bad sprite.
            GameManager.instance.AddPoints(1);
        }
        else
        {
            Debug.Log("Good sprite clicked! Add points.");
            // Add points for a good sprite.
            GameManager.instance.AddPoints(-1);
        }

        // Destroy the sprite immediately after it has been clicked.
        Destroy(gameObject);
    }
}
