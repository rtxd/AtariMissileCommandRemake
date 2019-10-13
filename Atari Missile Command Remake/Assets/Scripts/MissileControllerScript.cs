using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControllerScript : MonoBehaviour
{
    public GameObject missile;
    public Vector2 spawnPos;
    public float speed;
    public KeyCode fireButton;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        //When player presses fire button, shoot a missile at their mouse location
        if (Input.GetKeyDown(fireButton))
        {
            missile.GetComponent<MissileScript>().spawnPos = spawnPos;
            missile.GetComponent<MissileScript>().target = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            missile.GetComponent<MissileScript>().speed = speed;
            Instantiate(missile);
        }
    }
}
