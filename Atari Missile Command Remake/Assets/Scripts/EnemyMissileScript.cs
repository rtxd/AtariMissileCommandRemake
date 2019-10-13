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
    //The time it takes for the explosion animation to play out
    public float explosionTime;
    public Transform explosionPos;
    public AudioClip explosionAudio;
    public GameObject explosionAnim;
    public GameObject gameManager;
    float t;
    public bool mainMenu;
    Vector3 direction;

    void Start()
    {
        direction = (target - transform.position).normalized;
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
            //if(!mainMenu)
                explode();
        }
    }

    /// <summary>
    /// Move the missile
    /// </summary>
    void move()
    {
        //Time fraction for lerp
        t += Time.deltaTime / timeToReachTarget;
        //Shoot missile
        transform.position = Vector2.Lerp(startPosition, target, t);
    }

    /// <summary>
    /// Rotate sprite so it's facing the direction it's aiming
    /// </summary>
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

    /// <summary>
    /// If the missile collides with an explosion it should blow up
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        //When an enemy missile hits an explosion it should explode
        if(col.tag == "explosion")
        {
            explode();
        }
        
        
    }

}
