using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPDiamondSeed : Bullet
{
    private void FixedUpdate()
    {
        TurnTransform(GetRotationalSpeed());
    }
}
