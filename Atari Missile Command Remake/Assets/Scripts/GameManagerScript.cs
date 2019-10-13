using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    //Missile controllers, these control how much ammo is left
    GameObject MissileControllerRight;
    GameObject MissileControllerLeft;
    GameObject MissileControllerCenter;

    public GameObject EnemyMissile;
    public GameObject City;

    public GameObject YouWinSprite;
    public GameObject GameOverSprite;
    public GameObject WaveOneSprite;
    public GameObject WaveTwoSprite;
    public GameObject WaveThreeSprite;
    public Text ScoreUI;

    //Determine if mainMenu rules apply... we do this so the main
    //menu screen looks like the game, but doesn't actually play
    public bool mainMenu;

    //Audio
    public AudioClip GameOverAudio;
    public AudioClip BackgroundMusic;
    public AudioClip GameBootupAudio;
    public AudioClip WaveOneAudio;
    public AudioClip WaveTwoAudio;
    public AudioClip WaveThreeAudio;
    public AudioClip YouWinAudio;
    public AudioClip GameWinAudio;

    //Volume controls
    [SerializeField]
    float musicVolume = 0;
    [SerializeField]
    float voiceVolume = 0;
    
    //City controls
    public Vector3[] citySpawnPoints;
    List<GameObject> Cities;

    //Game states
    bool gameOver = false;
    bool gameWon = false;
    int score = 0;

    /// <summary>
    /// Set up the game here
    /// </summary>
    void Start()
    {
        //If it's not the main menu then don't display the score
        if(!mainMenu)
        {
            ScoreUI = GameObject.Find("Text").GetComponent<Text>();
        }
        
        MissileControllerRight = GameObject.Find("MissileControllerRight");
        MissileControllerLeft = GameObject.Find("MissileControllerLeft");
        MissileControllerCenter = GameObject.Find("MissileControllerCenter");

        //Setup the city
        spawnCities();

        //If it's not the main menu then start the game, starts with wave one
        if(!mainMenu)
            StartCoroutine(StartWaveOne());
        else
        {
            //if it is menu then just spawn a wave that doesn't do anything
            spawnWave();
        }
        //Play background music
        AudioSource.PlayClipAtPoint(BackgroundMusic, new Vector3(0, 0, -18), musicVolume);
        
    }

    // Update is called once per frame
    void Update()
    {
        //This is here in case you need to cheat for testing purposes
        //if (Input.GetKeyDown(KeyCode.Space))
        //    spawnWave();

        //Cycle through the cities list and remove any that have been destroyed
        for(int i = 0; i < (Cities.Count); i++)
        {
            if(Cities[i] == null)
            {
                Cities.Remove(Cities[i]);
            }
        }

        //If you have no cities left then end the game
        if(gameOver == false)
        {
            if (Cities.Count == 0)
            {
                StartCoroutine(GameOver());
                gameOver = true;
            }
        }

        //Check if you win game
        if (gameWon == true)
        {
            StartCoroutine(winGame());
            gameWon = false;
        }

        //Small cheat for testing purposes
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        //Update the score
        ScoreUI.text = "Your score: " + score;
    }

    /// <summary>
    /// Initiate first wave
    /// </summary>
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
        calculateScore();
        StartCoroutine(StartWaveTwo());
    }

    /// <summary>
    /// Start second wave
    /// </summary>
    IEnumerator StartWaveTwo()
    {
        Reset();
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
        yield return new WaitForSeconds(13);
        calculateScore();
        StartCoroutine(StartWaveThree());
    }

    /// <summary>
    /// Start wave three
    /// </summary>
    IEnumerator StartWaveThree()
    {
        Reset();
        AudioSource.PlayClipAtPoint(WaveThreeAudio, new Vector3(0, 0, -18), voiceVolume);
        var waveThreeSprite = Instantiate(WaveThreeSprite);
        yield return new WaitForSeconds(2);
        Destroy(waveThreeSprite);
        spawnWave();
        spawnWave();
        yield return new WaitForSeconds(8);
        spawnWave();
        yield return new WaitForSeconds(5);
        spawnWave();
        yield return new WaitForSeconds(5);
        spawnWave();
        yield return new WaitForSeconds(13);
        calculateScore();
        gameWon = true;
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

    IEnumerator GameOver()
    {
        Instantiate(GameOverSprite);
        AudioSource.PlayClipAtPoint(GameOverAudio, new Vector3(0, 0, -18), 1f);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void Reset()
    {
        //Reset all ammunition
        MissileControllerRight.GetComponent<MissileControllerScript>().Reset();
        MissileControllerLeft.GetComponent<MissileControllerScript>().Reset();
        MissileControllerCenter.GetComponent<MissileControllerScript>().Reset();
        //Reset cities
        ResetCities();
    }

    /// <summary>
    /// Restores all cities back to original state
    /// </summary>
    void ResetCities()
    {
        for (int i = 0; i < Cities.Count; i++)
        {
            Destroy(Cities[i]);
        }

        spawnCities();
    }

    /// <summary>
    /// Wins game, goes to main menu after 10 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator winGame()
    {
        var youWinSprite = Instantiate(YouWinSprite);
        AudioSource.PlayClipAtPoint(GameWinAudio, new Vector3(0, 0, -18), voiceVolume);
        yield return new WaitForSeconds(1);
        AudioSource.PlayClipAtPoint(YouWinAudio, new Vector3(0, 0, -18), voiceVolume);
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    /// <summary>
    /// Calculates score
    /// </summary>
    void calculateScore()
    {
        for(int i = 0; i < Cities.Count; i++)
        {
            score = score + 1000;
        }
        for (int i = 0; i < MissileControllerCenter.GetComponent<MissileControllerScript>().ammoCenter.Count; i++)
        {
            score = score + 200;
        }
        for (int i = 0; i < MissileControllerRight.GetComponent<MissileControllerScript>().ammoRight.Count; i++)
        {
            score = score + 200;
        }
        for (int i = 0; i < MissileControllerLeft.GetComponent<MissileControllerScript>().ammoLeft.Count; i++)
        {
            score = score + 200;
        }
    }

}
