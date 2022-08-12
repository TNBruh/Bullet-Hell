using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTSpark : Bullet
{
    [SerializeField] internal Vector3 maxSize = new Vector3(0.66f, 2.5f, 1f);
    [SerializeField] internal Vector3 startSize = new Vector3(0.01f, 2.5f, 1f);
    [SerializeField] internal float expandRate = 1;
    float xMaxVal = 0;
    float xVal = 0;

    // Start is called before the first frame update
    private void OnEnable()
    {
        maxSize = coords.localScale;
        xMaxVal = maxSize.x;
        xVal = startSize.x;
        coords.localScale = new Vector3(xVal, startSize.y, 1);
    }

    private void FixedUpdate()
    {
        if (xVal < xMaxVal)
        {
            coords.localScale = new Vector3(xVal, startSize.y, 1);
            xVal = Mathf.Clamp(xVal + expandRate * Time.deltaTime, xVal, xMaxVal);
        }
    }
}
