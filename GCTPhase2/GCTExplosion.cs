using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTExplosion : Bullet
{
    [SerializeField] float minSize = 0.1f;
    [SerializeField] float minSizeFire = 0.8f;
    [SerializeField] float growthSpeed = 1;
    [SerializeField] GameObject arrow;
    [SerializeField] float window = 15;
    [SerializeField] float firingAngle = 120;
    [SerializeField] float recoil2 = 3;
    Quaternion qWindow;
    float direction;
    Vector3 directionV;
    Quaternion q_Window;
    Quaternion[] qDist;
    Vector3 maxSize;
    Vector3 minSizeFireV;
    bool allowFire = true;
    bool allowFire2 = true;
    bool stopRotate = false;
    float halfDist;
    [SerializeField] float backStep = 0.01f;
    internal bool unact = false;
    //[SerializeField] Transform spawnTrans;
    GCTP2 script;

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
        directionV = Angle2Vector(coords.rotation.eulerAngles.z + 90);
        halfDist = coords.localScale.x / 2;

        //spawnTrans = coords.GetChild(0);


        script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP2)) as GCTP2;
        script.AddInstance((Bullet)this);
        script.ins = gameObject;
    }

    private void FixedUpdate()
    {
        //coords.localScale = Vector3.Lerp(coords.localScale, maxSize, growthSpeed * Time.deltaTime);
        coords.localScale = Vector3.MoveTowards(coords.localScale, maxSize, growthSpeed * Time.deltaTime);
        coords.position += directionV * GetSpeed();
        coords.rotation *= Quaternion.Euler(0, 0, rotationalSpeed * timeStoppedMult);
        StartCoroutine(StartFire());
        if ((Mathf.Abs(coords.position.x) > 4.5 && Mathf.Abs(coords.position.y) > 4.8))
        {
            unact = true;
        }
    }

    IEnumerator StartFire()
    {
        if (allowFire && !timeStopped && !stopRotate && (Mathf.Abs(coords.position.x) <= 4.5 && Mathf.Abs(coords.position.y) <= 4.8))
        {
            allowFire = false;
            GameObject ins;
            /*
            Instantiate(arrow, coords.position, coords.rotation * qDist[0]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[1]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[2]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[3]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[4]);
            Instantiate(arrow, coords.position, coords.rotation * qDist[5]);
            */
            halfDist = coords.localScale.x / 2;
            Vector3 pos = CalcSpot(coords.rotation.eulerAngles.z + window);
            ///Debug.Log(pos);
            ins = Instantiate(arrow, pos + coords.position, coords.rotation * qDist[0]);
            script.AddInstance(ins.GetComponent(typeof(Bullet)) as Bullet);

            pos = CalcSpot(coords.rotation.eulerAngles.z - window);
            ins = Instantiate(arrow, pos + coords.position, coords.rotation * qDist[1]);
            script.AddInstance(ins.GetComponent(typeof(Bullet)) as Bullet);

            pos = CalcSpot(coords.rotation.eulerAngles.z + window + 120);
            ins = Instantiate(arrow, pos + coords.position, coords.rotation * qDist[2]);
            script.AddInstance(ins.GetComponent(typeof(Bullet)) as Bullet);

            pos = CalcSpot(coords.rotation.eulerAngles.z - window + 120);
            ins = Instantiate(arrow, pos + coords.position, coords.rotation * qDist[3]);
            script.AddInstance(ins.GetComponent(typeof(Bullet)) as Bullet);

            pos = CalcSpot(coords.rotation.eulerAngles.z + window + 240);
            ins = Instantiate(arrow, pos + coords.position, coords.rotation * qDist[4]);
            script.AddInstance(ins.GetComponent(typeof(Bullet)) as Bullet);

            pos = CalcSpot(coords.rotation.eulerAngles.z - window + 240);
            ins = Instantiate(arrow, pos + coords.position, coords.rotation * qDist[5]);
            script.AddInstance(ins.GetComponent(typeof(Bullet)) as Bullet);

            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
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
