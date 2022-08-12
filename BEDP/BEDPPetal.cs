using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPPetal : Bullet
{
    [SerializeField] bool rotatesRight = true;
    bool allowFire = true;
    BEDPPetalBit[] petalBits;

    override protected void Start()
    {
        petalBits = new BEDPPetalBit[coords.childCount];
        for (int i = 0; i < coords.childCount; i++)
        {
            petalBits[i] = coords.GetChild(i).gameObject.GetComponent<BEDPPetalBit>();
        }
    }
    private void FixedUpdate()
    {
        //TurnTransform(GetRotationalSpeed());
        //StartCoroutine(SpawnBullet());

    }
}
