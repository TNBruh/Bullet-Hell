using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCASArrowLauncher : Bullet
{
    [SerializeField] GameObject arrow0;
    [SerializeField] GameObject arrow1;
    [SerializeField] GameObject arrow2;
    [SerializeField] GameObject arrow3;
    internal bool allowFire = false;
    [SerializeField] float angleLow = 30;
    Quaternion qOrigin;
    Quaternion q5;
    Quaternion q10;
    Quaternion q15;
    Quaternion qMax;

    // Start is called before the first frame update
    override protected void Start()
    {
        int mult = 1;
        /*
        base.Start();
        if (isRight)
        {
            angleLow += 360;
            q5 = Quaternion.Euler(0, 0, 5);
            q5 = Quaternion.Euler(0, 0, 10);
            q5 = Quaternion.Euler(0, 0, 15);
        }
        else
        {
            angleTop = angleTop * -1 + 360;
            angleLow *= -1;
            rotationalSpeed *= -1;
            float temp = angleTop;
            angleTop = angleLow;
            angleLow = temp;
            topLimit = 270;
            bottomLimit = 90;
            q5 = Quaternion.Euler(0, 0, -5);
            q5 = Quaternion.Euler(0, 0, -10);
            q5 = Quaternion.Euler(0, 0, -15);
        }
        */
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
            //Debug.Log(coords.localRotation.eulerAngles);
            //TurnTransform(GetRotationalSpeed());
            /*
             * values are the same but somehow the hashcode are different so they're not equal. that is why this is using quaternions to check if it has reached its rotation
            Debug.Log(coords.localRotation.eulerAngles.z == angleLow);
            Debug.Log(gameObject.name);
            Debug.Log(coords.localRotation.eulerAngles.z);
            Debug.Log(angleLow);
            float a = coords.localRotation.eulerAngles.z;
            float b = angleLow;
            float c = 30;
            float d = 30;
            Debug.Log(d == c);
            Debug.Log(a.GetHashCode());
            Debug.Log(b.GetHashCode());
            Debug.Log(c.GetHashCode());
            Debug.Log(d.GetHashCode());
            */
            //Debug.Log(coords.localRotation == qMax);
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
            Instantiate(arrow3, coords.position, coords.rotation);
            yield return new WaitForSeconds(recoil);
        }
        coords.localRotation = qOrigin;
    }

    internal void CommenceFire()
    {
        StartCoroutine(StartFire());
    }
}
