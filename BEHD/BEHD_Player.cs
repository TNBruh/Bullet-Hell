using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEHD_Player : Bullet
{
    //Vector3 targetPos;
    //Vector3 vectZero = new Vector3();
    [SerializeField] GameObject spawnButterflyBlue;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        coords.Translate(enemy.transform.position - coords.position);
        Instantiate(spawnButterflyBlue, coords.position, coords.rotation * Quaternion.Euler(0, 0, -90), coords);
        Instantiate(spawnButterflyBlue, coords.position, coords.rotation * Quaternion.Euler(0, 0, -18), coords);
        Instantiate(spawnButterflyBlue, coords.position, coords.rotation * Quaternion.Euler(0, 0, 54), coords);
        Instantiate(spawnButterflyBlue, coords.position, coords.rotation * Quaternion.Euler(0, 0, 126), coords);
        Instantiate(spawnButterflyBlue, coords.position, coords.rotation * Quaternion.Euler(0, 0, 198), coords);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("delta translate" + (enemy.transform.position - coords.position));
        //coords.Translate(enemy.transform.position - coords.position);
        transform.position = enemy.transform.position;
        //Debug.Log("current translate" + coords.position);

        TurnTransform(rotationalSpeed);
        //Debug.Log("current rotation" + coords.rotation);
    }
}
