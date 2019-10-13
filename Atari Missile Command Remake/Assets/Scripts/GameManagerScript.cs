using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    GameObject MissileControllerRight;
    GameObject MissileControllerLeft;
    GameObject MissileControllerCenter;
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

    [SerializeField]
    float musicVolume = 0;
    [SerializeField]
    float voiceVolume = 0;
    public Vector3[] citySpawnPoints;
    List<GameObject> Cities;
    bool gameOver = false;
    

    /// <summary>
    /// Set up the game here
    /// </summary>
    void Start()
    {
        MissileControllerRight = GameObject.Find("MissileControllerRight");
        MissileControllerLeft = GameObject.Find("MissileControllerLeft");
        MissileControllerCenter = GameObject.Find("MissileControllerCenter");
        spawnCities();
        //Setup the city
        StartCoroutine(StartWaveOne());
        //Invoke("StartWaveTwo", 5);
        //Invoke("StartWaveThree", 5);
        //Play background music
        AudioSource.PlayClipAtPoint(BackgroundMusic, new Vector3(0, 0, -18), musicVolume);
        
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    IEnumerator StartWaveOne()
    {
        AudioSource.PlayClipAtPoint(WaveOneAudio, new Vector3(0, 0, -18), voiceVolume);
        var waveOneSprite = Instantiate(WaveOneSprite);
        yield return new WaitForSeconds(2);
        Destroy(waveOneSprite);
        spawnWave();
        yield return new WaitForSeconds(5);
        spawnWave();
        yield return new WaitForSeconds(13);
        StartCoroutine(StartWaveTwo());
    }

    IEnumerator StartWaveTwo()
    {
        AudioSource.PlayClipAtPoint(WaveTwoAudio, new Vector3(0, 0, -18), voiceVolume);
        var waveTwoSprite = Instantiate(WaveTwoSprite);
        yield return new WaitForSeconds(2);
        Destroy(waveTwoSprite);
        spawnWave();
        spawnWave();
        yield return new WaitForSeconds(8);
        spawnWave();
        spawnWave();
        yield return new WaitForSeconds(5);
        spawnWave();
        yield return new WaitForSeconds(10);
        StartCoroutine(StartWaveTwo());
    }

    IEnumerator StartWaveThree()
    {
        AudioSource.PlayClipAtPoint(WaveThreeAudio, new Vector3(0, 0, -18), voiceVolume);
        var waveThreeSprite = Instantiate(WaveThreeSprite);
        yield return new WaitForSeconds(2);
        Destroy(waveThreeSprite);
        spawnWave();
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
        Instantiate(GameOverSprite);
        AudioSource.PlayClipAtPoint(GameOverAudio, new Vector3(0, 0, -18), 1f);
    }

    void Reset()
    {
        MissileControllerRight.GetComponent<MissileControllerScript>().Reset();
        MissileControllerLeft.GetComponent<MissileControllerScript>().Reset();
        MissileControllerCenter.GetComponent<MissileControllerScript>().Reset();
        ResetCities();
    }

    void ResetCities()
    {
        for (int i = 0; i < Cities.Count; i++)
        {
            Destroy(Cities[i]);
        }

        spawnCities();
    }
}
