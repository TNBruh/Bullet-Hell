using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;
using UnityEngine;

public class GCT3CardGen : Bullet
{
    [SerializeField] GameObject redCard;
    [SerializeField] GameObject blueCard;
    GameObject redCardIns;
    GameObject blueCardIns;
    GCT3MagicCard redCardScript;
    GCT3MagicCard blueCardScript;
    GCTP3 gct3Master;

    protected override void Start()
    {
        base.Start();
        redCardIns = Instantiate(redCard, new Vector3(20, 20, 0), new Quaternion());
        redCardIns.SetActive(false);
        blueCardIns = Instantiate(blueCard, new Vector3(20, 20, 0), new Quaternion());
        blueCardIns.SetActive(false);

        redCardScript = redCardIns.GetComponent("GCT3MagicCard") as GCT3MagicCard;
        blueCardScript = blueCardIns.GetComponent("GCT3MagicCard") as GCT3MagicCard;

        gct3Master = GameObject.FindGameObjectWithTag("Master").GetComponent("GCTP3") as GCTP3;
        gct3Master.cardGenS = this;
    }

    internal GameObject FireBlue(float angle = 0, float speed = 30)
    {
        //Instantiate(blueCard, coords.position, Quaternion.Euler(0, 0, angle));
        blueCardScript.speed = speed;
        GameObject card = Instantiate(blueCardIns, coords.position, Quaternion.Euler(0, 0, angle));
        card.SetActive(true);
        return card;

    }
    internal GameObject FireRed(float angle = 0, float speed = 30)
    {
        redCardScript.speed = speed; 
        GameObject card = Instantiate(redCardIns, coords.position, Quaternion.Euler(0, 0, angle));
        card.SetActive(true);
        return card;
    }

    internal void Despawn()
    {
        Destroy(blueCardIns);
        Destroy(redCardIns);
        Destroy(gameObject);
    }
}
