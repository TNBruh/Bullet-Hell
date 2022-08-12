using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSWall : Bullet
{
    private void FixedUpdate()
    {
        MoveBulletYTransform();
    }
}
