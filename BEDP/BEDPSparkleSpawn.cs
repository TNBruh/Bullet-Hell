using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPSparkleSpawn : Bullet
{
    protected override void Start()
    {
        base.Start();
        LookAtObject(coords.parent.position);
    }
}
