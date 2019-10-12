using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject EnemyMissile;

    // Start is called before the first frame update
    void Start()
    {
        spawnWave();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            spawnWave();

    }

    void spawnWave()
    {
        for(int i = 0; i < 10; i++)
        {
            Instantiate(EnemyMissile, new Vector3( (Random.Range(7, -7)), (Random.Range(20, 5)), 0), Quaternion.identity);
        }
    }
}
