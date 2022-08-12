using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPButterfly2 : Bullet
{
    [SerializeField] BEDPButterflyBit[] leftWing;
    [SerializeField] BEDPButterflyBit[] rightWing;
    internal int countReady = 0;
    internal bool isReady = false;
    [SerializeField] internal float recoilLockdown = 0.04f;
    [SerializeField] internal float recoilShotdown = 1.2f;
    [SerializeField] internal float recoilDualAimed = 1f;
    [SerializeField] bool act = false;
    [SerializeField] bool act1 = false;
    [SerializeField] bool act2 = false;
    [SerializeField] bool act3 = false;
    [SerializeField] bool act4 = false;
    [SerializeField] internal bool rightAiming = true;
    [SerializeField] float splashRecoil = 1.4f;
    [SerializeField] GameObject fountain;

    protected override void Start()
    {
        base.Start();

    }

    IEnumerator ReadyCheck()
    {
        yield return new WaitUntil(() => countReady == leftWing.Length + rightWing.Length);
        isReady = true;
    }

    private void FixedUpdate()
    {
        if (act)
        {
            act = false;
            //LockedShotdown();
            //DualAimed();
            //LaserShow();
            SplashAttack();
        }
        if (act1)
        {
            act1 = false;
            //LockedShotdown();
            DualAimed();
            //LaserShow();
            //SplashAttack();
        }
        if (act2)
        {
            act2 = false;
            //LockedShotdown();
            //DualAimed();
            LaserShow();
            //SplashAttack();
        }
        if (act3)
        {
            act3 = false;
            LockedShotdown();
            //DualAimed();
            //LaserShow();
            //SplashAttack();
        }
        if (act4)
        {
            act4 = false;
            //LockedShotdown();
            //DualAimed();
            //LaserShow();
            //SplashAttack();
            Lockdown();
        }

    }

    internal Coroutine Lockdown()
    {
        return StartCoroutine(LockdownEnumerated());
    }

    internal Coroutine LockedShotdown()
    {
        return StartCoroutine(LockedShotdownEnumerated());
    }

    internal Coroutine DualAimed()
    {
        return StartCoroutine(DualProgressiveEnumerated());
    }

    internal Coroutine SplashAttack()
    {
        return StartCoroutine(SplashEnumerated());
    }

    internal void LaserShow()
    {
        if (rightAiming)
        {
            for (int i = leftWing.Length - 1; i >= 0; i--)
            {
                leftWing[i].LaserShowStrayAttack();
                rightWing[i].LaserShowAttack();

                //rightWing[i].LaserShowStrayAttack();
            }
        }
        else
        {

            for (int i = leftWing.Length - 1; i >= 0; i--)
            {
                leftWing[i].LaserShowAttack();
                rightWing[i].LaserShowStrayAttack();
            }
        }
        rightAiming = !rightAiming;
    }

    IEnumerator LockdownEnumerated()
    {

        for (int i = leftWing.Length-1; i >= 0; i--)
        {
            leftWing[i].LockdownAttack();
            rightWing[i].LockdownAttack();
            yield return new WaitForSeconds(recoilLockdown);
        }
    }

    IEnumerator LockedShotdownEnumerated()
    {
        for (int i = leftWing.Length*2 - 1; i >= 0; i--)
        {
            if (i % 2 == 0)
            {
                rightWing[Mathf.RoundToInt(i / 2)].DualUnaimedAttack();
                //rightWing[Mathf.RoundToInt(i / 2)].DualProgressiveAttack();
            }
            else
            {
                leftWing[Mathf.RoundToInt(i / 2)].DualUnaimedAttack();

                //leftWing[Mathf.RoundToInt(i / 2)].DualProgressiveAttack();
            }
            yield return new WaitForSeconds(recoilShotdown);
        }
    }

    IEnumerator DualProgressiveEnumerated()
    {
        for (int i = leftWing.Length * 2 - 1; i >= 0; i--)
        {
            if (i % 2 == 0)
            {
                //rightWing[Mathf.RoundToInt(i / 2)].DualUnaimedAttack();
                rightWing[Mathf.RoundToInt(i / 2)].DualProgressiveAttack();
            }
            else
            {
                //leftWing[Mathf.RoundToInt(i / 2)].DualUnaimedAttack();

                leftWing[Mathf.RoundToInt(i / 2)].DualProgressiveAttack();
            }
            yield return new WaitForSeconds(recoilDualAimed);
        }
    }

    IEnumerator SplashEnumerated()
    {
        Transform obj;
        //Debug.Log("bruh");
        for (int i = 0; i < leftWing.Length * 2 - 1; i++)
        {
            if (i % 2 == 0)
            {
                obj = leftWing[Mathf.RoundToInt(i / 2)].transform;
                //rightWing[Mathf.RoundToInt(i / 2)].DualUnaimedAttack();
                //rightWing[Mathf.RoundToInt(i / 2)].DualProgressiveAttack();
                Instantiate(fountain, obj.position, leftWing[Mathf.RoundToInt(i / 2)].originAim);
            }
            else
            {
                obj = rightWing[Mathf.RoundToInt(i / 2)].transform;
                //leftWing[Mathf.RoundToInt(i / 2)].DualUnaimedAttack();
                //leftWing[Mathf.RoundToInt(i / 2)].DualProgressiveAttack();
                Instantiate(fountain, obj.position, rightWing[Mathf.RoundToInt(i / 2)].originAim);
            }
            yield return new WaitForSeconds(splashRecoil);
        }
    }




}
