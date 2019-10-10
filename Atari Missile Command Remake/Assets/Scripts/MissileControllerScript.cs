using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControllerScript : MonoBehaviour
{
    public GameObject missile;
    public Vector2 spawnPos;
    public float speed;
    public KeyCode fireButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(fireButton))
        {
            missile.GetComponent<MissileScript>().spawnPos = spawnPos;
            missile.GetComponent<MissileScript>().target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            missile.GetComponent<MissileScript>().speed = speed;
            Instantiate(missile);
        }
    }
}
