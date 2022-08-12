using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    /*
     * DEV DIARY
     * this is probably the 4th time i re-designed the underlying mechanics of every entity. do i hate myself?
     * i can't seem to be satisfied with anything i make. damn, i wish i'm secretly not a perfectionist.
     * all these reworks are killing me slowly.
     */
    [SerializeField] internal Rigidbody2D body;
    [SerializeField] internal Collider2D hitbox;
    [SerializeField] internal Transform coords;
    [SerializeField] internal bool isFriendly = true;
    [SerializeField] internal float atkPoint = 1f;
    [SerializeField] internal float atkMultiplier = 1f;
    [SerializeField] internal float speed = 10f;
    [SerializeField] internal float boost = 1f;
    [SerializeField] internal GameObject enemy;
    internal float originalBoost;
    [SerializeField] internal bool canMove = true;
    internal string leTag;
    //[SerializeField] internal Effect effectHandler;
    [SerializeField] internal bool timeStopped = false;
    [SerializeField] internal int timeStoppedMult = 1;


    virtual protected void Awake()
    {
        if (!body)
        {
            body = gameObject.GetComponent<Rigidbody2D>();
        }
        if (!hitbox)
        {
            hitbox = gameObject.GetComponent<Collider2D>();
        }
        //if (effectHandler is null)
        //{
        //    effectHandler = gameObject.GetComponent<Effect>();
        //}
        if (!coords)
        {
            coords = gameObject.GetComponent<Transform>();
        }
        if (string.Equals(gameObject.tag, "NPC"))
        {
            isFriendly = false;
        }
        leTag = gameObject.tag;
        if (!enemy)
        {
            if (isFriendly)
            {
                enemy = GameObject.FindWithTag("NPC");
            }
            else
            {
                enemy = GameObject.FindWithTag("Player");
            }
        }
    }
    virtual protected void Start()
    {
        if (!enemy)
        {
            if (isFriendly)
            {
                enemy = GameObject.FindWithTag("NPC");
            }
            else
            {
                enemy = GameObject.FindWithTag("Player");
            }
        }
    }

    internal void TurnFriendly(bool fren)
    {
        isFriendly = fren;
    }

    internal float GetDamage()
    {
        return atkPoint * atkMultiplier;
    }

    internal float GetDamageRaw()
    {
        return atkPoint;
    }

    internal void MultiplyAtk(float percentage, float addition)
    {
        atkMultiplier = atkMultiplier * percentage + addition;
    }

    internal void NormalizeAtkMult()
    {
        atkMultiplier = 1f;
    }

    internal void MultiplyBoost(float percentage, float addition)
    {
        boost = boost * percentage + addition;
    }

    internal void NormalizeBoost()
    {
        boost = 1f;
    }

    internal float GetSpeed()
    {
        return speed * boost * timeStoppedMult;
    }

    virtual internal void StopTime(bool isStopped)
    {
        timeStopped = isStopped;
        if (isStopped)
        {
            timeStoppedMult = 0;
        }
        else
        {
            timeStoppedMult = 1;
        }
    }
    /*
    internal int GetLayerInt(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }

    internal int AddLayer(int[] layers)
    {
        int result = layers[0];
        for (int i = 1; i < layers.Length; i++)
        {
            result |= 1 << layers[i];
        }
        return result;
    }

    internal void ChangeObjLayer(GameObject obj, int chosenLayer)
    {
        obj.layer = chosenLayer;
    }

    internal void AddObjLayer(GameObject obj, int chosenLayer)
    {
        Debug.LogWarning(obj.layer);
        Debug.LogWarning(1 << 20);
        Debug.LogWarning(1 << 11);
        Debug.LogWarning(1 << 20 | 1 << 11);
        obj.layer |= 1 << chosenLayer;
    }*/

}
