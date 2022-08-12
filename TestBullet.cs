using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TestBullet : Bullet
{
    [SerializeField] GameObject shot;
    [SerializeField] int reverseRotation = 1;
    bool allowFire = true;

    private void FixedUpdate()
    {
        //body.rotation + 120 * Time.fixedDeltaTime
        Turn(120);
        StartCoroutine(Fire());

        //Turn(12000);
    }

    IEnumerator Fire()
    {
        if (allowFire)
        {
            //Debug.LogWarning("Fire() called");
            allowFire = false;
            Instantiate(shot, gameObject.transform.position, gameObject.transform.rotation);
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
        
    }
}
