using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursorScript : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        //Set the position of the sprite to the mouse position
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }
}
