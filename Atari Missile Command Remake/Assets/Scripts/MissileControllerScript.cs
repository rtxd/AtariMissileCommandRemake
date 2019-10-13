using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControllerScript : MonoBehaviour
{
    public GameObject missile;
    public Vector2 spawnPos;
    public float speed;
    public KeyCode fireButton;
    List<GameObject> ammoRight;
    List<GameObject> ammoCenter;
    List<GameObject> ammoLeft;
    public GameObject bullet;
    public Vector3[] bulletSpawnPointsLeft;
    public Vector3[] bulletSpawnPointsRight;
    public Vector3[] bulletSpawnPointsCenter;
    public string controllerLocation;



    // Start is called before the first frame update
    void Start()
    {
        //Create a new list of bullets
        ammoLeft = new List<GameObject>();
        ammoRight= new List<GameObject>();
        ammoCenter = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            if(controllerLocation == "left")
            {
                bullet.GetComponent<BulletScript>().spawnPos = bulletSpawnPointsLeft[i];
                var newBullet = Instantiate(bullet);
                ammoLeft.Add(newBullet);
            }

            if (controllerLocation == "right")
            {
                bullet.GetComponent<BulletScript>().spawnPos = bulletSpawnPointsRight[i];
                var newBullet = Instantiate(bullet);
                ammoRight.Add(newBullet);
            }

            if (controllerLocation == "center")
            {
                bullet.GetComponent<BulletScript>().spawnPos = bulletSpawnPointsCenter[i];
                var newBullet = Instantiate(bullet);
                ammoCenter.Add(newBullet);
            }
        }


        
    }

    
    void Update()
    {
        //When player presses fire button, shoot a missile at their mouse location
        if (Input.GetKeyDown(fireButton))
        {
            

            if (controllerLocation == "center")
            {
                if(ammoCenter.Count > 0)
                {
                    fire();
                    var bulletToRemove = ammoCenter.Count-1;
                    Destroy(ammoCenter[bulletToRemove]);
                    ammoCenter.Remove(ammoCenter[bulletToRemove]);
                }
                

            }

            if (controllerLocation == "left")
            {
                if (ammoLeft.Count > 0)
                {
                    fire();
                    var bulletToRemove = ammoLeft.Count-1;
                    Destroy(ammoLeft[bulletToRemove]);
                    ammoLeft.Remove(ammoLeft[bulletToRemove]);
                }
                
            }

            if (controllerLocation == "right")
            {
                if (ammoRight.Count > 0)
                {
                    fire();
                    var bulletToRemove = ammoRight.Count-1;
                    Destroy(ammoRight[bulletToRemove]);
                    ammoRight.Remove(ammoRight[bulletToRemove]);

                }
                
            }
        }
    }

    void fire()
    {
        missile.GetComponent<MissileScript>().spawnPos = spawnPos;
        missile.GetComponent<MissileScript>().target = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        missile.GetComponent<MissileScript>().speed = speed;
        Instantiate(missile);
    }
}
