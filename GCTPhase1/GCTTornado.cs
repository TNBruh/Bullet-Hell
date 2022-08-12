using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTTornado : Bullet
{
    [SerializeField] GameObject card;
    [SerializeField] GameObject card1;
    GameObject inst;
    GameObject inst1;
    GCTCard instScript;
    GCTCard inst1Script;
    bool allowFire = false;
    Quaternion q72 = Quaternion.Euler(0, 0, 72);
    Quaternion q144 = Quaternion.Euler(0, 0, 144);
    Quaternion q216 = Quaternion.Euler(0, 0, 216);
    Quaternion q288 = Quaternion.Euler(0, 0, 288);
    bool allowAdjust = false;
    bool proceed = false;
    bool isRed = true;

    [SerializeField] float boostAdjust = -0.01f;
    [SerializeField] float boostAdjust1 = -0.005f;
    [SerializeField] float boostAdjust2 = -0.001f;

    [SerializeField] float maxBoost = -0.3f;
    [SerializeField] float lowBoost = -0.0005f;

    [SerializeField] float time;
    [SerializeField] float time1;
    [SerializeField] float time2;
    [SerializeField] float time3;


    protected override void Start()
    {
        base.Start();
        inst = Instantiate(card, new Vector3(8, 8, 0), new Quaternion());
        inst1 = Instantiate(card1, new Vector3(8, 8, 0), new Quaternion());
        inst.SetActive(false);
        instScript = (GCTCard)inst.GetComponent(typeof(GCTCard));
        inst1.SetActive(false);
        inst1Script = (GCTCard)inst1.GetComponent(typeof(GCTCard));
        StartCoroutine(CommenceAdjust());
        allowFire = true;

        coords.position = GameObject.FindGameObjectWithTag("Sakuya").transform.position;
    }

    private void FixedUpdate()
    {
        StartCoroutine(Fire());
        Turn(GetRotationalSpeed());
    }

    IEnumerator Fire()
    {
        if (allowFire)
        {
            allowFire = false;
            if (isRed)
            {
                Instantiate(inst, coords.position, coords.rotation).SetActive(true);
                Instantiate(inst, coords.position, coords.rotation * q72).SetActive(true);
                Instantiate(inst, coords.position, coords.rotation * q144).SetActive(true);
                Instantiate(inst, coords.position, coords.rotation * q216).SetActive(true);
                Instantiate(inst, coords.position, coords.rotation * q288).SetActive(true);
                isRed = false;
            }
            else
            {
                Instantiate(inst1, coords.position, coords.rotation).SetActive(true);
                Instantiate(inst1, coords.position, coords.rotation * q72).SetActive(true);
                Instantiate(inst1, coords.position, coords.rotation * q144).SetActive(true);
                Instantiate(inst1, coords.position, coords.rotation * q216).SetActive(true);
                Instantiate(inst1, coords.position, coords.rotation * q288).SetActive(true);
                isRed = true;
            }
            allowAdjust = true;
            yield return new WaitForSeconds(recoil);
            allowAdjust = false;
            allowFire = true;
        }
    }

    internal void Adjust(float inpDeAccel)
    {
        instScript.deAccel = inpDeAccel;
        inst1Script.deAccel = inpDeAccel;
        //instScript.window = inpWindow;
        //instScript.ChangeMult();
    }
    /*
    IEnumerator CommenceAdjust(float inpDeAccel)
    {
        yield return new WaitUntil(() => allowAdjust);
        Adjust(inpDeAccel);
    }
    */
    IEnumerator CommenceAdjust()
    {
        float boostInp = lowBoost;
        Adjust(boostInp);
        while (Mathf.Abs(maxBoost) > Mathf.Abs(boostInp))
        {
            yield return new WaitUntil(() => allowAdjust);
            Adjust(boostInp);
            //boostInp += boostAdjust;
            boostInp += lowBoost;
            yield return new WaitWhile(() => allowAdjust);
            //Debug.Log("1 " + maxBoost + " n " + boostInp);
            //yield return null;
        }
        /*
        boostInp = lowBoost;
        Adjust(boostInp);

        instScript.recoil = 8f;
        inst1Script.recoil = 8f;
        rotationalBoost *= -1;
        while (Mathf.Abs(maxBoost) > Mathf.Abs(boostInp))
        {
            yield return new WaitUntil(() => allowAdjust);
            Adjust(boostInp);
            boostInp += boostAdjust1;
            yield return new WaitWhile(() => allowAdjust);
            //Debug.Log("2 " + maxBoost + " n " + boostInp);
            //yield return null;
        }
        boostInp = maxBoost;
        Adjust(boostInp);
        //instScript.recoil = 12f;
        //inst1Script.recoil = 12f;
        rotationalBoost *= -1;
        while (Mathf.Abs(lowBoost) < Mathf.Abs(boostInp))
        {
            yield return new WaitUntil(() => allowAdjust);
            Adjust(boostInp);
            boostInp -= boostAdjust2;
            yield return new WaitWhile(() => allowAdjust);
            //Debug.Log("3 " + lowBoost + " n " + boostInp);
            //yield return null;
        }
        */
        Destroy(inst);
        Destroy(inst1);
        Destroy(gameObject);
    }


}
