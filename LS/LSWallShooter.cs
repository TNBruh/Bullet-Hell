using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSWallShooter : Bullet
{
    [SerializeField] GameObject wallOrb;
    [SerializeField] GameObject wallOrbV;
    bool allowFire = true;
    [SerializeField] float yPos;
    Quaternion q180 = Quaternion.Euler(new Vector3(0, 0, 180));
    Quaternion qWallV;
    int count = 0;

    Vector3 spawnCoords1;
    Vector3 spawnCoords2;
    Vector3 spawnCoords3;
    Vector3 spawnCoords4;


    override protected void Start()
    {
        base.Start();
        spawnCoords1 = new Vector3(0, yPos);
        spawnCoords2 = new Vector3(0, -yPos);
        spawnCoords3 = new Vector3(yPos, 0);
        spawnCoords4= new Vector3(-yPos, 0);
        qWallV = wallOrbV.transform.rotation;

    }

    private void FixedUpdate()
    {
        StartCoroutine(SpawnWall());
    }

    IEnumerator SpawnWall()
    {
        if (allowFire)
        {
            allowFire = false;
            yield return new WaitForSeconds(recoil);

            Instantiate(wallOrb, spawnCoords2, qZero);
            Instantiate(wallOrb, spawnCoords1, q180);
            if (count == 4)
            {
                Instantiate(wallOrbV, spawnCoords3, qWallV);
                Instantiate(wallOrbV, spawnCoords4, qWallV * q180);
                count = 0;
            }
            count++;
            allowFire = true;
        }
    }
}
