using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class GCNSCrashOrb : Bullet
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
    [SerializeField] float backStep = 0.01f;

    protected override void Start()
    {
        base.Start();
        qDist = new Quaternion[6];
        maxSize = coords.localScale;
        coords.localScale = new Vector3(minSize, minSize, 1);
        LookAtObject(enemy.transform.position);
        qDist[0] = Quaternion.Euler(0, 0, window);
        qDist[1] = Quaternion.Euler(0, 0, -window);
        qDist[2] = Quaternion.Euler(0, 0, window+120);
        qDist[3] = Quaternion.Euler(0, 0, -window+120);
        qDist[4] = Quaternion.Euler(0, 0, window+240);
        qDist[5] = Quaternion.Euler(0, 0, -window+240);
        minSizeFireV = new Vector3(minSizeFire, minSizeFire, 1);
        StartCoroutine(Burst());
    }

    private void FixedUpdate()
    {
        coords.localScale = Vector3.Lerp(coords.localScale, maxSize, growthSpeed * Time.deltaTime);
        if (!stopRotate)
        {

            coords.rotation *= Quaternion.Euler(0, 0, rotationalSpeed);
        }
        if (minSizeFireV.x - coords.localScale.x <= 0)
        {
            StartCoroutine(StartFire());
        }
    }

    IEnumerator StartFire()
    {
        if (allowFire && !stopRotate)
        {
            allowFire = false;
            float halfDist = coords.localScale.x / 2;
            Vector3 pos = CalcSpot(coords.rotation.eulerAngles.z + window);
            ///Debug.Log(pos);
            Instantiate(arrow, pos + coords.position, coords.rotation * qDist[0]);
            pos = CalcSpot(coords.rotation.eulerAngles.z - window);
            Instantiate(arrow, pos + coords.position, coords.rotation * qDist[1]);
            pos = CalcSpot(coords.rotation.eulerAngles.z + window + 120);
            Instantiate(arrow, pos + coords.position, coords.rotation * qDist[2]);
            pos = CalcSpot(coords.rotation.eulerAngles.z - window + 120);
            Instantiate(arrow, pos + coords.position, coords.rotation * qDist[3]);
            pos = CalcSpot(coords.rotation.eulerAngles.z + window + 240);
            Instantiate(arrow, pos + coords.position, coords.rotation * qDist[4]);
            pos = CalcSpot(coords.rotation.eulerAngles.z - window + 240);
            Instantiate(arrow, pos + coords.position, coords.rotation * qDist[5]);
            yield return new WaitForSeconds(recoil);
            allowFire = true;
            /*
            for (float i = 0; i < maxAge; i += Time.deltaTime)
            {
                
            }
            stopRotate = true;
            yield return new WaitForSeconds(recoil2);
            Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 0));
            Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 45));
            Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 90));
            Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 135));
            Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 180));
            Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 225));
            Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 270));
            Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 315));
            Destroy(gameObject);
            */
        }
    }

    IEnumerator Burst()
    {
        yield return new WaitForSeconds(recoil2);
        stopRotate = true;
        yield return new WaitForSeconds(recoil2/2);
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
    internal Vector3 CalcSpot(float inpRotation)
    {
        float dist = hitbox.bounds.extents.x - backStep;
        //Debug.Log(dist);
        Vector3 res = RotatePoint(inpRotation, new Vector3(0, dist));
        //Debug.Log(res);
        return (res);
    }

}
