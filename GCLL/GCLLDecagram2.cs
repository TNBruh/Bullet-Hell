using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCLLDecagram2 : Bullet
{
    [SerializeField] bool isOpening = true;
    [SerializeField] float openingTime = 2.5f;
    [SerializeField] GameObject bullet;
    bool allowFire = true;
    Quaternion q120 = Quaternion.Euler(0, 0, 120);
    Quaternion q240 = Quaternion.Euler(0, 0, 240);

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Opening());
        GameObject.FindGameObjectWithTag("Bullet2").GetComponent<GCLLMaster>().AddInstance((Bullet)this);
    }

    private void FixedUpdate()
    {
        if (isOpening)
        {
            MoveBulletYTransform();
        }
        else
        {
            TurnTransform(GetRotationalSpeed());
            StartCoroutine(Fire());
        }
    }

    IEnumerator Opening()
    {
        yield return new WaitForSeconds(openingTime);
        isOpening = false;
    }

    IEnumerator Fire()
    {
        if (allowFire && !timeStopped)
        {
            allowFire = false;
            //yield return new WaitUntil(() => { return !timeStopped; });
            Instantiate(bullet, coords.position, coords.rotation);
            Instantiate(bullet, coords.position, coords.rotation * q120);
            Instantiate(bullet, coords.position, coords.rotation * q240);
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }
}
