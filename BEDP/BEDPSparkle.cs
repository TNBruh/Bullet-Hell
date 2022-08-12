using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPSparkle : Bullet
{
    [SerializeField] float progAccel = 0.1f;
    [SerializeField] string targetTag = "Bullet2";
    [SerializeField] float progPercent = 2;
    GameObject target;

    override protected void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    private void FixedUpdate()
    {
        //TurnTransform(GetRotationalSpeed());
        //coords.position = Vector3.MoveTowards(coords.position, target.transform.position, progPercent);
        MoveBulletY();
        //progPercent += progAccel;
    }
}
