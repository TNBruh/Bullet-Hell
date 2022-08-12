using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BECTBodyStart : Bullet
{
    [SerializeField] GameObject realBullet;
    [SerializeField] bool isVertical = true;
    internal bool allowLaunch = false;
    internal bool haveFired = false;

    override protected void Start()
    {
        base.Start();
        if (!isVertical)
        {
            coords.Rotate(0, 0, 270);
        }
    }
    private void FixedUpdate()
    {
        if (allowLaunch)
        {
            MoveBulletYTransform();
        }

        if ((coords.position.y >= 9 || coords.position.y <= -9 || coords.position.x >= 9 || coords.position.x <= -9) && !haveFired)
        {
            Instantiate(realBullet, new Vector3(0, 8.9f, 0), qZero);
            haveFired = true;
            Destroy(gameObject);
        }
    }
}
