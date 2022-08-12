using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTGrowOrb : Bullet
{
    [SerializeField] float maxTurn = 90;
    Quaternion ceilTurn;
    SpriteRenderer spriteRenderer;
    [SerializeField] float direction;
    [SerializeField] float illusionSpeed = 0.1f;
    Vector3 directionV;
    [SerializeField] bool earlyTimeStop = false;
    [SerializeField] float growthSpeed = 0.05f;
    [SerializeField] float minSize = 0.1f;
    Vector3 maxSize;

    protected override void Start()
    {
        base.Start();
        ceilTurn = coords.rotation * Quaternion.Euler(0, 0, maxTurn);
        maxSize = coords.localScale;
        coords.localScale = new Vector3(minSize, minSize, 1);
    }

    private void FixedUpdate()
    {
        MoveBulletY();
        body.MoveRotation(Quaternion.Slerp(coords.rotation, ceilTurn, GetRotationalSpeed()));
        coords.localScale = Vector3.MoveTowards(coords.localScale, maxSize, growthSpeed * Time.deltaTime);

        if (timeStopped)
        {
            coords.position += directionV * Time.deltaTime * illusionSpeed;
        }
    }
}
