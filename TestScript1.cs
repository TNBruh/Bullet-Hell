using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : Bullet
{
    private void FixedUpdate()
    {
        //body.MovePosition(new Vector2(1, 2));
        coords.position = (new Vector2(1, 2));
    }
}
