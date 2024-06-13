using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int poolSize = 100;
    private List<GameObject> cubes;
    
    private Terrain terrain;
    private Vector3 terrainSize;
    private Vector3 terrainPosition;
    private float cubeHeightOffset = 1f; // Half the cube's height to adjust spawn position


    void Start()
    {
        terrain = Terrain.activeTerrain;
        terrainSize = terrain.terrainData.size;
        terrainPosition = terrain.transform.position;
        
        // Initialize the cube pool
        InitializeCubePool();
        // Spawn cubes at random positions
        SpawnCubes();
    }


    void InitializeCubePool()
    {
        cubes = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            cube.SetActive(false);
            cubes.Add(cube);
        }
    }

    void SpawnCubes()
    {
        foreach (GameObject cube in cubes)
        {
            float x = Random.Range(terrainPosition.x, terrainPosition.x + terrainSize.x);
            float z = Random.Range(terrainPosition.z, terrainPosition.z + terrainSize.z);
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPosition.y + cubeHeightOffset;
            Vector3 spawnPosition = new Vector3(x, y, z);
            
            cube.transform.position = spawnPosition;
            cube.SetActive(true);
        }
    }
}
