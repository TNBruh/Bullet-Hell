using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawner : MonoBehaviour
{
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning("enters");
        GameObject collided = collision.gameObject;
        if (collided.tag == "Bullet")
        {
            Destroy(collided);
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        Destroy(collided);
        /*
        if (collided.tag != "Bullet")
        {
            Destroy(collided);
        }*/
    }
}
