using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript0 : Bullet
{

    //private new PolygonCollider2D collider;
    // Start is called before the first frame update
    TrailRenderer a;
    Vector3[] vertexPoints;
    bool canPrint = true;
    float count = 0;

    override protected void Start()
    {
        //collider.
        a = GetComponent<TrailRenderer>();

    }

    private void FixedUpdate()
    {
        MoveBulletYTransform();
        //StartCoroutine(PrintPos());
        PrintPos2();
    }
    IEnumerator PrintPos()
    {
        if (canPrint)
        {
            canPrint = false;
            vertexPoints = new Vector3[a.positionCount];
            int b = a.GetPositions(vertexPoints);
            Debug.Log(b + " b");
            foreach (var i in vertexPoints)
            {
                Debug.Log(i);
            }
            yield return new WaitForSeconds(recoil);
            canPrint = true;
        }
    }

    internal void PrintPos2()
    {
        canPrint = false;
        vertexPoints = new Vector3[a.positionCount];
        int b = a.GetPositions(vertexPoints);
        //Debug.Log(b + " b");
        /*
        foreach (var i in vertexPoints)
        {
            Debug.Log(i);
        }
        
        */
        //Debug.Log("count " + count);
        //Debug.Log(a.widthCurve.Evaluate(count) * a.widthMultiplier);
        count += 0.1f;
        if (count > 1)
        {
            count = 0;
        }
    }
}
