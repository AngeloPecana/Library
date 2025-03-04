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
            // Choose a random sprite (bad or good)
            GameObject spritePrefab = Random.Range(0f, 1f) < 0.5f ? badSpritePrefab : goodSpritePrefab;

            // Get a random valid spawn position
            Vector3 spawnPosition = GetSafeRandomPosition();

            // Instantiate sprite
            GameObject sprite = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);

            // Track the new sprite's position
            spawnedPositions.Add(spawnPosition);

            // Destroy after its lifetime
            Destroy(sprite, spriteLifetime);

            // Wait before spawning the next one
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    Vector3 GetSafeRandomPosition()
    {
        Vector3 randomPosition;
        bool isValidPosition;

        do
        {
            randomPosition = GetRandomPosition();
            isValidPosition = true;

            // Check if the position is too close to any already spawned sprite
            foreach (Vector3 pos in spawnedPositions)
            {
                if (Vector3.Distance(pos, randomPosition) < minDistanceBetweenSprites)
                {
                    isValidPosition = false;
                    break;
                }
            }

        } while (!isValidPosition); 

        return randomPosition;
    }

    Vector3 GetRandomPosition()
{

    // Get main camera and its position
    Camera cam = Camera.main;
    Vector3 camPos = cam.transform.position;

    // Get camera boundaries in world space
    float camHeight = cam.orthographicSize; // Half of height
    float camWidth = camHeight * cam.aspect; // Half of width

    // Get sprite size to ensure full visibility
    float spriteSizeX = badSpritePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
    float spriteSizeY = badSpritePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 2f;

    // Define safe spawn boundaries (subtract sprite size to keep them fully on screen)
    float minX = camPos.x - camWidth + spriteSizeX;
    float maxX = camPos.x + camWidth - spriteSizeX;
    float minY = camPos.y - camHeight + spriteSizeY;
    float maxY = camPos.y + camHeight - spriteSizeY;

    // Generate a random position within the safe area
    float x = Random.Range(minX, maxX);
    float y = Random.Range(minY, maxY);

    return new Vector3(x, y, 0f);
}


    public void StopSpawning()
    {
        isSpawning = false;
    }
}
