using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GCTP3 : Bullet
{
    internal List<Bullet> instancesTGC = new List<Bullet>();
    Entity player;
    bool allowFire = true;
    [SerializeField] GameObject marisa;
    [SerializeField] GameObject sakuya;
    [SerializeField] GameObject reisen;
    [SerializeField] GameObject utsuho;

    [SerializeField] GameObject portal;
    [SerializeField] GameObject whiteBurst;
    [SerializeField] GameObject tornado;
    [SerializeField] GameObject linkedPortals;

    internal GCT3CardGen cardGenS;
    bool isRed = true;
    internal int countReadyPortal = 0;
    GameObject lastPortalCard;
    internal int countTotalCards = 0;
    [SerializeField] float minPortalCardSpeed = 10f;
    [SerializeField] float maxPortalCardSpeed = 100f;
    [SerializeField] int totalPortalCards = 120;
    [SerializeField] float cardGenRecoil = 0.14f;

    [SerializeField] float tNextWhiteBurst = 0.25f;
    [SerializeField] float tStartPrison = 0.32f;
    [SerializeField] float tTornado = 0.25f;
    [SerializeField] float tBeginGen = 0.5f;
    [SerializeField] float tPrelude = 0.3f;
    [SerializeField] float tDelayCard = 1.2f;
    [SerializeField] float tBetweenPops = 0.5f;

    NPCController marisaS;
    NPCController sakuyaS;
    NPCController reisenS;
    NPCController utsuhoS;

    [SerializeField] Vector3 sakuyaPos;
    [SerializeField] Vector3 marisaPos;
    [SerializeField] Vector3 reisenPos;
    [SerializeField] Vector3 utsuhoPos;

    [SerializeField] Vector3 sakuyaPos1;
    [SerializeField] Vector3 marisaPos1;
    [SerializeField] Vector3 reisenPos1;
    [SerializeField] Vector3 utsuhoPos1;


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
        LinkScript();
        StartCoroutine(Commence());
    }

    internal void LinkScript()
    {
        marisaS = marisa.GetComponent("NPCController") as NPCController;
        sakuyaS = sakuya.GetComponent("NPCController") as NPCController;
        reisenS = reisen.GetComponent("NPCController") as NPCController;
        utsuhoS = utsuho.GetComponent("NPCController") as NPCController;
    }

    internal void AddInstance(Bullet blt)
    {
        instancesTGC.Add(blt);
    }

    internal void CullNull()
    {
        instancesTGC.RemoveAll((Bullet i) => i == null);
    }

    internal void Freeze()
    {
        instancesTGC.ForEach(delegate (Bullet i) { i.StopTime(true); });
        //player.StopTime(true);
    }

    internal void PopCards()
    {
        CullNull();
        int c;
        if (instancesTGC.Count >= 12)
        {
            c = 12;
        }
        else
        {
            c = instancesTGC.Count;
        }
        for (int i = 0; i < c; i++)
        {
            instancesTGC[i].StopTime(true);
        }
    }

    internal void Unfreeze()
    {
        instancesTGC.ForEach(delegate (Bullet i) { i.StopTime(false); });
        //player.StopTime(false);
    }

    IEnumerator Commence()
    {
        sakuyaPos = sakuya.transform.position;
        marisaPos = marisa.transform.position;
        reisenPos = reisen.transform.position;
        utsuhoPos = utsuho.transform.position;

        marisaPos1 = utsuho.transform.position + new Vector3(-2, 4);
        marisa.transform.position = marisaPos1;
        marisaS.assignedPosition = utsuhoPos;

        utsuhoS.assignedPosition = utsuho.transform.position + new Vector3(0, 5);

        yield return new WaitUntil(() => marisa.transform.position == marisaS.assignedPosition);

        GameObject firstWhiteBurst = Instantiate(whiteBurst, marisa.transform.position, new Quaternion());
        Instantiate(portal, reisen.transform.position, new Quaternion());

        yield return new WaitForSeconds(tNextWhiteBurst);
        yield return new WaitUntil(() => !firstWhiteBurst);

        Instantiate(whiteBurst, marisa.transform.position, new Quaternion());

        yield return new WaitForSeconds(tStartPrison);
        yield return new WaitUntil(() => countReadyPortal == 3);

        countReadyPortal = 0;

        CullNull();
        Freeze();

        yield return new WaitForSeconds(tTornado);

        //GameObject torn = Instantiate(tornado, marisa.transform.position, qZero);

        GameObject linked = Instantiate(linkedPortals, sakuya.transform.position, new Quaternion());

        yield return new WaitForSeconds(tBeginGen);


        int dupeCountCard = totalPortalCards;

        cardGenS.FireBlue(0, 20);
        cardGenS.FireRed(60, 20);
        cardGenS.FireBlue(120, 20);
        cardGenS.FireRed(180, 20);
        cardGenS.FireBlue(240, 20);
        cardGenS.FireRed(300, 20);

        countTotalCards -= 6;

        yield return new WaitForSeconds(tPrelude);

        while (totalPortalCards > 0)
        {
            yield return StartCoroutine(RapidFire());
            yield return new WaitForSeconds(tDelayCard);
        }

        CullNull();

        yield return new WaitUntil(() => countTotalCards == dupeCountCard);
        //Debug.Log(lastPortalCard.gameObject.name);
        GameObject torn = Instantiate(tornado, marisa.transform.position, qZero);
        cardGenS.Despawn();
        Destroy(linked);

        while (true)
        {
            PopCards();
            yield return new WaitForSeconds(tBetweenPops);
            if (instancesTGC.Count == 0)
            {
                break;
            }
        }

        Destroy(torn);
        Destroy(gameObject);
    }

    IEnumerator RapidFire()
    {
        int cardCount = Random.Range(6, 30);
        if (cardCount >= totalPortalCards)
        {
            cardCount = totalPortalCards;
        }
        totalPortalCards -= cardCount;
        for (int i = 0; i < cardCount; i++)
        {
            if (isRed)
            {
                lastPortalCard = cardGenS.FireRed(Random.Range(0, 5) * 60, Random.Range(minPortalCardSpeed, maxPortalCardSpeed));
            }
            else
            {
                lastPortalCard = cardGenS.FireBlue(Random.Range(0, 5) * 60, Random.Range(minPortalCardSpeed, maxPortalCardSpeed));
            }
            isRed = !isRed;

            yield return new WaitForSeconds(cardGenRecoil);
        }
    }
}
