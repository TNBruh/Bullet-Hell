using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GCTP1 : Bullet
{
    internal List<Bullet> instancesTGC = new List<Bullet>();
    //[SerializeField] GameObject objFlower;
    //[SerializeField] GameObject knifeBurst;
    //[SerializeField] GameObject playerTGC;
    TGCPlayer playerScript;
    //[SerializeField] float recoil2 = 0.2f;
    //[SerializeField] float recoil3 = 2;
    //[SerializeField] float recoil4 = 0.08f;
    //[SerializeField] GameObject knifeSpawn;
    Entity player;
    bool allowFire = true;
    [SerializeField] float tPreBurstKnife = 3;
    [SerializeField] float tPreBurstNuke = 5;
    [SerializeField] float tPrePreSpark = 1;
    [SerializeField] float tPreIllusion = 5;
    [SerializeField] float tIllusion = 2;
    [SerializeField] float tPreSpark = 6;
    [SerializeField] float tPreTornado = 1;
    [SerializeField] float tPostTornado = 4;
    [SerializeField] float tPostTornado1 = 4;
    [SerializeField] float tPostTornado2 = 2;


    [SerializeField] internal List<GCTCrashOrb> nukeOrbs = new List<GCTCrashOrb>();
    [SerializeField] GameObject nuke;
    [SerializeField] GameObject flower;
    [SerializeField] GameObject sparkPoint;
    [SerializeField] GameObject knifeTornado;
    [SerializeField] GameObject burst;
    [SerializeField] GameObject marisa;
    [SerializeField] GameObject sakuya;

    [SerializeField] Vector3 marisaPos;
    [SerializeField] Vector3 sakuyaPosBurst;
    [SerializeField] Vector3 sakuyaPosFlower;
    [SerializeField] Vector3 sakuyaPosTornado;


    //[SerializeField] internal Thread secondThread;


    protected override void Start()
    {
        base.Start();
        /*
        objFlower.GetComponent<TGCFlower>().masterTGC = this;
        //Debug.Log(objFlower.name);
        knifeBurst.GetComponent<TGCBurstKnife>().masterTGC = this;
        playerScript = playerTGC.GetComponent<TGCPlayer>();
        playerScript.masterTGC = this;
        */

        //player = enemy.GetComponent<Entity>();
        //playerScript = Instantiate(playerTGC).GetComponent<TGCPlayer>();
        //ThreadStart tInst = new ThreadStart(AddInstance2);
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

    }

    internal void Freeze()
    {
        instancesTGC.ForEach(delegate (Bullet i) { i.StopTime(true); });
        //player.StopTime(true);
    }

    internal void Unfreeze()
    {
        instancesTGC.ForEach(delegate (Bullet i) { i.StopTime(false); });
        //player.StopTime(false);
    }

    IEnumerator Commence()
    {
        Instantiate(nuke);
        Instantiate(nuke);
        NPCController s = Instantiate(sakuya, new Vector3(5.77f, 5.39f), new Quaternion()).GetComponent(typeof(NPCController)) as NPCController;
        NPCController m = Instantiate(marisa, new Vector3(-5.53f, 5.39f), new Quaternion()).GetComponent(typeof(NPCController)) as NPCController;
        s.assignedPosition = sakuyaPosBurst;
        m.assignedPosition = marisaPos;
        yield return new WaitUntil(() => s.gameObject.transform.position == sakuyaPosBurst && m.gameObject.transform.position == marisaPos);
        yield return new WaitForSeconds(tPreBurstKnife);
        Instantiate(burst);
        yield return new WaitForSeconds(tPreBurstNuke);
        for (int i = 0; i < nukeOrbs.Count; i++)
        {
            nukeOrbs[i].CallBurst();
        }
        Instantiate(flower);
        GameObject sparkObj = Instantiate(sparkPoint);
        GCTSparkPoint sparkScript = sparkObj.GetComponent<GCTSparkPoint>();
        yield return new WaitForSeconds(tPrePreSpark);
        sparkScript.CommenceFire();
        GameObject b = Instantiate(burst);
        yield return new WaitWhile(() => b);
        s.assignedPosition = sakuyaPosFlower;
        yield return new WaitUntil(() => s.gameObject.transform.position == sakuyaPosFlower);
        yield return new WaitForSeconds(tPreIllusion);
        CullNull();
        Freeze();
        s.assignedPosition = sakuyaPosTornado;
        yield return new WaitForSeconds(tIllusion);
        CullNull();
        Unfreeze();
        yield return new WaitForSeconds(tPreSpark);
        sparkScript.CommenceLaser();
        Instantiate(knifeTornado);
        yield return new WaitForSeconds(tPostTornado);
        sparkScript.CommenceFire();
        yield return new WaitForSeconds(tPostTornado1);
        sparkScript.CommenceLaser();
        yield return new WaitForSeconds(tPostTornado2);
        Destroy(sparkScript.gameObject);
        Destroy(gameObject);

    }
}
