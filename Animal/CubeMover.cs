    using System.Collections;
using UnityEngine;

public class RandomCubeMover : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float minRestDuration = 1f;
    public float maxRestDuration = 3f;
    public float minMoveDuration = 2f;
    public float maxMoveDuration = 5f;
    private Terrain terrain;
    private Vector3 terrainSize;
    private Vector3 terrainPosition;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;
        terrainSize = terrain.terrainData.size;
        terrainPosition = terrain.transform.position;
    }

    private void Start()
    {
        StartCoroutine(CubeBehaviorRoutine());
    }

    private IEnumerator CubeBehaviorRoutine()
    {
        Vector3 direction = Vector3.zero;

        while (true)
        {
            bool willMove = Random.Range(0, 4) > 0; // 75% chance to move
            if (willMove)
            {
                direction = GetRandomDirection();
                float moveDuration = Random.Range(minMoveDuration, maxMoveDuration);
                float timer = 0;

                while (timer < moveDuration)
                {
                    Vector3 nextPosition = transform.position + direction * moveSpeed * Time.deltaTime;
                    if (IsWithinTerrainBounds(nextPosition))
                    {
                        ApplyGravity(); // Make sure the cube is always on the terrain
                        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        direction = GetRandomDirection(); // Change direction if next position is out of bounds
                    }
                    timer += Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(minRestDuration, maxRestDuration));
            }
        }
    }

    private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    private bool IsWithinTerrainBounds(Vector3 position)
    {
        return position.x >= terrainPosition.x && position.x <= terrainPosition.x + terrainSize.x &&
               position.z >= terrainPosition.z && position.z <= terrainPosition.z + terrainSize.z;
    }

    private void ApplyGravity()
{
    Vector3 currentPosition = transform.position;
    float terrainHeight = terrain.SampleHeight(transform.position) + terrainPosition.y;
    // Add an offset equal to half the cube's height to ensure it's fully above the terrain
    currentPosition.y = terrainHeight + 1f; // Assuming the cube is 2 meters tall
    transform.position = currentPosition;
}

}

