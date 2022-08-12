using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCStabilizer : MonoBehaviour
{
    float curRot;
    Transform p;
    [SerializeField] float rotationSpeed = 60f;

    private void Start()
    {
        p = transform.parent;
        curRot = p.rotation.eulerAngles.z;
        //FaceUp();
    }

    private void FixedUpdate()
    {
        //FaceUp();
        transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
    }

    internal void FaceUp()
    {
        transform.rotation *= Quaternion.Euler(0, 0, -curRot);
    }
}
