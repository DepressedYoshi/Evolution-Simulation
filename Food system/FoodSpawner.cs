using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public int poolSize = 200 ;
    private List<GameObject> Food;
    
    private Terrain terrain;
    private Vector3 terrainSize;
    private Vector3 terrainPosition;


    void Start()
    {
        terrain = Terrain.activeTerrain;
        terrainSize = terrain.terrainData.size;
        terrainPosition = terrain.transform.position;
        
        // Initialize the cube pool
        InitializeFoodPool();
        // Spawn cubes at random positions
        SpawnCubes();
    }

    void InitializeFoodPool()
    {
        Food = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject food = Instantiate(foodPrefab);
            food.SetActive(false);
            Food.Add(food);
        }
    }

    void SpawnCubes()
    {
        foreach (GameObject food in Food)
        {
            float x = Random.Range(terrainPosition.x, terrainPosition.x + terrainSize.x);
            float z = Random.Range(terrainPosition.z, terrainPosition.z + terrainSize.z);
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPosition.y;
            Vector3 spawnPosition = new Vector3(x, y, z);
            
            food.transform.position = spawnPosition;
            food.SetActive(true);
        }
    }
}
