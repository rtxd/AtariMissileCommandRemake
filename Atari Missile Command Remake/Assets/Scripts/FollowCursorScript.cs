﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the position of the sprite to the mouse position
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }
}
