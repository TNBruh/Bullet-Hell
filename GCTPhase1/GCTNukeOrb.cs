using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTNukeOrb : Bullet
{
    [SerializeField] float maxTurn = 90;
    Quaternion ceilTurn;
    [SerializeField] bool earlyTimeStop = false;

    protected override void Start()
    {
        base.Start();
        GCTP1 script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP1)) as GCTP1;
        script.AddInstance((Bullet)this);
        StopTime(earlyTimeStop);
        ceilTurn = coords.rotation * Quaternion.Euler(0, 0, maxTurn);
    }
    private void FixedUpdate()
    {
        MoveBulletY();
        body.MoveRotation(Quaternion.Slerp(coords.rotation, ceilTurn, GetRotationalSpeed()));
    }
}
