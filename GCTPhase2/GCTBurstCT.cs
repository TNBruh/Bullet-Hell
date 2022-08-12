using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTBurstCT : Bullet
{
    //internal TGCMaster masterTGC;
    [SerializeField] GameObject knifeBlue;
    [SerializeField] GameObject knifeRed;
    [SerializeField] int maxCount = 5;
    [SerializeField] float angle = 40;
    [SerializeField] float tilt = 8;
    Quaternion qTilt1;
    Quaternion qTilt2;
    Quaternion qTilt_1;
    Quaternion qTilt_2;
    Transform sakuya;

    [SerializeField] bool triggerFire = false;
    [SerializeField] bool triggerFire1 = false;

    protected override void Start()
    {
        base.Start();

        //knifeBlue.GetComponent<TGCKnifePurple>().masterTGC = masterTGC;
        //knifeRed.GetComponent<TGCKnifePurple>().masterTGC = masterTGC;
        qTilt1 = Quaternion.Euler(0, 0, tilt);
        qTilt2 = Quaternion.Euler(0, 0, tilt * 2);
        qTilt_1 = Quaternion.Euler(0, 0, -tilt);
        qTilt_2 = Quaternion.Euler(0, 0, -tilt * 2);

        sakuya = GameObject.FindGameObjectWithTag("Sakuya").transform;
        coords.position = sakuya.position;
    }

    private void FixedUpdate()
    {
        coords.position = sakuya.position;
        //coords.position = sakuya.position;
        /*
        if (triggerFire)
        {
            triggerFire = false;
            StartCoroutine(SpawnBullet());
        }
        if (triggerFire1)
        {
            triggerFire1 = false;
            StartCoroutine(SpawnBullet1());
        }
        */

    }

    IEnumerator SpawnBullet()
    {
        int count = 0;
        float distAngle = (angle * 2) / (maxCount - 1);

        LookAtObject(enemy.transform.position);
        coords.rotation *= Quaternion.Euler(0, 0, angle);
        //GameObject[] obj = new GameObject[5];
        while (count < maxCount)
        {
            //Debug.Log("fire--");
            Instantiate(knifeRed, coords.position, coords.rotation);
            Instantiate(knifeBlue, coords.position, coords.rotation * qTilt1);
            Instantiate(knifeRed, coords.position, coords.rotation * qTilt2);
            Instantiate(knifeBlue, coords.position, coords.rotation * qTilt_1);
            Instantiate(knifeRed, coords.position, coords.rotation * qTilt_2);
            /*
            foreach (GameObject i in obj)
            {
                i.GetComponent<TGCKnifePurple>().masterTGC = masterTGC;
                i.SetActive(true);
            }
            */
            yield return new WaitForSeconds(recoil);
            coords.rotation *= Quaternion.Euler(0, 0, -distAngle);
            count++;
            //Debug.Log(count < maxCount);
        }
    }

    internal void CommenceFire()
    {

        StartCoroutine(SpawnBullet());
    }
    internal void CommenceFire1()
    {

        StartCoroutine(SpawnBullet1());
    }

    IEnumerator SpawnBullet1()
    {
        int count = 0;
        float distAngle = (angle * 2) / (maxCount - 1);

        LookAtObject(enemy.transform.position);
        coords.rotation *= Quaternion.Euler(0, 0, -angle);
        //GameObject[] obj = new GameObject[5];
        while (count < maxCount)
        {
            //Debug.Log(count < maxCount);
            Instantiate(knifeRed, coords.position, coords.rotation);
            Instantiate(knifeBlue, coords.position, coords.rotation * qTilt1);
            Instantiate(knifeRed, coords.position, coords.rotation * qTilt2);
            Instantiate(knifeBlue, coords.position, coords.rotation * qTilt_1);
            Instantiate(knifeRed, coords.position, coords.rotation * qTilt_2);
            /*
            foreach (GameObject i in obj)
            {
                i.GetComponent<TGCKnifePurple>().masterTGC = masterTGC;
                i.SetActive(true);
            }
            */
            yield return new WaitForSeconds(recoil);
            coords.rotation *= Quaternion.Euler(0, 0, distAngle);
            count++;
        }
        //Debug.Log("fire--");
    }
}
