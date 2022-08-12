using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    [SerializeField] int amt = 100000;
    GameObject chld;

    // Start is called before the first frame update
    void Start()
    {
        chld = gameObject.transform.GetChild(0).gameObject;
        TestFind();
        TestGetComponent();
        TestGetChildren();
        TestGetComponentInChildren();
        TestSetActive();
    }

    internal void TestGetComponent()
    {
        var timeStart = Time.realtimeSinceStartup;
        for (int i = 0; i < amt; i++)
        {
            chld.GetComponent<TGCKnifePurple>();
        }
        Debug.Log(Time.realtimeSinceStartup - timeStart + " getcomponent");
    }

    internal void TestFind()
    {
        var timeStart = Time.realtimeSinceStartup;
        for (int i = 0; i < amt; i++)
        {
            GameObject.FindWithTag("NPC");
        }
        Debug.Log(Time.realtimeSinceStartup - timeStart + " find");
    }

    internal void TestGetChildren()
    {
        var timeStart = Time.realtimeSinceStartup;
        for (int i = 0; i < amt; i++)
        {
            gameObject.transform.GetChild(0);
        }
        Debug.Log(Time.realtimeSinceStartup - timeStart + " getchildren");
    }

    internal void TestGetComponentInChildren()
    {
        var timeStart = Time.realtimeSinceStartup;
        for (int i = 0; i < amt; i++)
        {
            gameObject.transform.GetComponentInChildren<TGCKnifePurple>(true);
        }
        Debug.Log(Time.realtimeSinceStartup - timeStart + " getcomponentinchildren");
    }
    internal void TestSetActive()
    {
        var timeStart = Time.realtimeSinceStartup;
        for (int i = 0; i < amt; i++)
        {
            chld.SetActive(true);
        }
        Debug.Log(Time.realtimeSinceStartup - timeStart + " setactive");
    }
}
