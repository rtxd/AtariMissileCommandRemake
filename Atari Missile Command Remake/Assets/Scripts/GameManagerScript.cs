using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject EnemyMissile;
    public GameObject City;
    public Vector3[] citySpawnPoints;
    List<GameObject> Cities;

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

        //foreach(GameObject city in Cities.ToList())
        for(int i = 0; i < (Cities.Count-1); i++)
        {
            if(Cities[i] == null)
            {
                Cities.Remove(Cities[i]);
            }
        }

        if (Cities.Count == 0)
            GameOver();
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
        Cities = new List<GameObject>();
        for (int i = 0; i < 6; i++)
        {
            var newCity = Instantiate(City, citySpawnPoints[i], Quaternion.identity);
            Cities.Add(newCity);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!!");
    }
}
