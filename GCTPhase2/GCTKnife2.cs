using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTKnife2 : Bullet
{
    [SerializeField] bool earlyTimeStop = true;

    protected override void Start()
    {
        base.Start();
        StopTime(earlyTimeStop);
        GCTP2 script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP2)) as GCTP2;
        script.AddInstance((Bullet)this);
    }

    private void FixedUpdate()
    {
        MoveBulletY();
    }
}
