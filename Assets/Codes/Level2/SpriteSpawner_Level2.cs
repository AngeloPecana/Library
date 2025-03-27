using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner_Level2 : MonoBehaviour
{
    [Header("Sprite Prefabs")]
    public GameObject[] badSprites;  // Array for 2 different bad sprites
    public GameObject[] goodSprites; // Array for 2 different good sprites

    [Header("Spawn Settings")]
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 2f;
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
            // Spawn one bad sprite.
            SpawnSprite(GetRandomSprite(badSprites));
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

            // Spawn one good sprite.
            SpawnSprite(GetRandomSprite(goodSprites));
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    void SpawnSprite(GameObject spritePrefab)
    {
        // Get a random valid spawn position.
        Vector3 spawnPosition = GetSafeRandomPosition();

        // Instantiate the sprite at the spawn position.
        GameObject sprite = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);
        spawnedPositions.Add(spawnPosition);

        // Schedule removal of the spawn position after the sprite's lifetime.
        StartCoroutine(RemovePositionAfterDelay(spawnPosition, spriteLifetime));

        // Destroy the sprite after its lifetime expires.
        Destroy(sprite, spriteLifetime);
    }

    GameObject GetRandomSprite(GameObject[] spriteArray)
    {
        // Randomly pick a sprite from the provided array.
        return spriteArray[Random.Range(0, spriteArray.Length)];
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
        // Obtain the main camera and its position.
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main camera not found!");
            return Vector3.zero;
        }
        Vector3 camPos = cam.transform.position;

        // Calculate the camera's boundaries (assuming orthographic camera).
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Get sprite size for safe spawning (assuming sprites are similar in size).
        float spriteSizeX = badSprites[0].GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        float spriteSizeY = badSprites[0].GetComponent<SpriteRenderer>().bounds.size.y / 2f;

        // Define safe spawn boundaries to keep sprites fully on screen.
        float minX = camPos.x - camWidth + spriteSizeX;
        float maxX = camPos.x + camWidth - spriteSizeX;
        float minY = camPos.y - camHeight + spriteSizeY;
        float maxY = camPos.y + camHeight - spriteSizeY;

        // Return a random position within the safe area.
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector3(x, y, 0f);
    }

    IEnumerator RemovePositionAfterDelay(Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnedPositions.Remove(position);
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
