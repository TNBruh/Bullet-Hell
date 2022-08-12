using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3FollowPlayer : Bullet
{
    protected override void Start()
    {
        base.Start();
        coords.position = enemy.transform.position;
    }

    private void FixedUpdate()
    {

        coords.position = enemy.transform.position;
    }
}
