using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject EnemyMissile;
    public GameObject City;
    public Vector3[] citySpawnPoints;

    /// <summary>
    /// Set up the game here
    /// </summary>
    void Start()
    {
        //Setup the city
        spawnCities();
        spawnWave();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            spawnWave();

    }

    /// <summary>
    /// Spawn an enemy wave of missiles
    /// </summary>
    void spawnWave()
    {
        for(int i = 0; i < 10; i++)
        {
            Instantiate(EnemyMissile, new Vector3( (Random.Range(7, -7)), (Random.Range(20, 5)), 0), Quaternion.identity);
        }
    }

    /// <summary>
    /// Setup all the cities and their coordinates
    /// </summary>
    void spawnCities()
    {
        for(int i = 0; i < 6; i++)
        {
            Instantiate(City, citySpawnPoints[i], Quaternion.identity);
        }
    }
}
