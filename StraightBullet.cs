using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet
{
    // Update is called once per frame
    /*
    override protected void Start()
    {
        base.Start();
        MoveBulletY();

    }
    */
    private void FixedUpdate()
    {
        MoveBulletY();
        //MoveBulletYTransform();
    }

}
