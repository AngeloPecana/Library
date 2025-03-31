using UnityEngine;

/// <summary>
/// Attach this script to a GameObject in your level scene (for example, an empty GameObject named "LevelIdentifier").
/// Set the levelNumber manually in the Inspector (e.g., 1 for Level 1, 2 for Level 2, etc.).
/// </summary>
public class LevelIdentifier : MonoBehaviour
{
    [Tooltip("Set the intended level number (e.g., 1 for Level 1, 2 for Level 2, etc.).")]
    public int levelNumber = 1;
}
