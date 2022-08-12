using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTBurstPoint : Bullet
{
    Transform sakuya;

    protected override void Start()
    {
        base.Start();

        //knifeBlue.GetComponent<TGCKnifePurple>().masterTGC = masterTGC;
        //knifeRed.GetComponent<TGCKnifePurple>().masterTGC = masterTGC;

        sakuya = GameObject.FindGameObjectWithTag("Sakuya").transform;
        coords.position = sakuya.position;
        LookAtObject(enemy.transform.position);
    }

    private void FixedUpdate()
    {
        coords.position = sakuya.position;
        LookAtObject(enemy.transform.position);
    }
}
