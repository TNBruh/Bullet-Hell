using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BEDPButterflyLauncher : Bullet
{
    [SerializeField] float bulletDist = 10;
    [SerializeField] GameObject redButterfly;
    [SerializeField] GameObject purpleOrb;
    [SerializeField] GameObject redOrb;
    [SerializeField] float recoil2;
    [SerializeField] int maxAbsorb = 10;
    [SerializeField] string targetTag = "Bullet4";
    bool allowFire = true;
    float currentRotation = 0;
    int countAbsorb = 0;
    int count = 0;
    int totalAbsorb;
    Transform child1;
    Transform child2;
    Transform child3;
    Transform enemyLoc;


    override protected void Start()
    {
        totalAbsorb = maxAbsorb * 3;
        base.Start();
        /*
        spawnLoc1 = RotatePoint(currentRotation, mainSpawnLoc);
        spawnLoc2 = RotatePoint(120 + currentRotation, mainSpawnLoc);
        spawnLoc3 = RotatePoint(240 + currentRotation, mainSpawnLoc);
        */
        child1 = coords.GetChild(0);
        child2 = coords.GetChild(1);
        child3 = coords.GetChild(2);
    }

    private void FixedUpdate()
    {
        /*
        spawnLoc1 = RotatePoint(currentRotation, mainSpawnLoc);
        spawnLoc2 = RotatePoint(120 + currentRotation, mainSpawnLoc);
        spawnLoc3 = RotatePoint(240 + currentRotation, mainSpawnLoc);
        */
        StartCoroutine(SpawnBullet());
        TurnTransform(GetRotationalSpeed());
    }

    IEnumerator SpawnBullet()
    {
        if (allowFire)
        {
            allowFire = false;
            while (count < maxAbsorb)
            {
                if (count%2 == 0)
                {
                    
                    Instantiate(redOrb, child1.position, child1.rotation);
                    Instantiate(redOrb, child2.position, child2.rotation);
                    Instantiate(redOrb, child3.position, child3.rotation);
                    
                }
                else
                {
                    Instantiate(purpleOrb, child1.position, child1.rotation);
                    Instantiate(purpleOrb, child2.position, child2.rotation);
                    Instantiate(purpleOrb, child3.position, child3.rotation);
                }
                yield return new WaitForSeconds(recoil);
                count++;
            }
            count = 0;
            yield return new WaitUntil(() => countAbsorb == totalAbsorb);
            countAbsorb = 0;
            //LookAtObject(enemy.transform.position);
            Instantiate(redButterfly, coords.position, coords.rotation);
            yield return new WaitForSeconds(recoil2);
            allowFire = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (string.Equals(collided.tag, targetTag))
        {
            Destroy(collided);
            countAbsorb++;
        }
    }
}
