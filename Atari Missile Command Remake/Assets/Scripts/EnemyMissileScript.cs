using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileScript : MonoBehaviour
{
    //Direction of missile
    Vector3 target;
    Vector3 startPosition;
    //Change this to dictate speed missile fires at
    public float timeToReachTarget;

    float t;

    // Start is called before the first frame update
    void Start()
    {
        //Choose a random direction to aim at the platform
        target = new Vector3(Random.Range(-5.0f, 5.0f), -6.0f, 0.0f);
        //Set the start position to the objects current position
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        rotate();

        
    }

    //Move
    void move()
    {
        //Time fraction for lerp
        t += Time.deltaTime / timeToReachTarget;
        //Shoot missile
        transform.position = Vector3.Lerp(startPosition, target, t);
    }

    //Rotate sprite so it's facing the direction it's aiming
    void rotate()
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime);
    }
}
