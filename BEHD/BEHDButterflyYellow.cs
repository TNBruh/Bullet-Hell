using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEHDButterflyYellow : StraightBullet
{
    [SerializeField] GameObject burstYellow;
    GameObject collided;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = collision.gameObject;
        if (string.Equals(collided.tag, "Barrier") || string.Equals(collided.tag, "OuterBarrier"))
        {
            
            if (string.Equals("BarrierTop", collided.name) || string.Equals("BarrierBottom", collided.name))
            {
                Instantiate(burstYellow, new Vector3(coords.position.x, collided.transform.position.y), coords.rotation);
                Destroy(gameObject);
            }
            else
            {
                Instantiate(burstYellow, new Vector3(collided.transform.position.x, coords.position.y), coords.rotation);
                Destroy(gameObject);
            }
        }
    }


}
