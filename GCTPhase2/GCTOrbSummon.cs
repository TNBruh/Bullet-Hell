using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTOrbSummon : Bullet
{
    [SerializeField] GameObject crossOrb;
    bool allowFire = true;
    [SerializeField] int maxBounce = 2;
    [SerializeField] int bounce = 0;

    protected override void Start()
    {
        base.Start();

        GCTP2 script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP2)) as GCTP2;
        script.AddInstance((Bullet)this);
    }


    private void FixedUpdate()
    {
        MoveBulletY();
        StartCoroutine(Fire());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        string dName;
        if (collided.tag == "Barrier")
        {
            if (bounce < maxBounce)
            {
                dName = collided.name;
                if (dName == "BarrierTop" || dName == "BarrierBottom")
                {
                    coords.rotation = Quaternion.Euler(0, 0, (-coords.eulerAngles.z + 180));
                }
                else if (dName == "BarrierLeft" || dName == "BarrierRight")
                {
                    coords.rotation = Quaternion.Euler(0, 0, -coords.eulerAngles.z);
                }
            }
            bounce++;
        }
    }

    IEnumerator Fire()
    {
        if (allowFire && !timeStopped && bounce <= maxBounce)
        {
            allowFire = false;
            Instantiate(crossOrb, coords.position, new Quaternion());
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }

    internal override void StopTime(bool isStopped)
    {
        base.StopTime(isStopped);
        Destroy(gameObject);
    }
}
