using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileScript : MonoBehaviour
{
    //Direction of missile
    Vector3 target;
    Vector2 startPosition;
    //Change this to dictate speed missile fires at
    float timeToReachTarget;
    public float explosionTime;
    public Transform explosionPos;
    public AudioClip explosionAudio;
    public GameObject explosionAnim;
    public GameObject gameManager;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        //Choose a random city to target
        var selectedCity = Random.Range(0, 6);
        target = gameManager.GetComponent<GameManagerScript>().citySpawnPoints[selectedCity];
        //Set the start position to the objects current position
        startPosition = transform.position;
        //Set a random speed
        timeToReachTarget = Random.Range(8, 16);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        rotate();
        //When the missile hits the city explode
        if (Vector3.Distance(target, transform.position) <= 0.2)
        {
            explode();
        }
    }

    //Move
    void move()
    {
        //Time fraction for lerp
        t += Time.deltaTime / timeToReachTarget;
        //Shoot missile
        transform.position = Vector2.Lerp(startPosition, target, t);
    }

    //Rotate sprite so it's facing the direction it's aiming
    void rotate()
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
        //Play explosion sound
        AudioSource.PlayClipAtPoint(explosionAudio, new Vector2(0, 0));
        //Setup explosion variables
        explosionAnim.GetComponent<ExplosionScript>().explosionTime = explosionTime;
        explosionAnim.GetComponent<ExplosionScript>().spawnPos = transform.position;
        //Create explosion
        Instantiate(explosionAnim, new Vector2(0, 0), Quaternion.identity);


        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //When an enemy missile hits an explosion it should explode
        if(col.tag == "explosion")
        {
            explode();
        }
        
        
    }

}
