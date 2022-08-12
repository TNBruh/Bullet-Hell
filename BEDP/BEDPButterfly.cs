using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPButterfly : Bullet
{
    internal bool allowLaunch = false;
    internal bool haveFired = false;

    private void FixedUpdate()
    {
        if (allowLaunch)
        {
            MoveBulletYTransform();
        }
        else
        {
            LookAtObject(enemy.transform.position);
        }

    }
}
