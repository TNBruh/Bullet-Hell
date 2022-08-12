using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTNPCSummon : Bullet
{
    [SerializeField] float direction;
    [SerializeField] GameObject marisa;
    [SerializeField] GameObject laser;
    [SerializeField] float shrinkSpeed = 0.01f;
    bool beginShrink = false;
    Vector3 dirV;

    [SerializeField] bool triggerSpawn = false;

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        MoveBulletYTransform();
        //coords.position += dirV * GetSpeed();
        if (Mathf.Abs(coords.position.x) >= 4.5f || Mathf.Abs(coords.position.y) >= 4.8f)
        {
            boost = 0;
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
        /*
        if (triggerSpawn)
        {
            triggerSpawn = false;
            Spawn();
        }
        */
    }

    internal void Spawn()
    {
        beginShrink = true;
        GameObject m = Instantiate(marisa, coords.position, new Quaternion());
        GCTSparkPoint2 g = Instantiate(laser, coords.position, new Quaternion()).GetComponent(typeof(GCTSparkPoint2)) as GCTSparkPoint2;
        g.marisa = m;

    }


}
