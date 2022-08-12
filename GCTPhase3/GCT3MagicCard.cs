using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3MagicCard : Bullet
{
    [SerializeField] float dist;
    internal Vector3 origin;

    protected override void Start()
    {
        base.Start();
        origin = coords.position;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(origin, coords.position) < dist)
        {
            MoveBulletY();
        }
        else
        {
            body.velocity = new Vector3();
            hitbox.enabled = false;
        }
    }

}
