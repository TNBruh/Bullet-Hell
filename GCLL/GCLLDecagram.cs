using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCLLDecagram : Bullet
{
    [SerializeField] GameObject bulletFast;
    [SerializeField] GameObject bulletSlow;
    [SerializeField] int maxBounces = 2;
    internal Vector3 direction;
    bool allowFire = true;
    Quaternion q90 = Quaternion.Euler(0, 0, 135);
    Quaternion q_90 = Quaternion.Euler(0, 0, -135);
    Quaternion q180 = Quaternion.Euler(0, 0, 180);
    int totalBounces = 0;

    protected override void Start()
    {
        base.Start();
        GameObject.FindGameObjectWithTag("Bullet2").GetComponent<GCLLMaster>().AddInstance((Bullet)this);
        //LookAtObject(enemy.transform.position);
    }

    private void FixedUpdate()
    {
        MoveBulletY();
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        if (allowFire && !timeStopped)
        {
            allowFire = false;
            //yield return new WaitUntil(() => { return !timeStopped; });
            Instantiate(bulletFast, coords.position, coords.rotation * q90);
            //Instantiate(bulletSlow, coords.position, coords.rotation * q90);
            Instantiate(bulletFast, coords.position, coords.rotation * q_90);
            //Instantiate(bulletSlow, coords.position, coords.rotation * q_90);
            Instantiate(bulletFast, coords.position, coords.rotation * q180);
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        string dName;
        if (collided.tag == "Barrier")
        {
            //totalBounces++;
            Debug.Log(collision.gameObject.name);
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
    }

}
