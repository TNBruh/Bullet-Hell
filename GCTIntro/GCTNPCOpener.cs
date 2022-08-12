using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTNPCOpener : Bullet
{
    [SerializeField] float slowSpeed = 0.1f;
    float normalSpeed;
    [SerializeField] float startDirection = 90;
    Vector3 startVect;
    [SerializeField] Vector3 endPos = new Vector3(5, 5, 0);
    bool isStarting = true;
    float prog = 0;
    SpriteRenderer spriteRenderer;
    float totalAlpha = 0;
    [SerializeField] float opaqueSpeed = 0.1f;

    protected override void Start()
    {
        base.Start();
        normalSpeed = speed;
        speed = slowSpeed;
        startVect = Angle2Vector(startDirection);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    private void FixedUpdate()
    {
        if (spriteRenderer.color.a < 1)
        {
            coords.position += startVect * speed * Time.deltaTime;
            totalAlpha += opaqueSpeed * Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, totalAlpha);
        }
        else
        {
            prog = Mathf.Clamp(prog + GetSpeed(), 0, 1);
            speed = normalSpeed;
            coords.position = Vector3.Lerp(coords.position, endPos, prog * Time.deltaTime);
        }
    }
}
