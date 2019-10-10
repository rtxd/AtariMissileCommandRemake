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
        transform.position = spawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("I'm alive");
        Destroy(gameObject, explosionTime);
    }
}
