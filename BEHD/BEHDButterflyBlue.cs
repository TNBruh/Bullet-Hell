using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEHDButterflyBlue : Bullet
{
    [SerializeField] GameObject spawnButterflyYellow;
    [SerializeField] GameObject spawnButterflyWhite;
    [SerializeField] bool isInPlace = false;
    [SerializeField] float timeToPlace;
    [SerializeField] float recoil2 = 1;
    bool allowFire = true;
    bool allowFire2 = true;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        StartCoroutine(InPlace());
    }


    private void FixedUpdate()
    {
        if (!isInPlace)
        {
            //LocalNonLinearMovementTransform(new Vector2(-1, 0));
            MoveBulletXTransform(-1);
        }

        StartCoroutine(SpawnBullet());
        StartCoroutine(SpawnBullet2());
    }

    IEnumerator SpawnBullet()
    {
        if (allowFire && isInPlace)
        {
            allowFire = false;
            Instantiate(spawnButterflyYellow, coords.position, coords.rotation);
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
        yield break;
    }

    IEnumerator SpawnBullet2()
    {
        if (allowFire2 && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && isInPlace)
        {
            allowFire2 = false;
            Instantiate(spawnButterflyWhite, coords.position, coords.rotation);
            yield return new WaitForSeconds(recoil2);
            allowFire2 = true;
        }
        yield break;
    }

    IEnumerator InPlace()
    {
        yield return new WaitForSeconds(timeToPlace);
        isInPlace = true;
    }
}
