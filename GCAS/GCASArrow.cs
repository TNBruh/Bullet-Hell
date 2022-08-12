using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCASArrow : Bullet
{
    [SerializeField] GameObject cyanOrb;
    Quaternion q90 = Quaternion.Euler(0, 0, 90);
    Quaternion q180 = Quaternion.Euler(0, 0, 180);
    Quaternion q270 = Quaternion.Euler(0, 0, 270);

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        MoveBulletY();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet2")
        {
            Instantiate(cyanOrb, coords.position, coords.rotation);
            Instantiate(cyanOrb, coords.position, coords.rotation * q90);
            Instantiate(cyanOrb, coords.position, coords.rotation * q180);
            Instantiate(cyanOrb, coords.position, coords.rotation * q270);
            Destroy(gameObject);
        }
    }
}
