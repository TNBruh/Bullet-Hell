using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPFlowerSpawner : Bullet
{
    GameObject[] flowerSpawns;
    List<BEDPFlowerSpawn> spawns = new List<BEDPFlowerSpawn>();
    int countReady = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        //flowerSpawns = new GameObject[coords.childCount];
        /*
        foreach (GameObject i in flowerSpawns)
        {

            //spawns.Add(i.GetComponent<BEDPFlowerSpawn>());
            //Instantiate(i, coords);
        }
        */
        for (int i = 0; i < coords.childCount; i++)
        {
            //flowerSpawns[i] = coords.GetChild(i).gameObject;
            spawns.Add(coords.GetChild(i).gameObject.GetComponent<BEDPFlowerSpawn>());
        }
        
    }

    internal void SetReady()
    {
        countReady++;
        //Debug.Log(countReady);
        if (countReady == coords.childCount)
        {
            foreach (BEDPFlowerSpawn i in spawns)
            {
                i.StartSpawn();
            }
            Destroy(gameObject);
        }
    }
}
