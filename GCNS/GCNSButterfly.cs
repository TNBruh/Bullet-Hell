using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCNSButterfly : Bullet
{
    [SerializeField] internal float r = 6.5f;
    [SerializeField] GameObject crashOrb;
    Vector3 pos;
    Vector3 nextPos;
    bool isHitting = false;
    bool isLaunching = false;
    bool allowFire = true;
    Quaternion q90 = Quaternion.Euler(0, 0, 90);
    Quaternion q_90 = Quaternion.Euler(0, 0, -90);
    LineRenderer line;

    protected override void Start()
    {
        base.Start();
        pos = new Vector3(0, r, 0);
        coords.position = new Vector3(0, r, 0);
        line = gameObject.GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.enabled = false;
        //nextPos = RotatePoint(Random.Range(0, 360), pos);

        StartCoroutine(Launch());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (!isHitting && collided.tag == "Barrier")
        {
            isHitting = true;
            Instantiate(crashOrb, coords.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isHitting)
        {
            isHitting = false;
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(coords.position.x) < 7 && Mathf.Abs(coords.position.y) < 7 && isLaunching)
        {
            MoveBulletY();
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

}
