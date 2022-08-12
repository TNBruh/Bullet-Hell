using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3PrisonPortal : Bullet
{
    [SerializeField] GameObject portal;
    //GCTP3 script;

    protected override void Start()
    {
        base.Start();

        LookAtObject(enemy.transform.position);
        Instantiate(portal, coords.position, coords.rotation);
        Instantiate(portal, coords.position, coords.rotation * Quaternion.Euler(0, 0, 120));
        Instantiate(portal, coords.position, coords.rotation * Quaternion.Euler(0, 0, 240));
        Destroy(gameObject);
    }
}
