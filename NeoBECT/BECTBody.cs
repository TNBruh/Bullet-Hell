using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BECTBody : Bullet
{
    [SerializeField] bool isVertical = true;

    override protected void Start()
    {
        if (!isVertical)
        {
            coords.Rotate(0, 0, 270);
        }
    }
    private void FixedUpdate()
    {
        MoveBulletYTransform();

        if (coords.position.y >= 9  || coords.position.y <= -9  || coords.position.x >= 9 || coords.position.x <= -9)
        {
            coords.Rotate(0, 0, 180);
        }
    }
}
