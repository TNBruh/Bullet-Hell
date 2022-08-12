using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTIntro : Bullet
{
    [SerializeField] float growthSpeed;
    Vector3 startSize = new Vector3(0.01f, 0.01f, 1);
    float nextR;
    float radius;
    bool hasSpawned = false;
    [SerializeField] GameObject marisa;
    [SerializeField] GameObject reisen;
    [SerializeField] GameObject sakuya;
    [SerializeField] GameObject utsuho;

    protected override void Start()
    {
        base.Start();
        radius = coords.localScale.x;
        coords.localScale = startSize;
    }

    private void FixedUpdate()
    {
        Turn(GetRotationalSpeed());
        nextR = Mathf.Clamp(coords.localScale.x + Time.deltaTime * growthSpeed, coords.localScale.x, radius);
        coords.localScale = new Vector3(nextR, nextR, 1);
        if (!hasSpawned && coords.localScale.x == radius)
        {
            hasSpawned = true;
            Instantiate(marisa, coords.position, qZero);
            Instantiate(reisen, coords.position, qZero);
            Instantiate(sakuya, coords.position, qZero);
            Instantiate(utsuho, coords.position, qZero);
            Destroy(gameObject, recoil);
        }
    }




}
