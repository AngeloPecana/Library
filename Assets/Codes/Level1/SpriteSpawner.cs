using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    [Header("Sprite Prefabs")]
    public GameObject badSpritePrefab;
    public GameObject goodSpritePrefab;

    [Header("Spawn Settings")]
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    public float spriteLifetime = 3f;
    public float minDistanceBetweenSprites = 3f;

    private bool isSpawning = true;
    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(SpawnSprites());
    }

    IEnumerator SpawnSprites()
    {
        while (isSpawning)
        {
            // Prevent spawning if the game is paused.
            if (GameManager.instance != null && !GameManager.instance.IsGameActive)
            {
                yield return null;
                continue;
            }

            // Randomly choose between bad and good sprite.
            GameObject spritePrefab = Random.Range(0f, 1f) < 0.5f ? badSpritePrefab : goodSpritePrefab;

            // Determine a safe spawn position.
            Vector3 spawnPosition = GetSafeRandomPosition();

            // Instantiate the sprite and track its position.
            GameObject sprite = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);
            spawnedPositions.Add(spawnPosition);

            // Schedule removal of the spawn position from the list after the sprite's lifetime.
            StartCoroutine(RemovePositionAfterDelay(spawnPosition, spriteLifetime));

            // Destroy the sprite after its lifetime expires.
            Destroy(sprite, spriteLifetime);

            // Wait for a random interval before spawning the next sprite.
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    IEnumerator RemovePositionAfterDelay(Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnedPositions.Remove(position);
    }

    Vector3 GetSafeRandomPosition()
    {
        const int maxAttempts = 100;
        int attempts = 0;
        Vector3 randomPosition;
        bool isValidPosition;

        do
        {
            randomPosition = GetRandomPosition();
            isValidPosition = true;

            // Ensure the new position is not too close to any already spawned sprite.
            foreach (Vector3 pos in spawnedPositions)
            {
                if (Vector3.Distance(pos, randomPosition) < minDistanceBetweenSprites)
                {
                    isValidPosition = false;
                    break;
                }
            }
            attempts++;
        } while (!isValidPosition && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Failed to find a safe spawn position after maximum attempts. Using the last generated position.");
        }

        return randomPosition;
    }

    Vector3 GetRandomPosition()
    {
        // Obtain the main camera.
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main camera not found!");
            return Vector3.zero;
        }
        Vector3 camPos = cam.transform.position;

        // Calculate the camera's boundaries (assuming an orthographic camera).
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Calculate half-size of the sprite (assumed same size for both sprite types).
        float spriteSizeX = badSpritePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        float spriteSizeY = badSpritePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 2f;

        // Define safe spawn boundaries to keep sprites fully on screen.
        float minX = camPos.x - camWidth + spriteSizeX;
        float maxX = camPos.x + camWidth - spriteSizeX;
        float minY = camPos.y - camHeight + spriteSizeY;
        float maxY = camPos.y + camHeight - spriteSizeY;

        // Generate a random position within the defined safe area.
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector3(x, y, 0f);
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
