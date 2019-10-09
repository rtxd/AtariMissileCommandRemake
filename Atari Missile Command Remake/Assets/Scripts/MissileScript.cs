using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{

    public Vector3 target;
    public Vector2 spawnPos;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPos;
    }

    

    // Update is called once per frame
    void Update()
    {
        fireMissile();
        if(transform.position == target)
        {
            explode();
        }
    }

    void fireMissile()
    {
        //move the missile
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        rotateToFaceMouse();
    }

    /// <summary>
    /// Rotate sprite to face mouse
    /// </summary>
    void rotateToFaceMouse()
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
    }

    /// <summary>
    /// Display animation for explosion and delete missile game object
    /// </summary>
    void explode()
    {
        Destroy(this.gameObject);
    }
}
