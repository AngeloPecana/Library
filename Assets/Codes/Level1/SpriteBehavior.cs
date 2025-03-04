using UnityEngine;

public class SpriteBehavior : MonoBehaviour
{
    public bool isBadSprite = false; // Whether this sprite is bad or not

    // This method is called when the sprite is clicked
    private void OnMouseDown()
    {
        Debug.Log("Sprite clicked!");

        if (isBadSprite)
        {
            Debug.Log("Bad sprite clicked! Minus points.");
            // Deduct points logic here
            GameManager.instance.AddPoints(1); // Subtract points for bad sprite
        }
        else
        {
            Debug.Log("Good sprite clicked! Add points.");
            // Add points logic here
            GameManager.instance.AddPoints(-1); // Add points for good sprite
        }

        // Destroy the sprite after it has been clicked
        Destroy(gameObject);
    }
}
