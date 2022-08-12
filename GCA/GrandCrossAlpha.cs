using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandCrossAlpha : Bullet
{
    [SerializeField] GameObject spawnObject;
    bool allowFire = true;
    Quaternion ninetyD = Quaternion.Euler(0, 0, 90);
    Quaternion hundredeightyD = Quaternion.Euler(0, 0, 180);
    Quaternion twohundredseventyD = Quaternion.Euler(0, 0, 270);
    Quaternion hundredtwentyD = Quaternion.Euler(0, 0, 120);
    Quaternion twohundredfortyD = Quaternion.Euler(0, 0, 240);

    private void FixedUpdate()
    {
        Turn(rotationalSpeed);
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        if (allowFire)
        {
            allowFire = false;
            Instantiate(spawnObject, coords.position, coords.rotation);
            Instantiate(spawnObject, coords.position, coords.rotation * ninetyD);
            Instantiate(spawnObject, coords.position, coords.rotation * hundredeightyD);
            Instantiate(spawnObject, coords.position, coords.rotation * twohundredseventyD);

            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
        yield break;
    }
}
