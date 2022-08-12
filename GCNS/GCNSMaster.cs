using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCNSMaster : Bullet
{
    [SerializeField] GameObject star;
    [SerializeField] GameObject nuke;
    [SerializeField] float recoil2 = 5;
    bool allowFire = true;
    bool allowFire2 = true;
    Transform npc;

    protected override void Start()
    {
        base.Start();
        npc = GameObject.FindGameObjectWithTag("NPC").transform;
    }

    private void FixedUpdate()
    {
        StartCoroutine(FireStar());
        StartCoroutine(FireNuke());
    }

    IEnumerator FireStar()
    {
        if (allowFire)
        {
            allowFire = false;
            Instantiate(star, npc.position, new Quaternion());
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }

    IEnumerator FireNuke()
    {
        if (allowFire2)
        {
            allowFire2 = false;
            Instantiate(nuke);
            yield return new WaitForSeconds(recoil2);
            allowFire2 = true;
        }
    }

}
