using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3BounceArrow : Bullet
{
    [SerializeField] bool canBounce = true;
    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        MoveBulletY();
        if (canBounce)
        {
            if (coords.position.y >= 4.8)
            {
                coords.rotation = Quaternion.Euler(0, 0, 180);
                canBounce = false;
            }
            else if (coords.position.y <= -4.8)
            {
                coords.rotation = Quaternion.Euler(0, 0, 0);
                canBounce = false;
            }
            else if (coords.position.x >= 4.5)
            {
                coords.rotation = Quaternion.Euler(0, 0, 90);
                canBounce = false;
            }
            else if (coords.position.x <= -4.5)
            {
                coords.rotation = Quaternion.Euler(0, 0, 270);
                canBounce = false;
            }
        }
    }
}
