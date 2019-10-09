using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControllerScript : MonoBehaviour
{
    public GameObject missile;
    public Vector2 spawnPos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            
            Instantiate(missile);
            missile.GetComponent<MissileScript>().spawnPos = spawnPos;
            missile.GetComponent<MissileScript>().target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            missile.GetComponent<MissileScript>().speed = speed;



        }
    }
}
