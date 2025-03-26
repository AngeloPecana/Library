using UnityEngine;

public class SpriteBehavior : MonoBehaviour
{
    public bool isBadSprite = false;

    // Called when the sprite is clicked
    private void OnMouseDown()
    {
        Debug.Log("Sprite clicked!");

        if (isBadSprite)
        {
            Debug.Log("Bad sprite clicked! Minus points.");
            // Deduct points (assuming GameManager handles point logic)
            GameManager.instance.AddPoints(1);
        }
        else
        {
            Debug.Log("Good sprite clicked! Add points.");
            // Add points (assuming GameManager handles point logic)
            GameManager.instance.AddPoints(-1);
        }

        // Destroy the sprite immediately after being clicked
        Destroy(gameObject);
    }
}
