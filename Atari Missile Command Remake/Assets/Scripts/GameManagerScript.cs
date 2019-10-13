using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject EnemyMissile;
    public GameObject City;
    public GameObject GameOverSprite;
    public GameObject WaveOneSprite;
    public GameObject WaveTwoSprite;
    public GameObject WaveThreeSprite;
    public bool mainMenu;

    public AudioClip GameOverAudio;
    public AudioClip BackgroundMusic;
    public AudioClip GameBootupAudio;
    public AudioClip WaveOneAudio;
    public AudioClip WaveTwoAudio;
    public AudioClip WaveThreeAudio;
    

    public float musicVolume;
    public Vector3[] citySpawnPoints;
    List<GameObject> Cities;
    bool gameOver = false;
    
    
    void StartWaveOne()
    {

    }

    void StartWaveTwo()
    {

    }

    void StartWaveThree()
    {

    }

    /// <summary>
    /// Set up the game here
    /// </summary>
    void Start()
    {
        
        //Setup the city
        spawnCities();
        spawnWave();
        //Play background music
        AudioSource.PlayClipAtPoint(BackgroundMusic, new Vector3(0, 0, -18), musicVolume);
        AudioSource.PlayClipAtPoint(GameBootupAudio, new Vector3(0, 0, -18), musicVolume);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            spawnWave();

        //foreach(GameObject city in Cities.ToList())
        for(int i = 0; i < (Cities.Count); i++)
        {
            if(Cities[i] == null)
            {
                Cities.Remove(Cities[i]);
            }
        }

        if(gameOver == false)
        {
            if (Cities.Count == 0)
            {
                GameOver();
                gameOver = true;
            }
        }
    }

    /// <summary>
    /// Spawn an enemy wave of missiles
    /// </summary>
    void spawnWave()
    {
        for(int i = 0; i < 10; i++)
        {
            if(mainMenu)
            {
                EnemyMissile.GetComponent<EnemyMissileScript>().mainMenu = true;
            }
            
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
            if (mainMenu)
            {
                newCity.GetComponent<CityScript>().mainMenu = true;
            }
            Cities.Add(newCity);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!!");
        Instantiate(GameOverSprite);
        AudioSource.PlayClipAtPoint(GameOverAudio, new Vector3(0, 0, -18), 1f);
    }
}
