using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTNukeArrow : Bullet
{
    [SerializeField] float maxTurn = 90;
    Quaternion ceilTurn;
    [SerializeField] bool cancelBurst;
    [SerializeField] GameObject[] illusionClone = new GameObject[4];
    [SerializeField] Sprite illusionSprite;
    Sprite normalSprite;
    SpriteRenderer spriteRenderer;
    [SerializeField] float direction;
    [SerializeField] float illusionSpeed = 0.1f;
    Vector3 directionV;
    [SerializeField] bool earlyTimeStop = false;

    protected override void Start()
    {
        base.Start();
        GCTP1 script = GameObject.FindGameObjectWithTag("Master").GetComponent(typeof(GCTP1)) as GCTP1;
        script.AddInstance((Bullet)this);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        normalSprite = spriteRenderer.sprite;
        directionV = Angle2Vector(direction);
        cancelBurst = earlyTimeStop;
        StopTime(earlyTimeStop);
        ceilTurn = coords.rotation * Quaternion.Euler(0, 0, maxTurn);
    }

    private void FixedUpdate()
    {
        MoveBulletY();
        body.MoveRotation(Quaternion.Slerp(coords.rotation, ceilTurn, GetRotationalSpeed()));
        
        if (timeStopped)
        {
            coords.position += directionV * Time.deltaTime * illusionSpeed;
        }
    }


    internal override void StopTime(bool isStopped)
    {
        base.StopTime(isStopped);
        if (isStopped)
        {
            if (!cancelBurst)
            {
                Instantiate(illusionClone[0], coords.position, coords.rotation);
                Instantiate(illusionClone[1], coords.position, coords.rotation);
                Instantiate(illusionClone[2], coords.position, coords.rotation);
                Instantiate(illusionClone[3], coords.position, coords.rotation);
                Destroy(gameObject);
            }
            spriteRenderer.sprite = illusionSprite;
            cancelBurst = false;
            hitbox.enabled = false;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
            hitbox.enabled = true;
        }
    }
}
