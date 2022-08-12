using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTCard : Bullet
{
    [SerializeField] internal float deAccel = -0.05f;
    [SerializeField] internal float maxTurn = -90;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject knife1;
    [SerializeField] internal float window = -20;
    bool isFiring = false;

    Quaternion ceilTurn;


    protected override void Start()
    {
        base.Start();
        ceilTurn = coords.rotation * Quaternion.Euler(0, 0, maxTurn);
    }

    private void FixedUpdate()
    {
        //body.velocity = coords.up * GetSpeed();
        MoveBulletY();
        ChangeMult();
        if (boost != 0)
        {
            body.MoveRotation(Quaternion.Slerp(coords.rotation, ceilTurn, GetRotationalSpeed()));
        }
        else if (!isFiring && boost == 0)
        {
            StartCoroutine(Burst());
        }
    }

    internal void ChangeMult(int mult = 1)
    {
        boost = Mathf.Clamp(boost + deAccel * mult, 0, Mathf.Infinity);
    }

    IEnumerator Burst()
    {
        isFiring = true;
        yield return new WaitForSeconds(recoil);
        Instantiate(knife, coords.position, coords.rotation);
        Instantiate(knife1, coords.position, coords.rotation * Quaternion.Euler(0,0,window));
        Destroy(gameObject);
    }
}
