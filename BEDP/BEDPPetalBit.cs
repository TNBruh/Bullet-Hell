using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPPetalBit : Bullet
{
    [SerializeField] Vector3 assignedPosition;
    //[SerializeField] GameObject orb;
    [SerializeField] float progPercent;
    [SerializeField] GameObject redOrb;
    string bulletName;
    int count;
    internal bool inPlace = false;
    bool allowFire = true;

    protected override void Start()
    {
        base.Start();
        assignedPosition = coords.localPosition;
        LookAtObject(assignedPosition);
        coords.localPosition = vectZero;
    }
    private void FixedUpdate()
    {
        if (!inPlace)
        {
            if (coords.localPosition != assignedPosition)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, assignedPosition, (progPercent / 100));
            }
            else if (coords.localPosition == assignedPosition)
            {
                inPlace = true;
                coords.parent.GetComponent<BEDPFlower>().SetReady();
            }
        }
    }

    internal void SpawnBullet()
    {
        if ((coords.position.x < 4.5 && coords.position.x > -4.5) && (coords.position.y < 4.8 && coords.position.y > -4.8))
        {
            Instantiate(redOrb, coords.position, coords.rotation);
        }
    }

    /*
    IEnumerator SpawnBullet()
    {
        if (allowFire)
        {
            allowFire = false;
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        orb = collision.gameObject;
        if (string.Equals(orb.tag, "Bullet"))
        {
            bulletName = orb.name;
            switch (bulletName)
            {
                //homes
                case "BEDPOrbRed":
                    break;
                //x
                case "BEDPOrbOrange":
                    break;
                //+
                case "BEDPOrbBlueDark":
                    break;
                //white
                case "BEDPOrbBlue":
                    break;
                //aims
                case "BEDPOrbYellow":
                    break;
                //bounces
                case "BEDPOrbPurple":
                    break;
                //limit
                case "BEDPOrbGreen":
                    break;

            }
        }
    }
    */


}
