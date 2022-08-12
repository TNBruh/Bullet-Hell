using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTCrashOrb2 : Bullet
{
    [SerializeField] float minSize = 0.01f;
    [SerializeField] float minSizeFire = 0.08f;
    [SerializeField] float growthSpeed = 1;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject orb;
    [SerializeField] float window = 15;
    [SerializeField] float firingAngle = 120;
    [SerializeField] float recoil2 = 3;
    Quaternion qWindow;
    Quaternion q_Window;
    Quaternion[] qDist;
    Vector3 maxSize;
    Vector3 minSizeFireV;
    bool allowFire = true;
    bool allowFire2 = true;
    bool stopRotate = false;

    protected override void Start()
    {
        base.Start();
        qDist = new Quaternion[6];
        maxSize = coords.localScale;
        coords.localScale = new Vector3(minSize, minSize, 1);
        LookAtObject(enemy.transform.position);
        qDist[0] = Quaternion.Euler(0, 0, window);
        qDist[1] = Quaternion.Euler(0, 0, -window);
        qDist[2] = Quaternion.Euler(0, 0, window + 120);
        qDist[3] = Quaternion.Euler(0, 0, -window + 120);
        qDist[4] = Quaternion.Euler(0, 0, window + 240);
        qDist[5] = Quaternion.Euler(0, 0, -window + 240);
        minSizeFireV = new Vector3(minSizeFire, minSizeFire, 1);
        StartCoroutine(Burst());
    }

    private void FixedUpdate()
    {
        coords.localScale = Vector3.Lerp(coords.localScale, maxSize, growthSpeed * Time.deltaTime);
        if (!stopRotate)
        {

            coords.rotation *= Quaternion.Euler(0, 0, rotationalSpeed * timeStoppedMult);
        }
        if (minSizeFireV.x - coords.localScale.x <= 0)
        {
            StartCoroutine(StartFire());
        }
    }

    IEnumerator StartFire()
    {
        if (allowFire && !timeStopped && !stopRotate)
        {
            allowFire = false;
            Instantiate(arrow, coords.position, coords.rotation * qDist[0]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[1]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[2]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[3]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[4]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[5]);
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }

    IEnumerator Burst()
    {
        stopRotate = true;
        yield return new WaitForSeconds(recoil2);
        coords.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 0) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 45) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 90) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 135) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 180) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 225) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 270) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 315) * coords.rotation);
        Destroy(gameObject);
    }

    internal void CallBurst()
    {

        StartCoroutine(Burst());
    }

}
