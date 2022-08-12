using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTArrowLauncher : Bullet
{
    [SerializeField] GameObject arrow0;
    [SerializeField] GameObject arrow1;
    [SerializeField] GameObject arrow2;
    [SerializeField] GameObject arrow3;
    internal bool allowFire = false;
    [SerializeField] float angleLow = 10;
    Quaternion qOrigin;
    Quaternion q5;
    Quaternion q10;
    Quaternion q15;
    Quaternion qMax;

    protected override void Start()
    {
        base.Start();
        int mult = 1;
        qOrigin = coords.localRotation;
        if (rotationalSpeed > 0)
        {
            angleLow = 360 - angleLow;
            mult = -1;
        }
        q5 = Quaternion.Euler(0, 0, 5 * mult);
        q10 = Quaternion.Euler(0, 0, 10 * mult);
        q15 = Quaternion.Euler(0, 0, 15 * mult);
        //Debug.Log(angleLow);
        qMax = Quaternion.Euler(0, 0, angleLow);
    }

    private void FixedUpdate()
    {
        if (allowFire)
        {
            coords.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(coords.localRotation.eulerAngles.z + GetRotationalSpeed(), Mathf.Min(qOrigin.eulerAngles.z, angleLow), Mathf.Max(qOrigin.eulerAngles.z, angleLow)));
            
            if (coords.localRotation == qMax)
            {
                //Debug.Log(allowFire);
                allowFire = false;
            }
        }
    }

    IEnumerator StartFire()
    {
        allowFire = true;
        while (allowFire)
        {
            Instantiate(arrow0, coords.position, coords.rotation * q15);
            Instantiate(arrow1, coords.position, coords.rotation * q10);
            Instantiate(arrow2, coords.position, coords.rotation * q5);
            //Instantiate(arrow3, coords.position, coords.rotation);
            yield return new WaitForSeconds(recoil);
        }
        coords.localRotation = qOrigin;
    }

    internal void CommenceFire()
    {
        StartCoroutine(StartFire());
    }
}
