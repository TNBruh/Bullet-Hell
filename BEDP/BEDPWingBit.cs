using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPWingBit : Bullet
{
    [SerializeField] Vector3 assignedPosition;
    [SerializeField] float progPercent = 1;
    internal bool haveChecked = false;

    internal bool spawnBlue = true;

    protected override void Start()
    {
        base.Start();
        assignedPosition = coords.localPosition;
        coords.localPosition = vectZero;
    }
    private void FixedUpdate()
    {
        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, assignedPosition, (progPercent / 100));
        if (!haveChecked)
        {
            if (coords.localPosition == assignedPosition)
            {
                coords.parent.GetComponent<BEDPWing>().AddReady();
                haveChecked = true;
            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, assignedPosition, (progPercent / 100));
            }
        }
    }
}
