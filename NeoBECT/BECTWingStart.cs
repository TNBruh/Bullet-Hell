using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BECTWingStart : Bullet
{
    [SerializeField] bool isRight = true;
    [SerializeField] float angleTop = 7;
    [SerializeField] float angleLow = -25;
    bool allowFire = true;
    bool allowFlap = false;
    BECTWingBitStart[] wingBits;
    List<bool> readyBits = new List<bool>();
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
        wingBits = new BECTWingBitStart[coords.childCount];
        for (int i = 0; i < coords.childCount; i++)
        {
            wingBits[i] = coords.GetChild(i).gameObject.GetComponent<BECTWingBitStart>();
        }
    }
    private void FixedUpdate()
    {

        if (((coords.localRotation.eulerAngles.z >= angleTop && (coords.localRotation.eulerAngles.z <= topLimit)) && rotationalSpeed > 0) || ((coords.localRotation.eulerAngles.z <= angleLow && coords.localRotation.eulerAngles.z >= bottomLimit) && rotationalSpeed < 0))
        {
            rotationalSpeed *= -1;
        }

        if (allowFlap)
        {
            TurnTransform(GetRotationalSpeed());

            StartCoroutine(SpawnBullet());
        }
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

    }

    IEnumerator SpawnBullet()
    {
        if (allowFire)
        {
            allowFire = false;
            foreach (BECTWingBitStart i in wingBits)
            {
                i.SpawnBullet();
            }
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
        yield break;
    }

    internal void AddReady()
    {
        readyBits.Add(true);
        allowFlap = readyBits.Count >= transform.childCount;
        coords.parent.GetComponent<BECTBodyStart>().allowLaunch = allowFlap;
    }
}
