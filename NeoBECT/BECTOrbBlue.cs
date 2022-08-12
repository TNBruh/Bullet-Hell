using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BECTOrbBlue : Bullet
{
    [SerializeField] GameObject blueCard;
    override protected void Start()
    {
        base.Start();
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(recoil);
        Instantiate(blueCard, coords.position, qZero);
        Instantiate(blueCard, coords.position, qZero * Quaternion.Euler(0, 0, 90));
        Instantiate(blueCard, coords.position, qZero * Quaternion.Euler(0, 0, 180));
        Instantiate(blueCard, coords.position, qZero * Quaternion.Euler(0, 0, 270));
        Destroy(gameObject);
    }
}
