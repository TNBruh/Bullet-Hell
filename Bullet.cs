using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : Entity
{
    /*
     * note to self:
     * can't make exponential homing function. so, that falls under custom bullet pattern.
     * create your own unique destructors bcuz it can't be virtualized
     * =end of note=
     * 
     * my single-celled brain cannot decide whether to use angularVelocity or MoveRotation. 
     * so there might be a big difference in Turn and Homing physics-wise.
     * i think i need to import cultured brain cells to boost my room temperature iq.
     * 
     * DO NOT USE ANGULAR VELOCITY TO CHANGE ROTATION
     */

    [SerializeField] internal float recoil = 0.4f;
    [SerializeField] internal float rotationalSpeed = 200;
    [SerializeField] internal Vector3 vectZero = new Vector3();
    [SerializeField] internal float rotationalBoost = 1;
    [SerializeField] internal Quaternion qZero = Quaternion.Euler(0, 0, 0);

    /*
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }*/

    internal virtual void MoveBulletY(float vertical = 1)
    {
        body.velocity = coords.up * GetSpeed() * Time.deltaTime * vertical;
    }

    internal virtual void MoveBulletYTransform(float vertical = 1)
    {
        coords.position += coords.up * GetSpeed() * Time.deltaTime * vertical;
    }

    internal virtual void MoveBulletX(float horizontal = 1)
    {
        body.velocity = coords.right * GetSpeed() * Time.deltaTime * horizontal;
    }

    internal virtual void MoveBulletXTransform(float horizontal = 1)
    {
        coords.position += coords.right * GetSpeed() * Time.deltaTime * horizontal;
    }

    internal void LookAtObject(Vector3 target)
    {
        Quaternion rotationToTarget;
        Vector3 direction = target - coords.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotationToTarget = Quaternion.Euler(0, 0, targetAngle - 90);
        //Debug.Log(targetAngle - 90);

        coords.rotation = rotationToTarget;
        //body.MoveRotation(rotationToTarget);
    }

    internal void Turn(float rotationSpeed, float inpTime = 60)
    {
        Quaternion nextRotation = Quaternion.Euler(coords.rotation.eulerAngles + new Vector3(0, 0, rotationSpeed));
        //Debug.Log(nextRotation.eulerAngles);
        float slerpSpeed = (1 / inpTime) * Mathf.Abs(rotationSpeed) * Time.deltaTime;
        Quaternion slerpVar = Quaternion.SlerpUnclamped(coords.rotation, nextRotation, slerpSpeed);
        body.MoveRotation(slerpVar);
    }

    internal void TurnTransform(float rotationSpeed, float inpTime = 60)
    {
        //Quaternion nextRotation = Quaternion.Euler(coords.rotation.eulerAngles + new Vector3(0, 0, rotationSpeed));
        /*
         * deprecated due to expensive calculations. still cool though.
         * Quaternion nextRotation = Quaternion.Euler(coords.forward) * Quaternion.Euler(new Vector3(0, 0, rotationSpeed));
         * float slerpSpeed = (1 / inpTime) * rotationSpeed * Time.deltaTime;
         * Quaternion slerpVar = Quaternion.Slerp(Quaternion.Euler(coords.forward), nextRotation, slerpSpeed);
         */



        coords.Rotate(coords.forward * rotationSpeed * Time.deltaTime);
        //coords.forward * rotationSpeed * Time.deltaTime
    }

    /// <summary>
    /// Homes on targeted GameObject. Time.deltaTime already applied.
    /// </summary>
    /// <param name="target">The target</param>
    /// <param name="rotationSpeed">I AM SPEED</param>
    /// <param name="inpTime">Unit of time it takes to rotate</param>
    internal void Homing(Vector3 target, float rotationSpeed, float inpTime = 60)
    {
        Quaternion rotationToTarget;
        Vector3 direction = target - coords.position;
        //Quaternion lookAt = Quaternion.LookRotation(direction.normalized);
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Vector3 localisedTargetSpace = coords.InverseTransformPoint(target);
        /*
        Quaternion rotationToTarget = ((lookAt.eulerAngles != ninetyLeft && lookAt.eulerAngles != ninetyRight) ? 
            (lookAt * Quaternion.AngleAxis(((direction.x > 0) ? -90 : 90), coords.forward))
            : (lookAt.eulerAngles == ninetyLeft ? Quaternion.AngleAxis(90, coords.forward) : Quaternion.AngleAxis(-90, coords.forward)));
        */
        rotationToTarget = Quaternion.Euler(0, 0, targetAngle - 90);
        //body.MoveRotation(rotationToTarget);

        //Debug.LogWarning(coords.InverseTransformPoint(target.transform.position));
        //Debug.LogWarning(direction);
        //Debug.LogWarning(rotationToTarget.eulerAngles);
        //Debug.LogWarning(coords.rotation.eulerAngles.z);
        //Debug.LogWarning(lookAt.eulerAngles);
        float slerpSpeed = (1 / inpTime) * rotationSpeed * Time.deltaTime;
        body.MoveRotation(Quaternion.Slerp(coords.rotation, rotationToTarget, slerpSpeed));

        MoveBulletY();
    }

    internal void NeoHoming(Vector3 target, float progPercent)
    {
        Quaternion rotationToTarget;
        Vector3 direction = target - coords.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotationToTarget = Quaternion.Euler(0, 0, targetAngle - 90);
        coords.transform.rotation = Quaternion.Slerp(coords.rotation, rotationToTarget, progPercent);

    }

    internal float CalcHalfCircumference(float axis1, float axis2)
    {
        return (Mathf.PI * (3 * (axis1 + axis2) - Mathf.Sqrt((3 * axis1 + axis2) * (axis1 + 3 * axis2)))) / 2;
    }

    /// <summary>
    /// Calculates half of the elliptical path's circumference then uses it to find the velocity.
    /// Assumes the said velocity is angular velocity.
    /// </summary>
    /// <param name="halfEllipseLength">Arch path towards target</param>
    /// <param name="angle">Rotation needed for the bullet to look at target</param>
    /// <returns>Angular velocity for Homing</returns>
    internal float MaxSpeedRecommendation(float halfEllipseLength, float angle = 45)
    {
        float rad = angle * (Mathf.PI / 180);
        float height = halfEllipseLength * Mathf.Tan(rad);

        float halfCirc = CalcHalfCircumference(halfEllipseLength, height);

        return halfEllipseLength;
    }

    /// <summary>
    /// Uses Kepler's 2nd law to calculate the recommended angular speed of an elliptical path
    /// </summary>
    /// <param name="perihelion">Shortest distance to "the star"</param>
    /// <param name="aphelion">Farthest distance to "the star"</param>
    /// <param name="time">Time needed to orbit around "the star"</param>
    /// <param name="r">Distance of bullet to "the star"</param>
    /// <returns>Angular velocity for Homing</returns>
    internal float KeplerPathRecommendation(float perihelion, float aphelion, float time, float r)
    {
        float sect1 = Mathf.PI / (time * Mathf.Pow(r, 2));
        float sect2 = (perihelion + aphelion) * Mathf.Sqrt(perihelion * aphelion);

        return sect1 * sect2 * time * 2;
    }

    /// <summary>
    /// Calculates the recommended upper boundary for angular velocity
    /// </summary>
    /// <param name="perihelion">Shortest distance to "the star"</param>
    /// <param name="aphelion">Farthest distance to "the star"</param>
    /// <param name="time">Time needed to orbit around "the star"</param>
    /// <returns>Angular velocity for Homing</returns>
    internal float KeplerPathMax(float perihelion, float aphelion, float time)
    {
        return KeplerPathRecommendation(perihelion, aphelion, time, perihelion);
    }

    /// <summary>
    /// Calculates the recommended average for angular velocity
    /// </summary>
    /// <param name="perihelion">Shortest distance to "the star"</param>
    /// <param name="aphelion">Farthest distance to "the star"</param>
    /// <param name="time">Time needed to orbit around "the star"</param>
    /// <returns>Angular velocity for Homing</returns>
    internal float KeplerPathAverage(float perihelion, float aphelion, float time)
    {
        float max = KeplerPathMax(perihelion, aphelion, time);
        float min = KeplerPathRecommendation(perihelion, aphelion, time, aphelion);

        return (max + min) / 2;
    }
    

    internal void NonLinearMovement(Vector2 direction)
    {
        //float newX = (coords.position.x + xBoost);
        //float newY = (coords.position.y + yBoost);

        Vector3 nextPath = direction * GetSpeed() * Time.deltaTime;
        body.MovePosition(coords.position + nextPath);
        //transform.position + transform.right * Time.deltaTime
    }

    internal void LocalNonLinearMovement(Vector2 direction)
    {
        //float newX = (coords.position.x + xBoost);
        //float newY = (coords.position.y + yBoost);
        Vector3 rotated = RotatePoint(coords.rotation.eulerAngles.z, direction);
        Vector3 nextPath = new Vector2(rotated.x, rotated.y) * GetSpeed() * Time.deltaTime;
        //Debug.Log(nextPath);
        body.MovePosition(coords.position + nextPath);
        //transform.position + transform.right * Time.deltaTime
    }

    internal void LocalNonLinearMovementTransform(Vector3 direction)
    {
        //Vector3 rotated = RotatePoint(coords.rotation.eulerAngles.z, direction);
        //Vector3 nextPath = new Vector2(rotated.x, rotated.y) * GetSpeed() * Time.deltaTime;
        //Debug.Log(nextPath);
        //body.MovePosition(coords.position + nextPath);
        //transform.position + transform.right * Time.deltaTime
        transform.position = transform.position + direction * Time.deltaTime * GetSpeed();
    }

    virtual internal void SelfDestruct()
    {
        Destroy(gameObject);
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

    internal Vector3 SideSteppedTargeting(float sidestep, Vector3 target)
    {
        float dist = Vector3.Distance(coords.position, target);
        float hypo = Mathf.Sqrt(Mathf.Pow(dist, 2f) + Mathf.Pow(sidestep, 2f));
        float arcensin = Mathf.Asin(dist / hypo);
        float flippedAngle = arcensin * Mathf.Rad2Deg;
        return new Vector3(0, 0, 90 - flippedAngle);
    }

    internal float GetRotationalSpeed()
    {
        return rotationalSpeed * rotationalBoost * Time.deltaTime * timeStoppedMult;
    }

    internal void ClampedTurnTransform( float rotationSpeed, float maxAngle, float minAngle = -90)
    {
        /*
        float targetRotation = Mathf.Clamp(coords.eulerAngles.z + rotationSpeed * Time.deltaTime, minAngle, maxAngle);
        targetRotation = Quaternion.RotateTowards();
        */
        //float zRotation = Mathf.Clamp(coords.eulerAngles.z + Time.deltaTime * rotationSpeed, minAngle, maxAngle);
        //Debug.Log(zRotation);
        //transform.eulerAngles = new Vector3(0.0f, 0.0f, zRotation);
        coords.Rotate(coords.forward * rotationSpeed * Time.deltaTime);
    }

    internal Vector3 Angle2Vector(float inpAngle)
    {
        return new Vector3(Mathf.Cos(inpAngle * Mathf.Deg2Rad), Mathf.Sin(inpAngle * Mathf.Deg2Rad));
    }

    internal Vector3 NeoAngle2Vector(float inpAngle)
    {
        //Debug.LogError(inpAngle + 90);
        //Debug.LogError(new Vector3(Mathf.Cos(inpAngle + 90 * Mathf.Deg2Rad), Mathf.Sin(inpAngle + 90 * Mathf.Deg2Rad)));
        return new Vector3(Mathf.Cos((inpAngle + 90) * Mathf.Deg2Rad), Mathf.Sin((inpAngle + 90) * Mathf.Deg2Rad));
    }

    internal Quaternion EnemyAngle(Vector3 target)
    {
        /*
         * Quaternion rotationToTarget;
        Vector3 direction = target - coords.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotationToTarget = Quaternion.Euler(0, 0, targetAngle - 90);
        //Debug.Log(targetAngle - 90);

        coords.rotation = rotationToTarget;
         */
        Quaternion rotationToTarget;
        Vector3 direction = target - coords.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotationToTarget = Quaternion.Euler(0, 0, targetAngle - 90);
        return rotationToTarget;
    }

}
