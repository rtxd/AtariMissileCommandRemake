using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public Vector2 spawnPos;
    public float explosionTime;
    // Start is called before the first frame update
    void Start()
    {
        //Set spawn position
        transform.position = spawnPos;
        //Destroy this explosion after a certain amount of time
        Destroy(gameObject, explosionTime);
    }

}
