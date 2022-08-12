using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3Sakuya : Bullet
{
    [SerializeField] GameObject prisonCard;
    GameObject[] spawnedCards = new GameObject[4];
    GCT3PrisonSource[] cardScripts = new GCT3PrisonSource[4];
    [SerializeField] float sideStep = 1.5f;

    protected override void Start()
    {
        base.Start();
        LookAtObject(enemy.transform.position);
        Vector3 eAngle = SideSteppedTargeting(sideStep, enemy.transform.position);
        spawnedCards[0] = Instantiate(prisonCard, coords.position, coords.rotation * Quaternion.Euler(eAngle));
        spawnedCards[1] = Instantiate(prisonCard, coords.position, coords.rotation * Quaternion.Euler(-eAngle));
        spawnedCards[2] = Instantiate(prisonCard, coords.position, coords.rotation * Quaternion.Euler(eAngle*2f));
        spawnedCards[3] = Instantiate(prisonCard, coords.position, coords.rotation * Quaternion.Euler(-eAngle*2f));
        cardScripts[0] = spawnedCards[0].GetComponent("GCT3PrisonSource") as GCT3PrisonSource;
        cardScripts[1] = spawnedCards[1].GetComponent("GCT3PrisonSource") as GCT3PrisonSource;
        cardScripts[2] = spawnedCards[2].GetComponent("GCT3PrisonSource") as GCT3PrisonSource;
        cardScripts[3] = spawnedCards[3].GetComponent("GCT3PrisonSource") as GCT3PrisonSource;
    }
    private void FixedUpdate()
    {
        if (cardScripts[0].isDone && cardScripts[1].isDone && cardScripts[2].isDone && cardScripts[3].isDone)
        {
            Destroy(gameObject);
        }
    }
    /*
     * IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(recoil);
        LookAtObject(enemy.transform.position);
        eAngle = SideSteppedTargeting(angle, enemy.transform.position);
        qAnglePos = Quaternion.Euler(eAngle);
        qAngleNeg = Quaternion.Euler(0, 0, -eAngle.z);
        Instantiate(greenButterfly, coords.position, coords.rotation * qAngleNeg);
        Instantiate(greenButterfly, coords.position, coords.rotation * qAnglePos);
        Destroy(gameObject);
    }
     */




}
