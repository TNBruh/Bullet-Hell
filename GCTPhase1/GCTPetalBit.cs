using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTPetalBit : Bullet
{
    Vector3 originPos;
    //[SerializeField] float progPercent;
    internal bool inPlace = false;
    //[SerializeField] GameObject redKnife;
    //[SerializeField] GameObject blueKnife;
    //red [0] blue [1]
    [SerializeField] float stepDist = 1.5f;
    [SerializeField] GameObject[] pair = new GameObject[2];
    [SerializeField] GameObject[] pair1 = new GameObject[2];
    [SerializeField] GameObject[] pair2 = new GameObject[2];
    [SerializeField] GameObject[] pair3 = new GameObject[2];
    internal TGCMaster masterTGC;

    [SerializeField] bool triggerBurst = false;

    protected override void Awake()
    {
        base.Awake();
        /*
        masterTGC = GameObject.FindGameObjectWithTag("Bullet2").GetComponent<TGCMaster>();
        masterTGC.AddInstance((Bullet)this);
        */
    }

    protected override void Start()
    {
        base.Start();
        GCTP1 script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP1)) as GCTP1;
        script.AddInstance((Bullet)this);
        originPos = coords.localPosition;
        coords.localPosition = vectZero;
    }

    private void FixedUpdate()
    {
        if (coords.localPosition != originPos)
        {
            coords.localPosition = Vector3.MoveTowards(coords.localPosition, originPos, GetSpeed());
        }
        /*
        if (triggerBurst)
        {
            triggerBurst = false;
            StopTime(true);
        }
        */
    }

    private void SpawnBullet()
    {
        LookAtObject(enemy.transform.position);
        Vector3 eAngle = SideSteppedTargeting(stepDist, enemy.transform.position);
        //GameObject[] obj = new GameObject[3];
        Instantiate(pair[1], coords.position, coords.rotation * Quaternion.Euler(eAngle));
        Instantiate(pair[1], coords.position, coords.rotation * Quaternion.Euler(0, 0, -eAngle.z));
        Instantiate(pair[0], coords.position, coords.rotation);
        
        Instantiate(pair1[1], coords.position, coords.rotation * Quaternion.Euler(eAngle));
        Instantiate(pair1[1], coords.position, coords.rotation * Quaternion.Euler(0, 0, -eAngle.z));
        Instantiate(pair1[0], coords.position, coords.rotation);
        
        Instantiate(pair2[1], coords.position, coords.rotation * Quaternion.Euler(eAngle));
        Instantiate(pair2[1], coords.position, coords.rotation * Quaternion.Euler(0, 0, -eAngle.z));
        Instantiate(pair2[0], coords.position, coords.rotation);
        
        Instantiate(pair3[1], coords.position, coords.rotation * Quaternion.Euler(eAngle));
        Instantiate(pair3[1], coords.position, coords.rotation * Quaternion.Euler(0, 0, -eAngle.z));
        Instantiate(pair3[0], coords.position, coords.rotation);
        
        /*
        foreach (GameObject i in obj)
        {
            i.GetComponent<TGCKnifePurple>().masterTGC = masterTGC;
            i.SetActive(true);
        }
        */
        Destroy(gameObject);
    }

    internal override void StopTime(bool isStopped)
    {
        base.StopTime(isStopped);
        SpawnBullet();


    }
}
