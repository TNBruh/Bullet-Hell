using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3TPIn : Bullet
{
    [SerializeField] GameObject output;
    [SerializeField] GCT3TPOut outputScript;
    [SerializeField] float rotation;
    [SerializeField] float dist = 1.2f;
    [SerializeField] Sprite blueCard;
    [SerializeField] Sprite redCard;
    GameObject sakuya;
    Vector3 originVect;
    GCTP3 masterScript;

    protected override void Start()
    {
        base.Start();
        coords.rotation = Quaternion.Euler(0, 0, rotation);
        sakuya = GameObject.FindGameObjectWithTag("Sakuya");
        coords.position = sakuya.transform.position;
        originVect = sakuya.transform.position;
        StartCoroutine(StopMovement());
        outputScript = output.GetComponent("GCT3TPOut") as GCT3TPOut;
        masterScript = GameObject.FindGameObjectWithTag("Master").GetComponent("GCTP3") as GCTP3;

    }

    private void FixedUpdate()
    {
        MoveBulletYTransform(-1);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet2")
        {

        }
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet2")
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == blueCard)
            {
                outputScript.Fire(false);
            }
            else if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == redCard)
            {
                outputScript.Fire(true);
            }
            Destroy(collision.gameObject);
            masterScript.countTotalCards++;
        }
    }

    IEnumerator StopMovement()
    {
        yield return new WaitUntil(() => Vector3.Distance(originVect, coords.position) >= dist);
        boost = 0;
    }
}
