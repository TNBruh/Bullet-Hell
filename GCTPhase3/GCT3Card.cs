using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3Card : Bullet
{
    GCTP3 script;
    [SerializeField] GameObject knife;

    override protected void Start()
    {
        base.Start();
        script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP3)) as GCTP3;
        script.AddInstance((Bullet)this);
    }

    internal override void StopTime(bool isStopped)
    {
        base.StopTime(isStopped);
        Instantiate(knife, gameObject.transform.position, gameObject.transform.rotation * Quaternion.Euler(0, 0, 90));
        Instantiate(knife, gameObject.transform.position, gameObject.transform.rotation * Quaternion.Euler(0, 0, -90));
        Destroy(gameObject);
    }
}
