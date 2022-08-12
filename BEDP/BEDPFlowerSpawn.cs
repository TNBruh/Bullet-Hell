using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPFlowerSpawn : Bullet
{
    Vector3 assignedPosition;
    [SerializeField] GameObject flower;
    [SerializeField] float progPercent;
    bool isReady = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        assignedPosition = coords.position;

        //Debug.Log(assignedPosition.x);
        coords.position = vectZero;
        if ((assignedPosition.x > 4.5 || assignedPosition.x < -4.5) || (assignedPosition.y > 4.8 || assignedPosition.y < -4.8))
        {
            //Debug.Log("disabled");
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (coords.position != assignedPosition)
        {
            coords.position = Vector3.MoveTowards(coords.position, assignedPosition, progPercent / 100);
            
        }
        else if (!isReady && coords.position == assignedPosition)
        {
            isReady = true;
            coords.parent.GetComponent<BEDPFlowerSpawner>().SetReady();
        }
    }

    internal void StartSpawn()
    {
        Instantiate(flower, coords.position, coords.rotation);
        Destroy(gameObject);
    }
}
