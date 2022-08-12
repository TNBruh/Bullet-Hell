using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTStarfall : Bullet
{
    [SerializeField] Vector3 startPos;
    [SerializeField] float resetYPos;
    Vector3 resetPos;
    [SerializeField] float endYPos = -8;
    Vector3 endPos;
    protected override void Start()
    {
        base.Start();
        coords.position = startPos;

        GCTP2 script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP2)) as GCTP2;
        script.AddInstance((Bullet)this);

        resetPos = new Vector3(startPos.x, resetYPos);

        endPos = new Vector3(coords.position.x, endYPos);
    }

    private void FixedUpdate()
    {
        coords.position = Vector3.MoveTowards(coords.position, endPos, GetSpeed());
        Turn(GetRotationalSpeed());
        if (coords.position == endPos)
        {
            coords.position = resetPos;
        }
    }
}
