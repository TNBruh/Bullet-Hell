using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    /*
     * TODO
     * CHECK[V] find a way to handle multiple unique coroutines of bullet shots. 
     * CHECK[V] make a default fire function.
     */

    List<Coroutine> ExecuteFires = new List<Coroutine>();

    private void FixedUpdate()
    {
        MovePlayer();
        DefaultFireHandler();
    }
    
    void MovePlayer()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(xMove, yMove);

        MoveCharacter(movement);
    }

    virtual internal void DefaultFireHandler()
    {
        GameObject[] bulletList = new GameObject[bulletSpawners.Length];
        int[] indexes = new int[bulletSpawners.Length];
        for (int i = 0; i < bulletList.Length; i++)
        {
            bulletList[i] = BulletObjects[0];
            indexes[i] = i;
        }
        FireHandler(bulletList, indexes);

    }

    virtual internal void FireHandler(string[] bulletNames, int[] spawnerIndexes)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int ind = Mathf.Min(bulletNames.Length, spawnerIndexes.Length);

            for (int i = 0; i < ind; i++)
            {
                Fire(bulletNames[i], spawnerIndexes[i]);
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            foreach (Coroutine exe in ExecuteFires)
            {
                StopCoroutine(exe);
                ExecuteFires.Remove(exe);
            }
        }
    }
    
    internal void Fire(string bulletName, int spawnerIndex)
    {
        GameObject BulletObject;
        Bullet script;

        
        foreach (GameObject bullet in BulletObjects)
        {
            if (bullet.transform.name == bulletName)
            {
                BulletObject = bullet;
                script = bullet.GetComponent<Bullet>();

                ExecuteFires.Add(StartCoroutine(TrueFire(BulletObject, bulletSpawners[spawnerIndex], script.recoil)));
                break;
            }
        }

    }

    virtual internal void FireHandler(GameObject[] bullets, int[] spawnerIndexes)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int ind = Mathf.Min(bullets.Length, spawnerIndexes.Length);

            for (int i = 0; i < ind; i++)
            {
                Fire(bullets[i], spawnerIndexes[i]);
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            foreach (Coroutine exe in ExecuteFires)
            {
                StopCoroutine(exe);
                ExecuteFires.Remove(exe);
            }
        }
    }

    internal void Fire(GameObject bullet, int spawnerIndex)
    {
        Bullet script = bullet.GetComponent<Bullet>();
        ExecuteFires.Add(StartCoroutine(TrueFire(bullet, bulletSpawners[spawnerIndex], script.recoil)));

    }

    IEnumerator TrueFire(GameObject bullet, Vector2 spawnPoint, float recoil)
    {
        while (true)
        {
            if (!timeStopped)
            {
                Instantiate(bullet, spawnPoint, new Quaternion());
            }
            yield return new WaitForSeconds(recoil);
        }
    }

}
