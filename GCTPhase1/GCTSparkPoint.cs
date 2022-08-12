using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTSparkPoint : Bullet
{
    [SerializeField] GameObject sparkObject;
    [SerializeField] GameObject arrowLauncher0;
    GCTArrowLauncher launcher0;
    [SerializeField] GameObject arrowLauncher1;
    GCTArrowLauncher launcher1;
    [SerializeField] float sparkRecoil = 1f;
    [SerializeField] float sparkRecoil2 = 1f;
    internal bool allowFire = true;
    LayerMask filterMask;
    LineRenderer lineRenderer;
    Quaternion q10 = Quaternion.Euler(0, 0, 10);
    Quaternion q20 = Quaternion.Euler(0, 0, 30);
    Quaternion q_10 = Quaternion.Euler(0, 0, -10);
    Quaternion q_20 = Quaternion.Euler(0, 0, -30);
    bool isTargetting = false;
    Transform marisa;
    [SerializeField] bool triggerFire = false;
    [SerializeField] bool triggerLaser = false;



    protected override void Start()
    {
        base.Start();
        LookAtObject(enemy.transform.position);
        launcher0 = arrowLauncher0.GetComponent<GCTArrowLauncher>();
        launcher1 = arrowLauncher1.GetComponent<GCTArrowLauncher>();
        filterMask = LayerMask.GetMask("Player", "Bullet", "Bullet2", "Enemy", "Enemy2");
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.enabled = false;
        marisa = GameObject.FindGameObjectWithTag("Marisa").transform;
        coords.position = marisa.position;
    }

    IEnumerator Fire()
    {
        if (allowFire)
        {
            allowFire = false;
            //aim
            //draw line
            lineRenderer.enabled = true;
            LookAtObject(enemy.transform.position);
            //lineRenderer.SetPosition(0, coords.position);
            //lineRenderer.SetPosition(1, direction * 50);
            //activate arrow launchers (set allowFire into true)
            launcher0.CommenceFire();
            launcher1.CommenceFire();
            //wait until they're done
            yield return new WaitUntil(() => !launcher0.allowFire && !launcher1.allowFire);
            //disable when done
            
            //Instantiate(cyanButterfly, coords.position, coords.rotation * q10);
            //Instantiate(cyanButterfly, coords.position, coords.rotation * q20);
            //Instantiate(cyanButterfly, coords.position, coords.rotation * q_10);
            //Instantiate(cyanButterfly, coords.position, coords.rotation * q_20);
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }

    internal void CommenceFire()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Laser()
    {
        //activate laser

        lineRenderer.enabled = false;
        sparkObject.SetActive(true);
        yield return new WaitForSeconds(sparkRecoil);

        Vector3 direction = enemy.transform.position - coords.position;
        direction = enemy.transform.position - coords.position;
        direction = RotatePoint(-coords.rotation.eulerAngles.z, direction);
        rotationalSpeed = Mathf.Abs(rotationalSpeed);
        if (direction.x >= 0)
        {
            rotationalSpeed *= -1;
        }
        isTargetting = true;

        //coords.rotation = Quaternion.Slerp(coords.rotation, rotationToTarget, Time.deltaTime * GetRotationalSpeed());

        yield return new WaitForSeconds(sparkRecoil2);
        //Destroy(gameObject);
        isTargetting = false;
        sparkObject.SetActive(false);
    }

    internal void CommenceLaser()
    {
        StartCoroutine(Laser());
    }

    private void FixedUpdate()
    {
        coords.position = marisa.position;
        if (isTargetting)
        {
            TurnTransform(GetRotationalSpeed());
        }
        /*
        if (triggerFire)
        {
            triggerFire = false;
            CommenceFire();
        }
        if (triggerLaser)
        {
            triggerLaser = false;
            CommenceLaser();
        }
        */
        
    }
}
