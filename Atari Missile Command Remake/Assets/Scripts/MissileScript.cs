using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{

    
    public Vector2 spawnPos;
    public GameObject explosionAnim;
    public GameObject locationMarker;
    public float speed;
    public float explosionTime;
    public Transform explosionPos;
    public AudioClip explosionAudio;
    Vector3 direction;
    public Vector3 target;
    bool markerPlaced = false;
    GameObject thisLocationMarker;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPos;
        direction = (target - transform.position).normalized;
    }

    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        rotateToFaceMouse();
        //move the missile
        
        if(Vector3.Distance(target, transform.position) <= 0.2)
        {
            Destroy(thisLocationMarker);
            explode();
        }
        else
        {
            transform.position = transform.position + (direction * (speed * Time.deltaTime));
            if (!markerPlaced)
                placeLocationMarker(target.x, target.y);
        }
        
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
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
        AudioSource.PlayClipAtPoint(explosionAudio, new Vector3(0, 0, 0));
        explosionAnim.GetComponent<ExplosionScript>().explosionTime = explosionTime;
        explosionAnim.GetComponent<ExplosionScript>().spawnPos = transform.position;
        Instantiate(explosionAnim, new Vector3(0,0,0), Quaternion.identity);
        
        
        Destroy(this.gameObject);
    }

    void placeLocationMarker(float targetX, float targetY)
    {
        thisLocationMarker = Instantiate(locationMarker, new Vector3(targetX, targetY, 0), Quaternion.identity);
        markerPlaced = true;
    }
}
