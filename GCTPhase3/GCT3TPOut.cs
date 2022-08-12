using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GCT3TPOut : Bullet
{
    [SerializeField] GameObject redKnife;
    [SerializeField] GameObject blueKnife;
    [SerializeField] float rotation;
    //[SerializeField] float goBackDuration = 1.2f;
    [SerializeField] float dist = 1.2f;
    Vector3 originVect;


    protected override void Start()
    {
        base.Start();
        boost = 0;
        coords.rotation = Quaternion.Euler(0, 0, rotation);
        StartCoroutine(StopMovement());
    }

    private void FixedUpdate()
    {
        MoveBulletYTransform(-1);
    }

    internal void Fire(bool isRed)
    {
        if (isRed)
        {
            Instantiate(redKnife, coords.position, coords.rotation);
            Instantiate(redKnife, coords.position, Quaternion.Euler(0, 0, 60) * coords.rotation);
            Instantiate(redKnife, coords.position, Quaternion.Euler(0, 0, 120) * coords.rotation);
            Instantiate(redKnife, coords.position, Quaternion.Euler(0, 0, 180) * coords.rotation);
            Instantiate(redKnife, coords.position, Quaternion.Euler(0, 0, 240) * coords.rotation);
            Instantiate(redKnife, coords.position, Quaternion.Euler(0, 0, 300) * coords.rotation);
        }
        else
        {
            Instantiate(blueKnife, coords.position, coords.rotation);
            Instantiate(blueKnife, coords.position, Quaternion.Euler(0, 0, 60) * coords.rotation);
            Instantiate(blueKnife, coords.position, Quaternion.Euler(0, 0, 120) * coords.rotation);
            Instantiate(blueKnife, coords.position, Quaternion.Euler(0, 0, 180) * coords.rotation);
            Instantiate(blueKnife, coords.position, Quaternion.Euler(0, 0, 240) * coords.rotation);
            Instantiate(blueKnife, coords.position, Quaternion.Euler(0, 0, 300) * coords.rotation);
        }
    }

    IEnumerator StopMovement()
    {
        yield return new WaitUntil(() => coords.position == enemy.transform.position);
        boost = 1;
        yield return new WaitUntil(() => Vector3.Distance(enemy.transform.position, coords.position) >= dist);
        boost = 0;
    }
}
