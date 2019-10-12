using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControllerScript : MonoBehaviour
{
    public GameObject missile;
    public Vector2 spawnPos;
    public float speed;
    public KeyCode fireButton;
    List<GameObject> ammo;
    public GameObject bullet;
    public float[] bulletSpawnPointsY;
    public float[] bulletSpawnPointsX;


    // Start is called before the first frame update
    void Start()
    {
        //Create a new list of bullets
        ammo = new List<GameObject>();
        for(int i = 0; i < 10; i++)
        {
            //bullet.GetComponent<BulletScript>.spawnPos = new Vector3
            //Instantiate(bullet);
            //ammo.Add(bullet);
        }


        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(fireButton))
        {
            missile.GetComponent<MissileScript>().spawnPos = spawnPos;
            missile.GetComponent<MissileScript>().target = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            missile.GetComponent<MissileScript>().speed = speed;
            Instantiate(missile);
        }
    }
}
