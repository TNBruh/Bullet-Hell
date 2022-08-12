using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTFlower : Bullet
{
    Transform npc;

    protected override void Start()
    {
        base.Start();
        /*
        npc = GameObject.FindGameObjectWithTag("NPC").transform;
        masterTGC = GameObject.FindGameObjectWithTag("Bullet2").GetComponent<TGCMaster>();
        
        //Debug.Log("flower");
        masterTGC.AddInstance((Bullet)this);
        */

        npc = GameObject.FindGameObjectWithTag("Sakuya").transform;
        coords.position = npc.position;
        LookAtObject(enemy.transform.position);
    }

    private void FixedUpdate()
    {
        //Debug.Log(npc.name);
        LookAtObject(enemy.transform.position);
        coords.position = npc.position;
        if (coords.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
