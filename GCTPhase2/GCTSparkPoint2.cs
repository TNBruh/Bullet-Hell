using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTSparkPoint2 : Bullet
{
    [SerializeField] GameObject sparkObject;
    [SerializeField] internal GameObject marisa;
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
    [SerializeField] bool triggerFire = false;
    [SerializeField] bool triggerLaser = false;



    protected override void Start()
    {
        base.Start();
        LookAtObject(enemy.transform.position);
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.enabled = false;
        //marisa = GameObject.FindGameObjectWithTag("Marisa").transform;
        //coords.position = marisa.position;
        CommenceLaser();
    }

    IEnumerator Laser()
    {
        //activate laser
        LookAtObject(enemy.transform.position);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(recoil);


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
        Destroy(marisa);
        Destroy(gameObject);
    }

    internal void CommenceLaser()
    {
        StartCoroutine(Laser());
    }

    private void FixedUpdate()
    {
        if (isTargetting)
        {
            TurnTransform(GetRotationalSpeed());
        }
        /*
        if (triggerLaser)
        {
            triggerLaser = false;
            StartCoroutine(Laser());
        }
        */
        

    }
}
