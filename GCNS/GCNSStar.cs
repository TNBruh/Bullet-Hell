using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCNSStar : Bullet
{
    [SerializeField] float startSize = 0.01f;
    Vector3 endSize;
    [SerializeField] GameObject orb;
    [SerializeField] float growthSpeed;

    protected override void Start()
    {
        base.Start();
        endSize = coords.localScale;
        coords.localScale = new Vector3(startSize, startSize, 1);
        StartCoroutine(Burst());
    }

    private void FixedUpdate()
    {
        Turn(GetRotationalSpeed());
        coords.localScale = Vector3.Lerp(coords.localScale, endSize, growthSpeed * Time.deltaTime);
    }

    IEnumerator Burst()
    {
        yield return new WaitForSeconds(recoil);
        coords.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 0) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 45) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 90) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 135) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 180) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 225) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 270) * coords.rotation);
        Instantiate(orb, coords.position, Quaternion.Euler(0, 0, 315) * coords.rotation);
        Destroy(gameObject);
    }
}
