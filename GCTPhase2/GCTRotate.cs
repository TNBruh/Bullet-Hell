using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTRotate : Bullet
{
    private void FixedUpdate()
    {
        TurnTransform(GetRotationalSpeed());
        coords.position = enemy.transform.position;
        if (coords.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

}
