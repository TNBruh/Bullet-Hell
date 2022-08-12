using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTNuke2 : Bullet
{
    [SerializeField] internal float r = 6.5f;
    [SerializeField] GameObject orb;
    Vector3 pos;
    Vector3 nextPos;
    bool isHitting = false;
    bool isLaunching = false;
    bool allowFire = true;
    Quaternion q90 = Quaternion.Euler(0, 0, 90);
    Quaternion q_90 = Quaternion.Euler(0, 0, -90);
    LineRenderer line;
    Transform child;

    protected override void Start()
    {
        base.Start();
        pos = new Vector3(0, r, 0);
        coords.position = new Vector3(0, r, 0);
        line = gameObject.GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.enabled = false;
        //nextPos = RotatePoint(Random.Range(0, 360), pos);
        child = coords.GetChild(0);

        StartCoroutine(Launch());
    }
    private void FixedUpdate()
    {
        if (isLaunching)
        {
            MoveBulletY();
        }
        if (coords.childCount == 0)
        {
            Destroy(gameObject);
        }
        //StartCoroutine(Launch());
    }


    IEnumerator Launch()
    {
        coords.position = RotatePoint(Random.Range(0, 360), pos);
        LookAtObject(enemy.transform.position);


        //line.SetPosition(1, -coords.position + enemy.transform.position);
        line.enabled = true;
        yield return new WaitForSeconds(recoil);
        line.enabled = false;
        isLaunching = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (!isHitting && collided.tag == "Barrier")
        {
            isHitting = true;
            //coords.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Instantiate(orb, new Vector3(Mathf.Clamp(child.position.x, -4.6f, 4.6f), Mathf.Clamp(child.position.y, -4.9f, 4.9f)), new Quaternion());
            Instantiate(orb, new Vector3(Mathf.Clamp(child.position.x, -4.6f, 4.6f), Mathf.Clamp(child.position.y, -4.9f, 4.9f)), Quaternion.Euler(0, 0, 45));
            Instantiate(orb, new Vector3(Mathf.Clamp(child.position.x, -4.6f, 4.6f), Mathf.Clamp(child.position.y, -4.9f, 4.9f)), Quaternion.Euler(0, 0, 90));
            Instantiate(orb, new Vector3(Mathf.Clamp(child.position.x, -4.6f, 4.6f), Mathf.Clamp(child.position.y, -4.9f, 4.9f)), Quaternion.Euler(0, 0, 135));
            Instantiate(orb, new Vector3(Mathf.Clamp(child.position.x, -4.6f, 4.6f), Mathf.Clamp(child.position.y, -4.9f, 4.9f)), Quaternion.Euler(0, 0, 180));
            Instantiate(orb, new Vector3(Mathf.Clamp(child.position.x, -4.6f, 4.6f), Mathf.Clamp(child.position.y, -4.9f, 4.9f)), Quaternion.Euler(0, 0, 225));
            Instantiate(orb, new Vector3(Mathf.Clamp(child.position.x, -4.6f, 4.6f), Mathf.Clamp(child.position.y, -4.9f, 4.9f)), Quaternion.Euler(0, 0, 270));
            Instantiate(orb, new Vector3(Mathf.Clamp(child.position.x, -4.6f, 4.6f), Mathf.Clamp(child.position.y, -4.9f, 4.9f)), Quaternion.Euler(0, 0, 315));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isHitting)
        {
            isHitting = false;
        }
    }
}
