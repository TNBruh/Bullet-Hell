using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTPrisonCard : Bullet
{
    [SerializeField] Vector3 originPos;
    [SerializeField] float maxDist;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject knife1;
    [SerializeField] internal float window = -80;
    [SerializeField] internal float deAccel = -0.001f;

    protected override void Start()
    {
        base.Start();
        originPos = coords.position;
        GCTP2 script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP2)) as GCTP2;
        script.AddInstance((Bullet)this);

    }

    private void FixedUpdate()
    {
        MoveBulletY();
        ChangeMult();
        if (boost <= 0 || Vector3.Distance(coords.position, originPos) > maxDist)
        {
            StopTime(true);

        }
    }



    internal void ChangeMult(int mult = 1)
    {
        boost = Mathf.Clamp(boost + deAccel * mult, 0, Mathf.Infinity);
    }

    internal void NormalizeSpeed()
    {
        boost = 1;
    }

    IEnumerator Burst()
    {
        yield return new WaitForSeconds(recoil);
        Instantiate(knife, coords.position, coords.rotation);
        Instantiate(knife1, coords.position, coords.rotation * Quaternion.Euler(0, 0, window));
        Destroy(gameObject);
    }

    internal override void StopTime(bool isStopped)
    {
        base.StopTime(isStopped);
        if (!isStopped)
        {
            StartCoroutine(Burst());
        }
    }
}
