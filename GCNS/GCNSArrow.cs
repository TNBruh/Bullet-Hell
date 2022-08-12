using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCNSArrow : Bullet
{
    [SerializeField] float maxTurn = 90;
    Quaternion ceilTurn;

    protected override void Start()
    {
        base.Start();
        ceilTurn = coords.rotation * Quaternion.Euler(0, 0, maxTurn);
    }

    private void FixedUpdate()
    {
        MoveBulletY();
        body.MoveRotation(Quaternion.Slerp(coords.rotation, ceilTurn, GetRotationalSpeed()));
    }
}
