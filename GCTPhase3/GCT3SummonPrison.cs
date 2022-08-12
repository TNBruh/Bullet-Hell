using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3SummonPrison : Bullet
{
    bool beginShrink = false;
    [SerializeField] float shrinkSpeed = 0.01f;
    [SerializeField] GameObject sakuya;
    internal bool isReady = false;
    [SerializeField] internal bool triggerSpawn = false;
    GCTP3 script;

    protected override void Start()
    {
        base.Start();

        script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP3)) as GCTP3;
        script.AddInstance((Bullet)this);
    }
    private void FixedUpdate()
    {
        MoveBulletYTransform();
        //coords.position += dirV * GetSpeed();
        if ((Mathf.Abs(coords.position.x) >= 4.5f || Mathf.Abs(coords.position.y) >= 4.8f) && !isReady)
        {
            boost = 0;
            isReady = true;
            script.countReadyPortal++;
        }
        //TurnTransform(GetRotationalSpeed());
        if (beginShrink)
        {
            coords.localScale = Vector3.MoveTowards(coords.localScale, new Vector3(), shrinkSpeed * Time.deltaTime);
            if (coords.localScale == new Vector3())
            {
                Destroy(gameObject);
            }
        }
        
        if (triggerSpawn)
        {
            triggerSpawn = false;
            Spawn();
        }
        

    }
    internal override void StopTime(bool isStopped)
    {
        base.StopTime(isStopped);
        Spawn();
    }

    internal void Spawn()
    {
        beginShrink = true;
        GameObject m = Instantiate(sakuya, coords.position, new Quaternion());
    }
}
