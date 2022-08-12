using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTCardSpawner : Bullet
{
    [SerializeField] GameObject card;
    [SerializeField] GameObject card1;
    bool isRed = true;
    bool allowAdjust = false;
    bool allowFire = true;
    [SerializeField] float sideStep = 0.8f;

    [SerializeField] float selfDestructTimer = 6;

    GCTPrisonCard cardScript;
    GCTPrisonCard card1Script;

    GameObject inst;
    GameObject inst1;

    [SerializeField] float boostAdjust = -0.005f;

    [SerializeField] float recoilAdjust = 0.05f;

    [SerializeField] float maxBoost = -0.3f;
    [SerializeField] float lowBoost = -0.0005f;

    protected override void Start()
    {
        base.Start();
        inst = Instantiate(card, new Vector3(8, 8, 0), new Quaternion());
        inst.SetActive(false);
        cardScript = inst.GetComponent(typeof(GCTPrisonCard)) as GCTPrisonCard;

        inst1 = Instantiate(card1, new Vector3(8, 8, 0), new Quaternion());
        inst1.SetActive(false);
        card1Script = inst1.GetComponent(typeof(GCTPrisonCard)) as GCTPrisonCard;

        LookAtObject(enemy.transform.position);
        Quaternion turn = Quaternion.Euler(SideSteppedTargeting(sideStep, enemy.transform.position));
        coords.rotation *= turn;
        StartCoroutine(CommenceAdjust());
        //Destroy(gameObject, selfDestructTimer);
        StartCoroutine(Destruction());
    }

    IEnumerator Fire()
    {
        if (allowFire)
        {
            allowFire = false;
            if (isRed)
            {
                Instantiate(inst, coords.position, coords.rotation).SetActive(true);
                isRed = false;
            }
            else
            {
                Instantiate(inst1, coords.position, coords.rotation).SetActive(true);
                isRed = true;
            }
            allowAdjust = true;
            yield return new WaitForSeconds(recoil);
            allowAdjust = false;
            allowFire = true;
        }
    }

    internal void Adjust(float inpDeAccel, float inpRecoil)
    {
        cardScript.deAccel = inpDeAccel;
        card1Script.deAccel = inpDeAccel;
        cardScript.recoil = inpRecoil;
        card1Script.recoil = inpRecoil;
        //instScript.window = inpWindow;
        //instScript.ChangeMult();
    }

    IEnumerator CommenceAdjust()
    {
        float boostInp = lowBoost;
        float rec = recoilAdjust;
        Adjust(boostInp, rec);

        while (Mathf.Abs(maxBoost) > Mathf.Abs(boostInp))
        {
            yield return new WaitUntil(() => allowAdjust);
            Adjust(boostInp, rec);
            //boostInp += boostAdjust;
            boostInp += lowBoost;
            rec += recoilAdjust;
            yield return new WaitWhile(() => allowAdjust);
            //Debug.Log("1 " + maxBoost + " n " + boostInp);
            //yield return null;
        }
        Destroy(inst);
        Destroy(inst1);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(selfDestructTimer);

        Destroy(inst);
        Destroy(inst1);
        Destroy(gameObject);
    }
}
