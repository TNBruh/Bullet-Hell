using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPurpleOrb : Bullet
{
    [SerializeField] float launchSpeed;
    [SerializeField] float launchAngle;
    //accumulated speed
    internal float gravAddX = 0;
    internal float gravAddY = 0;
    //acceleration
    [SerializeField] float xAccel = 0;
    [SerializeField] float yAccel = 0;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //IfOperation();
        //RawOperation();
        Vector3 conversion = Angle2Vector(launchAngle);
        //Debug.Log(conversion);
        gravAddX = conversion.x * launchSpeed;
        gravAddY = conversion.y * launchSpeed;
    }

    private void FixedUpdate()
    {
        CustomMove();
    }

    private void CustomMove()
    {
        coords.position += (new Vector3(gravAddX, gravAddY) * Time.deltaTime);
        gravAddX += xAccel;
        gravAddY += yAccel;
    }


    /*
    private void IfOperation()
    {
        var timeStart = Time.realtimeSinceStartup;
        int count = 0;
        Vector3 vect;
        while (count < totalOperation)
        {
            vect = new Vector3(coords.position.x + movX, coords.position.y + movY);
            if (verticalGrav)
            {
                movY *= gravAdd;
            }
            else
            {
                movX *= gravAdd;
            }
            count++;
        }
        Debug.Log(Time.realtimeSinceStartup - timeStart);
    }

    private void RawOperation()
    {
        var timeStart = Time.realtimeSinceStartup;
        int count = 0;
        Vector3 vect;
        while (count < totalOperation)
        {
            vect = new Vector3(coords.position.x + movX * gravAddX, coords.position.y + movY * gravAddY);
            count++;
        }
        Debug.Log(Time.realtimeSinceStartup - timeStart);
    }
    */
}
