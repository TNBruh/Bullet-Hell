using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : NPC
{
    [SerializeField] internal Vector3 assignedPosition;
    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        coords.position = Vector3.MoveTowards(coords.position, assignedPosition, GetSpeed());
    }
}
