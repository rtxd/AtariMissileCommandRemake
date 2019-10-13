using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        //If enemy missile hits city then the city should swap to a destroyed city sprite
        //Maybe set a boolean to swap animation/image
        if (col.tag == "enemyMissile")
        {
            Destroy(this.gameObject, 1);
        }
    }
}
