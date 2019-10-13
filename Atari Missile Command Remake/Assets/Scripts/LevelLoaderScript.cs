using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    public string levelType;

    /// <summary>
    /// When the player shoots a missile at the level, it should load that level
    /// I wasn't able to get around to making the second level (the custom level) 
    /// so it was disabled in the main menu screen
    /// </summary>
    /// <param name="col"></param>
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
