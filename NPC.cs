using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    [SerializeField] internal GameObject[] rawFixedPoints;
    internal Vector2[] fixedPoints;

    
    private void Start()
    {
        fixedPoints = new Vector2[rawFixedPoints.Length];
        for (int i = 0; i < rawFixedPoints.Length; i++)
        {
            fixedPoints[i] = rawFixedPoints[i].transform.position;
        }
        
    }

    internal void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject leObject = collision.gameObject;
        TryGetComponent<Entity>(out Entity leScript);
        if (leScript && leScript.isFriendly && leObject.tag == "Bullet")
        {
            Damage(leScript.GetDamage(), false);
        }
    }
}
