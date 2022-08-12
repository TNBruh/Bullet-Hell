using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEHDBurstCardYellow : Bullet
{
    [SerializeField] GameObject yellowCard;
    [SerializeField] float fSpread = 5;
    [SerializeField] float fAngle = 20;
    Quaternion dSpread;
    Quaternion dSpread2;
    Quaternion dProg;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        LookAtObject(enemy.transform.position);

        coords.rotation = coords.rotation * Quaternion.Euler(0, 0, fAngle);
        dSpread = Quaternion.Euler(0, 0, fSpread);
        dSpread2 = Quaternion.Euler(0, 0, -fSpread);
        dProg = Quaternion.Euler(0, 0, -fAngle / 2);
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        //20
        Instantiate(yellowCard, coords.position, coords.rotation);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread2);
        yield return new WaitForSeconds(recoil);
        coords.rotation *= dProg;
        //10
        Instantiate(yellowCard, coords.position, coords.rotation);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread2);
        yield return new WaitForSeconds(recoil);
        coords.rotation *= dProg;
        //0
        Instantiate(yellowCard, coords.position, coords.rotation);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread2);
        yield return new WaitForSeconds(recoil);
        coords.rotation *= dProg;
        //-10
        Instantiate(yellowCard, coords.position, coords.rotation);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread2);
        yield return new WaitForSeconds(recoil);
        coords.rotation *= dProg;
        //-20
        Instantiate(yellowCard, coords.position, coords.rotation);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread);
        Instantiate(yellowCard, coords.position, coords.rotation * dSpread2);
        Destroy(gameObject);
        yield break;
    }


}
