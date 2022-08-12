using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPWing : Bullet
{
    [SerializeField] bool isRight = true;
    [SerializeField] float angleTop = 7;
    [SerializeField] float angleLow = -25;
    bool allowFire = true;
    bool allowFlap = false;
    BEDPWingBit[] wingBits;
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
        wingBits = new BEDPWingBit[coords.childCount];
        for (int i = 0; i < coords.childCount; i++)
        {
            wingBits[i] = coords.GetChild(i).gameObject.GetComponent<BEDPWingBit>();
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

    internal void AddReady()
    {
        readyBits.Add(true);
        allowFlap = readyBits.Count >= transform.childCount;
        coords.parent.GetComponent<BEDPButterfly>().allowLaunch = allowFlap;
    }
}
