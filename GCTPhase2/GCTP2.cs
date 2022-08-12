using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GCTP2 : Bullet
{
    internal List<Bullet> instancesTGC = new List<Bullet>();
    Entity player;
    bool allowFire = true;
    [SerializeField] GameObject marisa;
    [SerializeField] GameObject sakuya;
    [SerializeField] GameObject reisen;
    [SerializeField] GameObject utsuho;

    [SerializeField] GameObject burst;
    GCTBurstCT burstScript;
    [SerializeField] GameObject summonOrb;
    [SerializeField] GameObject summonNPC;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject prison;
    [SerializeField] GameObject nuke;

    [SerializeField] GameObject summonSprite;

    [SerializeField] Vector3 reisenStartPos = new Vector3(5.2f, 5.2f);
    [SerializeField] Vector3 reisenPos = new Vector3(3.5f, 3f);
    [SerializeField] Vector3 utsuhoPos = new Vector3(0, 3.02f);

    [SerializeField] bool triggerUnfreeze = false;
    [SerializeField] bool triggerFreeze = false;

    [SerializeField] bool onBurst = false;
    [SerializeField] float recoilBurst = 1f;
    [SerializeField] bool onExplosion = false;
    [SerializeField] float recoilExplosion = 0.2f;
    [SerializeField] float recoilSummonOrb = 1.2f;
    [SerializeField] float recoilSupriseExplosion = 1f;
    [SerializeField] bool onNuke = false;
    [SerializeField] float delayNuke = 4;
    [SerializeField] float recoilNuke = 0.2f;
    [SerializeField] float delayMarisaSpark = 0.4f;
    [SerializeField] float delayDestruction = 1;

    internal GameObject ins;



    //[SerializeField] internal Thread secondThread;


    protected override void Start()
    {
        base.Start();
        
        if (!marisa)
        {
            marisa = GameObject.FindGameObjectWithTag("Marisa");
        }
        if (!sakuya)
        {
            sakuya = GameObject.FindGameObjectWithTag("Sakuya");
        }
        if (!reisen)
        {
            reisen = GameObject.FindGameObjectWithTag("Reisen");
        }
        if (!utsuho)
        {
            utsuho = GameObject.FindGameObjectWithTag("Utsuho");
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(Entity)) as Entity;
        StartCoroutine(Commence());
    }

    internal void AddInstance(Bullet blt)
    {
        instancesTGC.Add(blt);
    }

    internal void CullNull()
    {
        instancesTGC.RemoveAll((Bullet i) => i == null);
    }

    private void FixedUpdate()
    {
        
        if (triggerUnfreeze)
        {
            triggerUnfreeze = false;
            CullNull();
            Unfreeze();
        }
        if (triggerFreeze)
        {
            triggerFreeze = false;
            CullNull();
            Freeze();
        }
        
    }

    internal void Freeze()
    {
        instancesTGC.ForEach(delegate (Bullet i) { i.StopTime(true); });
        player.StopTime(true);
    }

    internal void Unfreeze()
    {
        instancesTGC.ForEach(delegate (Bullet i) { i.StopTime(false); });
        player.StopTime(false);
    }

    IEnumerator Commence()
    {
        //int r1 = Random.Range(0, 2);
        //int r2 = Random.Range(0, 2);
        int r1 = 1;
        int r2 = 0;

        reisenStartPos.x *= (1 + r1 / (-1) + r1 / (-1));
        reisenStartPos.y *= (1 + r2 / (-1) + r2 / (-1));
        reisenPos.x *= (1 + r1 / (-1) + r1 / (-1));
        reisenPos.y *= (1 + r2 / (-1) + r2 / (-1));
        // r / (-1)
        // 1 + (r/(-1))
        // 0/(-1) + 1 + (0/(-1)) = 0 + 1
        // 1/(-1) + 1 + (1/(-1)) = -1 + 1 + (-1)
        GameObject objUtsuho = Instantiate(utsuho, new Vector3(0, 5, 0), new Quaternion());
        NPCController utsuhoScript = objUtsuho.GetComponent(typeof(NPCController)) as NPCController;
        utsuhoScript.assignedPosition = utsuhoPos;
        GameObject objReisen = Instantiate(reisen, reisenStartPos, new Quaternion());
        NPCController reisenScript = objReisen.GetComponent(typeof(NPCController)) as NPCController;
        reisenScript.assignedPosition = reisenPos;
        NPCController marisaScript = marisa.GetComponent(typeof(NPCController)) as NPCController;
        marisaScript.assignedPosition += new Vector3(-3, 0);
        GameObject objSakuya = sakuya;
        NPCController sakuyaScript = sakuya.GetComponent(typeof(NPCController)) as NPCController;
        sakuyaScript.assignedPosition = new Vector3(reisenPos.x * -1f, reisenPos.y);
        yield return new WaitUntil(() => objUtsuho.transform.position == utsuhoPos && objReisen.transform.position == reisenPos && objSakuya.transform.position == new Vector3(reisenPos.x * -1, reisenPos.y));
        onBurst = true;
        onExplosion = true;
        Coroutine brst = StartCoroutine(SakuyaBurst(sakuyaScript));
        Coroutine expl = StartCoroutine(UtsuhoExplosion(objUtsuho.transform.GetChild(0)));

        //Instantiate(summonOrb, objReisen.transform.position, qZero);
        
        GameObject smnOrb = Instantiate(summonOrb, new Vector3(12, 12, 0), new Quaternion());
        smnOrb.SetActive(false);
        
        GameObject smnObj = Instantiate(smnOrb, objReisen.transform.position, new Quaternion());
        Bullet smnObjScript = smnObj.GetComponent(typeof(Bullet)) as Bullet;
        smnObjScript.LookAtObject(enemy.transform.position);
        Quaternion q = smnObj.transform.rotation;
        //Debug.Log(q.eulerAngles);
        smnObj.SetActive(true);

        //Instantiate(summonSprite, smnObj.transform);

        Instantiate(smnOrb, objReisen.transform.position, q * Quaternion.Euler(0, 0, 30)).SetActive(true);
        Instantiate(smnOrb, objReisen.transform.position, q * Quaternion.Euler(0, 0, -30)).SetActive(true);


        yield return new WaitForSeconds(recoilSummonOrb);
        /*
        Instantiate(smnOrb, objReisen.transform.position, q * Quaternion.Euler(0, 0, 80)).SetActive(true);
        Instantiate(smnOrb, objReisen.transform.position, q * Quaternion.Euler(0, 0, -80)).SetActive(true);
        Destroy(smnOrb);
        */
        onExplosion = false;

        yield return expl;
        yield return new WaitUntil(() => !ins);

        onBurst = false;
        yield return brst;



        CullNull();
        Freeze();
        yield return null;
        GameObject p = Instantiate(prison, enemy.transform.position, new Quaternion());
        yield return new WaitWhile(() => p);

        CullNull();
        Unfreeze();

        yield return new WaitForSeconds(recoilSupriseExplosion);

        Instantiate(explosion, objUtsuho.transform.GetChild(0).transform.position, new Quaternion());
        //onBurst = true;
        //StartCoroutine(UtsuhoExplosion(objUtsuho.transform.GetChild(0)));
        yield return null;

        yield return new WaitForSeconds(delayNuke);
        //onBurst = false;
        onNuke = true;
        StartCoroutine(UtsuhoNuke());

        GameObject npcS = Instantiate(summonNPC, new Vector3(6, 6, 0), new Quaternion());
        GCTNPCSummon objNPCScript;
        npcS.SetActive(false);

        GameObject objNPCS = Instantiate(npcS, objReisen.transform.position, new Quaternion());
        objNPCScript = objNPCS.GetComponent(typeof(GCTNPCSummon)) as GCTNPCSummon;
        objNPCScript.LookAtObject(enemy.transform.position);
        objNPCScript.boost = 1;
        GameObject n1 = Instantiate(npcS, objReisen.transform.position, objNPCScript.transform.rotation * Quaternion.Euler(0, 0, 30));
        GameObject n2 = Instantiate(npcS, objReisen.transform.position, objNPCScript.transform.rotation * Quaternion.Euler(0, 0, -30));
        objNPCS.SetActive(true);
        n1.SetActive(true);
        n2.SetActive(true);
        Coroutine s1 = StartCoroutine(TriggerSummonMarisa(objNPCScript));
        yield return s1;
        Coroutine s2 = StartCoroutine(TriggerSummonMarisa(n1.GetComponent(typeof(GCTNPCSummon)) as GCTNPCSummon));
        yield return s2;
        Coroutine s3 = StartCoroutine(TriggerSummonMarisa(n2.GetComponent(typeof(GCTNPCSummon)) as GCTNPCSummon));
        Destroy(npcS);
        yield return new WaitWhile(() => n2);
        onNuke = false;
        yield return new WaitForSeconds(delayDestruction);
        Destroy(gameObject);


    }

    IEnumerator SakuyaBurst(NPCController inpSakuya)
    {
        burstScript = Instantiate(burst, inpSakuya.gameObject.transform.position, new Quaternion()).GetComponent(typeof(GCTBurstCT)) as GCTBurstCT;
        bool flip = false;
        yield return new WaitForSeconds(recoilBurst);
        while (onBurst)
        {
            if (flip)
            {
                burstScript.CommenceFire();
            }
            else
            {
                burstScript.CommenceFire1();
            }
            yield return new WaitForSeconds(recoilBurst);
            flip = !flip;
        }
        Destroy(burstScript.gameObject);
    }
    IEnumerator UtsuhoExplosion(Transform inpPos)
    {
        while (onExplosion)
        {
            Instantiate(explosion, inpPos.position, new Quaternion());
            yield return new WaitForSeconds(recoilExplosion);
        }
    }

    IEnumerator UtsuhoNuke()
    {
        while (onNuke)
        {
            Instantiate(nuke);
            yield return new WaitForSeconds(recoilNuke);
        }
    }

    IEnumerator TriggerSummonMarisa(GCTNPCSummon inpScr)
    {
        yield return new WaitUntil(() => inpScr.boost == 0);
        yield return new WaitForSeconds(delayMarisaSpark);
        inpScr.Spawn();
    }

    
}
