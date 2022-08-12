using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEDPDiamond : Bullet
{
    [SerializeField] float prog = 0.04f;
    [SerializeField] float window = 10f;
    int mult = 0;

    protected override void Start()
    {
        base.Start();
        LookAtObject(enemy.transform.position);
        coords.rotation *= Quaternion.Euler(0, 0, window);
        boost = 0;
        StartCoroutine(Launch());
    }

    private void FixedUpdate()
    {
        boost = Mathf.Clamp01(boost + prog) * mult;
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(recoil);
        mult = 1;
    }
}
