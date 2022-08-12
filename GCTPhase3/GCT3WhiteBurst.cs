using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3WhiteBurst : Bullet
{
    [SerializeField] GameObject whiteOrb;

    protected override void Start()
    {
        base.Start();
        LookAtObject(enemy.transform.position);
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        Instantiate(whiteOrb, coords.position, coords.rotation);
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, 14));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, 28));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, 42));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, 56));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, -14));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, -28));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, -42));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, -56));
        yield return new WaitForSeconds(recoil);
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, 7));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, -7));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, 21));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, -21));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, 35));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, -35));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, 49));
        Instantiate(whiteOrb, coords.position, coords.rotation * Quaternion.Euler(0, 0, -49));
        Destroy(gameObject);

    }

}
