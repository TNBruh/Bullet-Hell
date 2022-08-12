using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEHD_NPC : Bullet
{
    [SerializeField] GameObject spawnButterflyYellow;
    [SerializeField] float waitActivation = 1.4f;
    [SerializeField] Transform npcCoords;
    bool allowFire = false;
    bool allowFire2 = true;

    override protected void Start()
    {
        base.Start();
        LookAtObject(enemy.transform.position);
        StartCoroutine(ActivateFire());
        npcCoords = GameObject.FindGameObjectWithTag("NPC").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        coords.position = npcCoords.position;
        LookAtObject(enemy.transform.position);
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        if (allowFire2 && allowFire)
        {
            allowFire2 = false;
            Instantiate(spawnButterflyYellow, coords.position, coords.rotation);
            Instantiate(spawnButterflyYellow, coords.position, coords.rotation * Quaternion.Euler(0, 0, 30));
            Instantiate(spawnButterflyYellow, coords.position, coords.rotation * Quaternion.Euler(0, 0, -30));
            yield return new WaitForSeconds(recoil);
            allowFire2 = true;
        }
        yield break;
    }

    IEnumerator ActivateFire()
    {
        yield return new WaitForSeconds(waitActivation);
        allowFire = true;
    }


}
