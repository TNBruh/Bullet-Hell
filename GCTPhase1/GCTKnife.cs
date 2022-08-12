using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GCTKnife : Bullet
{
    [SerializeField] internal GCTP1 masterTGC;
    [SerializeField] bool earlyTimeStop = true;
    [SerializeField] Sprite illusionSprite;
    Sprite normalSprite;
    SpriteRenderer spriteRenderer;
    [SerializeField] float direction;
    [SerializeField] float illusionSpeed = 0.1f;
    Vector3 directionV;
    [SerializeField] bool cancelBurst;
    [SerializeField] GameObject[] illusionClone = new GameObject[4];

    [SerializeField] bool triggerIllusion = false;
    [SerializeField] bool triggerIllusionStop = false;

    // Start is called before the first frame update
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
        //masterTGC = GameObject.FindGameObjectWithTag("Master").GetComponent<GCTP1>();

        //masterTGC.AddInstance((Bullet)this);
    }

    private void FixedUpdate()
    {
        if (timeStopped)
        {
            coords.position += directionV * Time.deltaTime * illusionSpeed;
        }
        //Debug.Log(GetSpeed());

        /*
        if (triggerIllusion)
        {
            triggerIllusion = false;
            StopTime(true);
        }
        if (triggerIllusionStop)
        {
            triggerIllusionStop = false;
            StopTime(false);
        }
        */
        

        MoveBulletY();
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
