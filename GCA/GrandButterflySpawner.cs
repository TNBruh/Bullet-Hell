using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandButterflySpawner : Bullet
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] float haltFire = 0.4f;
    Quaternion adjustD = Quaternion.Euler(0, 0, 90);
    bool allowFire = false;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(LetFire());
    }

    private void FixedUpdate()
    {
        MoveBulletY();
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        if (allowFire)
        {
            allowFire = false;
            Instantiate(spawnObject, coords.position, coords.rotation * adjustD);

            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
        yield break;
    }

    IEnumerator LetFire()
    {
        yield return new WaitForSeconds(haltFire);
        allowFire = true;
    }
}
