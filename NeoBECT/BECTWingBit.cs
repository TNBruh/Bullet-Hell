using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BECTWingBit : Bullet
{
    [SerializeField] Vector3 assignedPosition;
    [SerializeField] GameObject blueEgg;
    [SerializeField] GameObject greenEgg;
    [SerializeField] float progPercent = 1;
    internal bool spawnBlue = true;
    internal bool allowFire = true;

    override protected void Start()
    {
        base.Start();
    }
    private void FixedUpdate()
    {
        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, assignedPosition, (progPercent / 100));
        /*
        if ((coords.position.x < 4.5 && coords.position.x > -4.5) && (coords.position.y < 4.8 && coords.position.y > -4.8))
        {
            StartCoroutine(SpawnBullet());
        }
        */
    }
    /*
    IEnumerator SpawnBullet()
    {
        if (allowFire)
        {
            allowFire = false;
            Instantiate(blueEgg, coords.position, coords.rotation);
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
        yield break;
    }
    */
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
