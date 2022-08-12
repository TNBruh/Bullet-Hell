using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTCrossOrb : Bullet
{
    [SerializeField] GameObject knife;
    protected override void Start()
    {
        base.Start();

        GCTP2 script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP2)) as GCTP2;
        script.AddInstance((Bullet)this);
    }

    internal override void StopTime(bool isStopped)
    {
        base.StopTime(isStopped);
        Instantiate(knife, coords.position, coords.rotation);
        Instantiate(knife, coords.position, coords.rotation * Quaternion.Euler(0, 0, 90));
        Instantiate(knife, coords.position, coords.rotation * Quaternion.Euler(0, 0, 180));
        Instantiate(knife, coords.position, coords.rotation * Quaternion.Euler(0, 0, 270));
        Destroy(gameObject);
    }
}
