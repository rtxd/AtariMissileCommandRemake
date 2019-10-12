using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
