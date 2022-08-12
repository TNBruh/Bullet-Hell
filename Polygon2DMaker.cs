using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon2DMaker : MonoBehaviour
{
    internal PolygonCollider2D polyCollider = new PolygonCollider2D();
    internal TrailRenderer trailRenderer;
    internal float minVertexDistance;
    internal AnimationCurve func;
    internal float multiplier;
    internal float zRotation;
    internal Vector2 worldPos;
    internal List<Vector2> leftSide = new List<Vector2>();
    internal List<Vector2> rightSide = new List<Vector2>();
    internal Vector2[] tempFormed;
    internal List<Vertex> pairedVertices = new List<Vertex>();
    internal Vertex singlePairVertices;
    internal Vector3 lastPosition;
    internal Vector3 currentPosition;
    internal float lifeTime;
    GameObject colliderObj;
    GameObject pseudoParent;
    [SerializeField] string objTag = "Bullet";
    bool haveSpawn = false;
    [SerializeField] internal float interval = 0.4f;
    [SerializeField] internal bool allowSpawn = true;
    [SerializeField] float shrinker = 0;

    static internal Vector2 nullVector = new Vector2(-1, 0);

    internal void ShaveNullVector()
    {
        leftSide.RemoveAll((Vector2 i) => i == nullVector);
        rightSide.RemoveAll((Vector2 i) => i == nullVector);
    }

    internal void AutoSpawnVertex()
    {
        if (Mathf.Abs(Vector3.Distance(lastPosition, currentPosition)) >= minVertexDistance && allowSpawn/*!haveSpawn*/)
        {
            Debug.Log(">>Spawned New<<");
            /*
            lastPosition = currentPosition;
            polyVertex = new Vert(currentPosition, gameObject.transform.rotation.eulerAngles.z, func, lifeTime, multiplier);
            leftSide.Add(polyVertex.leftVector);
            rightSide.Add(polyVertex.rightVector);
            */
            /*
            Debug.Log("spawned");
            Debug.Log(minVertexDistance);
            Debug.Log(lastPosition + " last position");
            Debug.Log(currentPosition + " current position");
            Debug.Log(Vector3.Distance(lastPosition, currentPosition));
            */
            lastPosition = currentPosition;
            singlePairVertices = new Vertex(currentPosition, zRotation, func, lifeTime, multiplier, shrinker);
            /*
            leftSide.Add(singlePairVertices.leftVector);
            rightSide.Add(singlePairVertices.rightVector);
            */
            pairedVertices.Add(singlePairVertices);
            //polyVertex = new Vertex()
            //haveSpawn = true;
            StartCoroutine(StartSpawnTimer());
        }
    }

    IEnumerator StartSpawnTimer()
    {
        allowSpawn = false;
        yield return new WaitForSeconds(interval);
        allowSpawn = true;
    }

    internal void Nullifier()
    {
        pairedVertices.RemoveAll((Vertex i) =>
        {
            //Debug.Log(i.SupposedToBeRemoved());
            return i.SupposedToBeRemoved();
        });
    }

    internal void FormTempList()
    {
        int count = pairedVertices.Count;
        int countDouble = count * 2;
        tempFormed = new Vector2[countDouble];
        for (int i = 0; i < count; i++)
        {
            tempFormed[i] = pairedVertices[i].leftVector;
            tempFormed[countDouble - (1 + i)] = pairedVertices[i].rightVector;
            Debug.Log(pairedVertices[i].leftVector + "left");
            Debug.Log(pairedVertices[i].leftVector + "right");
            /*
            Debug.Log(tempFormed.Length + " length");
            Debug.Log(i + " count");
            Debug.Log(tempFormed[i].x + " x loc");
            */
        }
    }

    internal void PolygonMaker(List<Vector2> inpVerticesLeft, List<Vector2> inpVerticesRight, ref PolygonCollider2D inpCollider)
    {
        inpVerticesRight.Reverse();
        inpVerticesLeft.AddRange(inpVerticesRight);
        //>>remember to turn this on<<
        //inpCollider.SetPath(0, inpVerticesLeft);
    }

    internal void PolygonMaker(Vector2[] inpVertices, ref PolygonCollider2D inpCollider)
    {
        inpCollider.SetPath(0, inpVertices);
    }

    internal void CraftPolygon()
    {
        //>>remember to turn this on<<
        polyCollider.SetPath(0, tempFormed);
    }

    internal void UpdateVertices()
    {
        /*
        foreach (Vertex i in pairedVertices)
        {
            i.TimeSinceSpawnIncrement();
            i.CalculateVertices();
            //i.DebugReport();
        }
        */
        for (int i = 0; i < pairedVertices.Count; i++)
        {
            pairedVertices[i].TimeSinceSpawnIncrement();
            pairedVertices[i].CalculateVertices();
        }
    }

    private void Start()
    {
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        minVertexDistance = trailRenderer.minVertexDistance;
        lifeTime = trailRenderer.time;
        func = trailRenderer.widthCurve;
        multiplier = trailRenderer.widthMultiplier;

        Transform objCoords = gameObject.transform;

        zRotation = objCoords.rotation.eulerAngles.z;
        worldPos = objCoords.position;

        lastPosition = worldPos;
        currentPosition = worldPos;

        pseudoParent = gameObject;
        colliderObj = new GameObject();
        colliderObj.tag = objTag;
        polyCollider = colliderObj.AddComponent<PolygonCollider2D>();
        polyCollider.isTrigger = true;

    }

    private void FixedUpdate()
    {
        currentPosition = gameObject.transform.position;
        AutoSpawnVertex();
        Nullifier();
        FormTempList();
        CraftPolygon();
        UpdateVertices();
    }

    private class Vert
    {
        /*
        private void FixedUpdate()
        {
            TimeSinceSpawnIncrement();
            if (SupposedToBeRemoved())
            {
                leftVector = nullVector;
                rightVector = nullVector;
            }
            vertexPair = CalculateVertices();
            leftVector = vertexPair[0];
            rightVector = vertexPair[1];
        }
        */


    }



    internal class Vertex
    {
        internal Vector3 spawnLoc;
        internal float zRotation;
        internal AnimationCurve func;
        internal float timeSinceSpawn;
        internal float lifeTime;
        internal float multiplier;
        internal float shrinker;
        internal Vector2[] vertexPair;
        internal Vector2 leftVector;
        internal Vector2 rightVector;
        static internal Vector2 nullVector = new Vector3(-1, 0);

        internal Vector2[] CalculateVertices()
        {
            float funcResult = func.Evaluate(timeSinceSpawn) * multiplier - shrinker;
            Debug.Log(timeSinceSpawn + " since spawn");
            Debug.Log(func.Evaluate(timeSinceSpawn) * multiplier + " function");
            Debug.Log(func.Evaluate(timeSinceSpawn) * multiplier + spawnLoc.x + " function 2");
            if (funcResult < 0)
            {
                funcResult = 0;
            }
            Vector3 rightSide = spawnLoc + RotatePoint(zRotation, new Vector3(funcResult, 0));
            Vector3 leftSide = spawnLoc - RotatePoint(zRotation, new Vector3(funcResult, 0));
            leftVector = leftSide;
            rightVector = rightSide;
            //Vector2 rightSide = new Vector2(spawnLoc.x, spawnLoc.y) + new Vector2(RotatePoint(zRotation, new Vector3(funcResult, 0)).x, 0);
            //Vector2 leftSide = new Vector2(spawnLoc.x, spawnLoc.y) - new Vector2(RotatePoint(zRotation, new Vector3(funcResult, 0)).x, 0);
            /*
            Debug.Log(rightSide);
            Debug.Log(leftSide);
            */
            Vector2[] result = { leftSide, rightSide };
            return result;
        }

        internal Vector3 RotatePoint(float angle, Vector3 pt)
        {
            //Debug.Log(pt);
            float a = angle * Mathf.PI / 180f;
            float cosa = Mathf.Cos(a), sina = Mathf.Sin(a);
            //Debug.Log(pt.x * cosa - pt.y * sina);
            //Debug.Log(pt.x * sina + pt.y * cosa);
            return new Vector3(pt.x * cosa - pt.y * sina, pt.x * sina + pt.y * cosa);
        }

        internal Vertex(Vector3 inpSpawnLoc, float inpZRotation, AnimationCurve inpFunc, float inpLifeTime, float inpMultiplier, float inpShrinker = 0)
        {
            spawnLoc = inpSpawnLoc;
            zRotation = inpZRotation;
            func = inpFunc;
            lifeTime = inpLifeTime;
            multiplier = inpMultiplier;
            shrinker = inpShrinker;
            //vertexPair = CalculateVertices();
            CalculateVertices();
            //leftVector = vertexPair[0];
            //rightVector = vertexPair[1];
        }


        internal void TimeSinceSpawnIncrement()
        {
            timeSinceSpawn = Mathf.Clamp(timeSinceSpawn + Time.deltaTime, 0, lifeTime);
            /*
            Debug.Log(timeSinceSpawn);
            */
        }

        internal bool SupposedToBeRemoved()
        {
            //Debug.Log(timeSinceSpawn);
            return (timeSinceSpawn >= lifeTime);
        }

        internal void DebugReport()
        {
            Debug.Log(leftVector + "left vertex");
            Debug.Log(rightVector + "right vertex");
        }

        ~Vertex()
        {
            Debug.Log("destroyed");
        }
    }
}
