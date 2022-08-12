using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT4ArrowGhost : Bullet
{
    [SerializeField] float addProgPercent = 0.01f;
    [SerializeField] float intervalAdd = 0.2f;
    [SerializeField] float delayStartAim = 1.4f;
    [SerializeField] float delayRegisterEPos = 1.4f;
    [SerializeField] float minDist = 0.02f;
    [SerializeField] float tAccel = 0.4f;
    Vector3 playerPos;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(BeginProgressiveAim());
    }

    private void FixedUpdate()
    {
        MoveBulletY();
    }

    IEnumerator RegisterEnemyPosition()
    {
        yield return new WaitForSeconds(delayRegisterEPos);
        playerPos = enemy.transform.position;
    }

    IEnumerator BeginProgressiveAim()
    {
        yield return StartCoroutine(RegisterEnemyPosition());
        float totalProg = 0;
        yield return new WaitForSeconds(delayStartAim);
        boost *= 1.2f;
        while (totalProg != 1 && /*coords.position != playerPos*/ !(Vector3.Distance(coords.position, playerPos) <= minDist))
        {
            totalProg = Mathf.Clamp(totalProg + addProgPercent, 0, 1);
            NeoHoming(playerPos, totalProg);
            yield return new WaitForSeconds(intervalAdd);
        }
    }

}
