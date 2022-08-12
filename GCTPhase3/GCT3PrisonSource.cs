using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3PrisonSource : Bullet
{
    [SerializeField] GameObject redCard;
    [SerializeField] GameObject blueCard;
    [SerializeField] internal bool isDone = false;

    [SerializeField] bool isRed = true;

    GameObject lastSpawn;
    List<GameObject> touchedObj = new List<GameObject>();

    override protected void Start()
    {
        base.Start();
        if (isRed)
        {
            lastSpawn = Instantiate(redCard, coords.position, coords.rotation);
        }
        else
        {
            lastSpawn = Instantiate(blueCard, coords.position, coords.rotation);
        }
        isRed = !isRed;
    }

    private void FixedUpdate()
    {
        MoveBulletY();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (lastSpawn == collision.gameObject)
        {
            if (isRed)
            {
                lastSpawn = Instantiate(redCard, coords.position, coords.rotation);
            }
            else
            {
                lastSpawn = Instantiate(blueCard, coords.position, coords.rotation);
            }

            isRed = !isRed;
        }
        if (Mathf.Abs(coords.position.x) > 4.5 || Mathf.Abs(coords.position.y) > 4.8)
        {
            lastSpawn = null;
            isDone = true;
        }
    }
}
