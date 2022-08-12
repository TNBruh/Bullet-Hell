using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BECTWing : Bullet
{
    [SerializeField] bool isRight = true;
    [SerializeField] float angleTop = 7;
    [SerializeField] float angleLow = -25;
    bool allowFire = true;
    BECTWingBit[] wingBits;
    float topLimit = 90;
    float bottomLimit = 270;

    override protected void Start()
    {
        base.Start();
        if (isRight)
        {
            angleLow += 360;
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
        }
        wingBits = new BECTWingBit[coords.childCount];
        for (int i = 0; i < coords.childCount; i++)
        {
            wingBits[i] = coords.GetChild(i).gameObject.GetComponent<BECTWingBit>();
        }
    }
    private void FixedUpdate()
    {
        
        if (((coords.localRotation.eulerAngles.z >= angleTop && (coords.localRotation.eulerAngles.z <= topLimit)) && rotationalSpeed > 0) || ((coords.localRotation.eulerAngles.z <= angleLow && coords.localRotation.eulerAngles.z >= bottomLimit) && rotationalSpeed < 0))
        {
            rotationalSpeed *= -1;
        }

        TurnTransform(GetRotationalSpeed());
        //Debug.Log((coords.rotation.eulerAngles.z));
        //Debug.Log((angleLow));

        //ClampedTurnTransform(rotationalSpeed * rotationalBoost, 7, -25);
        //Debug.Log(coords.localRotation.eulerAngles.z);
        //Debug.Log(coords.rotation.eulerAngles.z);
        /*
        if (coords.rotation.eulerAngles.z == 7 || coords.rotation.eulerAngles.z == 335)
        {
            rotationalSpeed *= -1;
        }
        */
        StartCoroutine(SpawnBullet());

    }

    IEnumerator SpawnBullet()
    {
        if (allowFire)
        {
            allowFire = false;
            foreach (BECTWingBit i in wingBits)
            {
                i.SpawnBullet();
            }
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
        yield break;
    }
}
