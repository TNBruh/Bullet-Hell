using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCLLMaster : Bullet
{
    [SerializeField] float openingTime = 8;
    [SerializeField] float frozenTime = 2;
    [SerializeField] GameObject revolvingDecagram;
    [SerializeField] GameObject bouncingDecagram;
    Transform npc;
    bool allowFire = false;
    internal List<Bullet> instancesTGC = new List<Bullet>();


    protected override void Start()
    {
        base.Start();
        npc = GameObject.FindGameObjectWithTag("NPC").transform;

        coords.position = npc.transform.position;
        AddInstance((Bullet)this);
        Instantiate(revolvingDecagram, coords.position, coords.rotation, coords);
        Instantiate(revolvingDecagram, coords.position, coords.rotation * Quaternion.Euler(0, 0, 120), coords);
        Instantiate(revolvingDecagram, coords.position, coords.rotation * Quaternion.Euler(0, 0, 240), coords);

        Instantiate(bouncingDecagram, coords.position, Quaternion.Euler(0,0,45));
        //Instantiate(bouncingDecagram, coords.position, Quaternion.Euler(0,0,135));
        //Instantiate(bouncingDecagram, coords.position, Quaternion.Euler(0,0,225));
        Instantiate(bouncingDecagram, coords.position, Quaternion.Euler(0,0,315));
        //Instantiate(bouncingDecagram, coords.position, qZero);
        StartCoroutine(Commence());

    }

    internal void AddInstance(Bullet blt)
    {
        instancesTGC.Add(blt);
    }

    internal void CullNull()
    {
        instancesTGC.RemoveAll((Bullet i) => i == null);
    }

    internal void Freeze()
    {
        instancesTGC.ForEach(delegate (Bullet i) { i.StopTime(true); });
    }

    internal void Unfreeze()
    {
        instancesTGC.ForEach(delegate (Bullet i) { i.StopTime(false); });
    }

    private void FixedUpdate()
    {
        TurnTransform(GetRotationalSpeed());
        coords.position = npc.transform.position;
        StartCoroutine(Illusion());
    }

    IEnumerator Commence()
    {
        yield return new WaitForSeconds(openingTime);
        allowFire = true;
    }

    IEnumerator Illusion()
    {
        if (allowFire)
        {
            allowFire = false;
            CullNull();
            Freeze();
            yield return new WaitForSeconds(frozenTime);
            CullNull();
            Unfreeze();
            Vector2 angleinVector = (-coords.position + enemy.transform.position).normalized;
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }
}
