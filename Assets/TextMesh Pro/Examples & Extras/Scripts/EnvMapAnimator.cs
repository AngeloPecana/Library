using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// Animates the environment mapping of a TextMeshPro component by updating its material matrix.
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class EnvMapAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [Tooltip("Rotation speeds (degrees per second) for the environment map animation.")]
    public Vector3 rotationSpeeds;

    private TMP_Text tmpText;
    private Material material;

    /// <summary>
    /// Initializes references and caches the shared font material.
    /// </summary>
    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        if (tmpText == null)
        {
            Debug.LogError("EnvMapAnimator: TMP_Text component not found on the GameObject.");
            enabled = false;
            return;
        }

        material = tmpText.fontSharedMaterial;
        if (material == null)
        {
            Debug.LogError("EnvMapAnimator: Could not retrieve the shared material from the TMP_Text component.");
            enabled = false;
        }
    }

    /// <summary>
    /// Continuously updates the environment map matrix based on the current time and rotation speeds.
    /// </summary>
    IEnumerator Start()
    {
        // Create a reusable matrix instance.
        Matrix4x4 envMatrix = new Matrix4x4();

        while (true)
        {
            // Compute the rotation based on elapsed time and rotation speeds.
            Quaternion rotation = Quaternion.Euler(Time.time * rotationSpeeds.x,
                                                     Time.time * rotationSpeeds.y,
                                                     Time.time * rotationSpeeds.z);

            // Set the transformation matrix with no translation and uniform scaling.
            envMatrix.SetTRS(Vector3.zero, rotation, Vector3.one);

            // Update the shader property responsible for environment mapping.
            material.SetMatrix("_EnvMatrix", envMatrix);

            yield return null;
        }
    }
}
