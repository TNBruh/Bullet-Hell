using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GCLLArrow : Bullet
{
    [SerializeField] float transSpeed = 10f;
    [SerializeField] bool startFrozen = false;
    [SerializeField] bool cancelBurst = true;
    [SerializeField] GameObject clone;
    [SerializeField] GameObject clone1;
    [SerializeField] GameObject clone2;
    [SerializeField] GameObject clone3;
    [SerializeField] float direction = 45f;
    [SerializeField] Sprite passiveState;
    Sprite normalState;
    SpriteRenderer spriteRenderer;
    Vector3 directionV;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        normalState = spriteRenderer.sprite;
        cancelBurst = startFrozen;
        StopTime(startFrozen);
        GameObject.FindGameObjectWithTag("Bullet2").GetComponent<GCLLMaster>().AddInstance((Bullet)this);
        directionV = Angle2Vector(direction);
    }

    private void FixedUpdate()
    {
        if (!timeStopped)
        {
            MoveBulletY();
        }
        else
        {
            coords.position += directionV * transSpeed * Time.deltaTime;
        }
    }

    internal override void StopTime(bool isStopped)
    {
        base.StopTime(isStopped);
        if (Mathf.Abs(coords.position.x) >= 5.5f || Mathf.Abs(coords.position.y) >= 5.5f)
        {
            Destroy(gameObject);
        }
        if(isStopped)
        {
            hitbox.enabled = false;
            spriteRenderer.sprite = passiveState;
            if (cancelBurst)
            {
                cancelBurst = false;
            }
            else
            {
                Instantiate(clone, coords.position, coords.rotation);
                Instantiate(clone1, coords.position, coords.rotation);
                Instantiate(clone2, coords.position, coords.rotation);
                Instantiate(clone3, coords.position, coords.rotation);
                Destroy(gameObject);
            }
        }
        else
        {
            hitbox.enabled = true;
            spriteRenderer.sprite = normalState;
        }
    }

}
