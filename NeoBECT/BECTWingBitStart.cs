using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BECTWingBitStart : Bullet
{
    [SerializeField] Vector3 assignedPosition;
    [SerializeField] GameObject blueEgg;
    [SerializeField] GameObject greenEgg;
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
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, assignedPosition, (progPercent / 100));
        if (coords.localPosition == assignedPosition && !haveChecked)
        {
            coords.parent.GetComponent<BECTWingStart>().AddReady();
            haveChecked = true;
        }
    }
    internal void SpawnBullet()
    {
        if ((coords.position.x < 4.5 && coords.position.x > -4.5) && (coords.position.y < 4.8 && coords.position.y > -4.8))
        {
            if (spawnBlue)
            {
                Instantiate(blueEgg, coords.position, coords.rotation);
            }
            else
            {
                Instantiate(greenEgg, coords.position, coords.rotation);
            }
        }
        spawnBlue = !spawnBlue;
    }
}
