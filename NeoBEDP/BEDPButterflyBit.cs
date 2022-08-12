using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPButterflyBit : Bullet
{
    internal Vector3 originPos;
    [SerializeField] Vector3 lockPosition = new Vector3(2, -3, 0);
    internal bool isReady = false;
    [SerializeField] float progPercent = 0.04f;
    internal float totalProgPercent = 0;
    internal int progMult = 0;
    GameObject laserChild;
    Vector3 laserChildSize;
    BEDPLaser laserScript;
    BEDPButterfly2 butterflyScript;
    internal Quaternion originAim;
    [SerializeField] internal float progSpeed = 0.05f;
    internal bool isRight = false;
    internal float rotationMultiplier = 0;
    [SerializeField] float laserShowDeaccel = 0.4f;
    [SerializeField] float firingTime = 2f;
    [SerializeField] int laserShowRepeat = 3;
    [SerializeField] float laserShowRecoil = 0.2f;
    [SerializeField] float lockdownRecoil = 0.08f;
    [SerializeField] BEDPButterfly2 butterfly;
    [SerializeField] float laserShowDuration = 6f;
    internal bool endCycle = false;
    Quaternion startAngle;
    float startAngleEuler;
    Quaternion endAngle;
    float endAngleEuler;
    bool isLocking = false;

    [SerializeField] bool act = false;

    protected override void Start()
    {
        base.Start();
        laserChild = coords.transform.GetChild(0).gameObject;
        laserChildSize = laserChild.transform.localScale;
        laserScript = laserChild.GetComponent("BEDPLaser") as BEDPLaser;

        originPos = coords.localPosition;

        LookAtObject(transform.parent.localPosition);
        coords.rotation *= Quaternion.Euler(0, 0, 180);
        originAim = coords.rotation;

        coords.localPosition = new Vector3();

        if (coords.localPosition.x - originPos.x < 0)
        {
            isRight = true;
        }
        if (!isRight)
        {
            lockPosition.x = -1 * Mathf.Abs(lockPosition.x);
        }



        StartCoroutine(ReadyCheck());
    }

    private void FixedUpdate()
    {
        coords.localPosition = Vector3.MoveTowards(coords.localPosition, originPos, progSpeed);
        coords.rotation *= Quaternion.Euler(0, 0, GetRotationalSpeed() * rotationMultiplier);
        if (isLocking)
        {
            //coords.rotation = Quaternion.SlerpUnclamped(startAngle, endAngle, -totalProgPercent);
            coords.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(startAngleEuler, endAngleEuler, totalProgPercent));
            totalProgPercent = Mathf.Clamp01(totalProgPercent + progPercent * Time.deltaTime * progMult);
        }
    }

    IEnumerator ReadyCheck()
    {
        yield return new WaitUntil(() => coords.localPosition == originPos);

        //startAngle = Quaternion.Euler(0, 0, Mathf.Round(EnemyAngle(coords.parent.position + new Vector3(0, 1)).eulerAngles.z));
        //endAngle = Quaternion.Euler(0, 0, Mathf.Round(EnemyAngle(lockPosition).eulerAngles.z));
        startAngle = EnemyAngle(coords.parent.position + new Vector3(0, 1));
        endAngle = EnemyAngle(lockPosition);

        startAngleEuler = startAngle.eulerAngles.z;
        endAngleEuler = endAngle.eulerAngles.z;




        if (isRight)
        {
            endAngleEuler = endAngleEuler - 360;
        }
        else
        {
            endAngleEuler = endAngleEuler + 360;
        }

        isReady = true;
        butterfly.countReady++;
        progSpeed = 0;
    }

    internal void DualProgressiveAttack()
    {
        StartCoroutine(DualProgressive());
    }

    internal void LaserShowAttack()
    {
        //StartCoroutine(LaserShowAimed());
        StartCoroutine(NeoLaserShowAimed());
    }
    
    internal void LaserShowStrayAttack()
    {

        //StartCoroutine(LaserShowStrayAimed());
        StartCoroutine(NeoLaserShowStrayAimed());
    }

    internal void LockdownAttack()
    {
        StartCoroutine(Lockdown());
    }

    internal void DualUnaimedAttack()
    {
        StartCoroutine(DualUnaimed());
    }

    internal int CheckDirection()
    {
        Vector3 direction = enemy.transform.position - coords.position;
        direction = RotatePoint(-coords.rotation.eulerAngles.z, direction);
        if (direction.x >= 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    IEnumerator DualProgressive()
    {
        isReady = false;
        LookAtObject(enemy.transform.position);
        laserChild.SetActive(true);
        yield return new WaitUntil(() => laserScript.isDone);
        rotationMultiplier = CheckDirection();
        yield return new WaitForSeconds(firingTime);
        laserScript.SpawnDiamonds3();
        yield return new WaitUntil(() => !laserScript.isFiring);
        rotationMultiplier = 0;
        laserChild.SetActive(false);
        isReady = true;
    }

    IEnumerator LaserShowAimed()
    {
        isReady = false;
        coords.rotation = originAim;

        float originRecoil = laserScript.recoilDiamondWave;
        laserScript.recoilDiamondWave *= 4f;

        laserChild.SetActive(true);
        yield return new WaitUntil(() => laserScript.isDone);
        rotationMultiplier = CheckDirection() * laserShowDeaccel;
        for (int i = 0; i < laserShowRepeat; i++)
        {

            laserScript.SpawnDiamonds3();
            yield return new WaitUntil(() => !laserScript.isFiring);

        }


        laserScript.recoilDiamondWave = originRecoil;

        rotationMultiplier = 0;
        laserChild.SetActive(false);
        isReady = true;
    }
    
    IEnumerator LaserShowStrayAimed()
    {
        /*
        isReady = false;
        coords.rotation = originAim;
        laserChild.SetActive(true);
        yield return new WaitUntil(() => laserScript.isDone);
        rotationMultiplier = CheckDirection() * -1 * laserShowDeaccel;
        for (int i = 0; i < laserShowRepeat; i++)
        {
            laserScript.SpawnDiamonds2();
            yield return new WaitForSeconds(laserShowRecoil);
        }
        rotationMultiplier = 0;
        laserChild.SetActive(false);
        isReady = true;*/
        isReady = false;
        coords.rotation = originAim;

        float originRecoil = laserScript.recoilDiamondWave;
        laserScript.recoilDiamondWave *= 4f;

        laserChild.SetActive(true);
        yield return new WaitUntil(() => laserScript.isDone);
        rotationMultiplier = CheckDirection() * laserShowDeaccel * -1;
        for (int i = 0; i < laserShowRepeat; i++)
        {

            laserScript.SpawnDiamonds3();
            yield return new WaitUntil(() => !laserScript.isFiring);

        }


        laserScript.recoilDiamondWave = originRecoil;

        rotationMultiplier = 0;
        laserChild.SetActive(false);
        isReady = true;
    }

    IEnumerator Lockdown()
    {
        isReady = false;
        coords.rotation = startAngle;
        totalProgPercent = 0;
        float normalDiamondDist = laserScript.diamondDist;
        laserScript.diamondDist *= 3.5f;
        laserChild.SetActive(true);
        float originRecoil = laserScript.recoilDiamondWave;
        laserScript.recoilDiamondWave *= 6f;
        yield return new WaitUntil(() => laserScript.isDone);
        progMult = 1;
        isLocking = true;
        while (coords.rotation != endAngle)
        {
            laserScript.SpawnOrbs();
            yield return new WaitUntil(() => !laserScript.isFiring);
        }
        progMult = 0;
        isLocking = false;
        isReady = true;
        laserScript.diamondDist = normalDiamondDist;
        laserScript.recoilDiamondWave = originRecoil;
        laserChild.SetActive(false);
    }

    IEnumerator DualUnaimed()
    {

        isReady = false;
        LookAtObject(enemy.transform.position);
        laserChild.SetActive(true);
        yield return new WaitUntil(() => laserScript.isDone);
        laserScript.SpawnDiamonds4();
        yield return new WaitUntil(() => !laserScript.isFiring);
        laserChild.SetActive(false);
        isReady = true;
    }

    IEnumerator NeoLaserShowAimed()
    {
        isReady = false;
        coords.rotation = originAim;

        float originRecoil = laserScript.recoilDiamondWave;
        laserScript.recoilDiamondWave *= 4f;

        laserChild.SetActive(true);
        yield return new WaitUntil(() => laserScript.isDone);
        rotationMultiplier = CheckDirection() * laserShowDeaccel;
        /*
        for (int i = 0; i < laserShowRepeat; i++)
        {

            laserScript.SpawnDiamonds3();
            yield return new WaitUntil(() => !laserScript.isFiring);

        }*/
        Coroutine diamondCycle = StartCoroutine(DiamondCycler());
        yield return new WaitForSeconds(laserShowDuration);
        StopCoroutine(diamondCycle);
        endCycle = true;
        //yield return new WaitUntil(() => !laserScript.isFiring);
        //StopCoroutine(diamondCycle);

        laserScript.recoilDiamondWave = originRecoil;

        rotationMultiplier = 0;
        laserChild.SetActive(false);
        isReady = true;
        endCycle = false;
    }

    IEnumerator DiamondCycler()
    {
        while (!endCycle)
        {
            laserScript.SpawnDiamonds3();
            yield return new WaitUntil(() => !laserScript.isFiring);
        }
    }

    IEnumerator NeoLaserShowStrayAimed()
    {
        /*
        isReady = false;
        coords.rotation = originAim;
        laserChild.SetActive(true);
        yield return new WaitUntil(() => laserScript.isDone);
        rotationMultiplier = CheckDirection() * -1 * laserShowDeaccel;
        for (int i = 0; i < laserShowRepeat; i++)
        {
            laserScript.SpawnDiamonds2();
            yield return new WaitForSeconds(laserShowRecoil);
        }
        rotationMultiplier = 0;
        laserChild.SetActive(false);
        isReady = true;*/
        isReady = false;
        coords.rotation = originAim;

        float originRecoil = laserScript.recoilDiamondWave;
        laserScript.recoilDiamondWave *= 4f;

        laserChild.SetActive(true);
        yield return new WaitUntil(() => laserScript.isDone);
        rotationMultiplier = CheckDirection() * laserShowDeaccel * -1;
        /*
        for (int i = 0; i < laserShowRepeat; i++)
        {

            laserScript.SpawnDiamonds3();
            yield return new WaitUntil(() => !laserScript.isFiring);

        }*/
        Coroutine diamondCycle = StartCoroutine(DiamondCycler());
        yield return new WaitForSeconds(laserShowDuration);
        StopCoroutine(diamondCycle);
        endCycle = true;
        //yield return new WaitUntil(() => !laserScript.isFiring);
        //StopCoroutine(diamondCycle);


        laserScript.recoilDiamondWave = originRecoil;

        rotationMultiplier = 0;
        laserChild.SetActive(false);
        endCycle = false;
        isReady = true;
    }


}
