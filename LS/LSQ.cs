using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSQ : Bullet
{
    private void Update()
    {
        if (coords.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
