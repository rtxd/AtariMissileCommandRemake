using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    public string levelType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        //When a missile hits this, it should select the level
        if (col.tag == "explosion")
        {
            if (levelType == "recreated")
            {
                SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
            }
            else if (levelType == "custom")
            {
                SceneManager.LoadScene("CustomLevel", LoadSceneMode.Single);
            }
            
        }


    }
}
