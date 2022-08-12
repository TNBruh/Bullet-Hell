using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPFlower : Bullet
{
    [SerializeField] int movementRight = 1;
    BEDPPetalBit[] petalBits;
    List<bool> readyBits = new List<bool>();
    bool allowFire = true;
    GameObject orb;
    bool isReady = false;
    [SerializeField] float xEndPosition;
    [SerializeField] float xStartPosition;


    //
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        petalBits = new BEDPPetalBit[coords.childCount];
        for (int i = 0; i < coords.childCount; i++)
        {
            petalBits[i] = coords.GetChild(i).gameObject.GetComponent<BEDPPetalBit>();
        }
        if (speed < 0)
        {
            movementRight = -1;
        }
    }

    private void FixedUpdate()
    {
        TurnTransform(GetRotationalSpeed());
        coords.position += new Vector3(Time.deltaTime * GetSpeed(), 0);
        if (isReady)
        {
            //enable this when red orbs are programmed properly
            //StartCoroutine(SpawnBullet());
        }
        if (coords.position.x * movementRight >= xEndPosition)
        {
            coords.position = new Vector2(xStartPosition*movementRight, coords.position.y);
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        orb = collision.gameObject;

        string bulletName = orb.name;
        if (string.Equals(bulletName, "BEDPBodyRed"))
        {
            StartCoroutine(SpawnBullet());
        }
    }
    */

    IEnumerator SpawnBullet()
    {
        if (allowFire)
        {
            allowFire = false;
            foreach (BEDPPetalBit i in petalBits)
            {
                i.SpawnBullet();
            }
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }

    internal void SetReady()
    {
        readyBits.Add(true);
        if (readyBits.Count == petalBits.Length)
        {
            isReady = true;
        }
    }
}
