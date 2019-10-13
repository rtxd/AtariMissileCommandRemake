using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScript : MonoBehaviour
{
    public bool mainMenu;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!mainMenu)
        {
            //If enemy missile hits city then the city should swap to a destroyed city sprite
            //Maybe set a boolean to swap animation/image
            if (col.tag == "enemyMissile")
            {
                Destroy(this.gameObject, 1);
            }
        }
        
    }
}
