using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandButterflyHoming : Bullet
{
    Vector3 targetLocation;
    bool home = true;

    protected override void Awake()
    {
        base.Awake();
        Transform targetObj = GameObject.FindGameObjectWithTag("NPC").transform;
        targetLocation = new Vector3(targetObj.position.x, targetObj.position.y);
    }

    private void FixedUpdate()
    {
        if (home)
        {
            Homing(targetLocation, 200);
            home = (Mathf.Abs(Vector3.Distance(coords.position, targetLocation)) < 0.8) ? accel() : true;
        }
        else
        {
            MoveBulletY();
        }
    }

    internal bool accel()
    {
        boost = 1.2f;
        return false;
    }


}
