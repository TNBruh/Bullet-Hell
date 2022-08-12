using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEHDButterflyWhite : Bullet
{
    [SerializeField] GameObject whiteCard;
    private void FixedUpdate()
    {
        MoveBulletY();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string nem;
        if (string.Equals(collision.gameObject.tag, "Barrier") || string.Equals(collision.gameObject.tag, "OuterBarrier"))
        {
            nem = collision.gameObject.name;
            switch (nem)
            {
                case "BarrierTop":
                    Instantiate(whiteCard, coords.position, Quaternion.Euler(0, 0, 180));
                    break;
                case "BarrierBottom":
                    Instantiate(whiteCard, coords.position, Quaternion.Euler(0, 0, 0));
                    break;
                case "BarrierLeft":
                    Instantiate(whiteCard, coords.position, Quaternion.Euler(0, 0, -90));
                    break;
                case "BarrierRight":
                    Instantiate(whiteCard, coords.position, Quaternion.Euler(0, 0, 90));
                    break;
            }
            Destroy(gameObject);
        }
    }
}
