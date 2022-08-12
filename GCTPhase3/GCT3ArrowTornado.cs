using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCT3ArrowTornado : Bullet
{
    [SerializeField] GameObject yellowArrow;
    [SerializeField] GameObject whiteArrow;
    [SerializeField] float window = 15f;
    float originWindow;
    Quaternion q120;
    Quaternion q240;
    bool allowFire = true;
    int count = 0;
    [SerializeField] internal float expansion = 3f;

    protected override void Start()
    {
        base.Start();
        q120 = Quaternion.Euler(0, 0, 120);
        q240 = Quaternion.Euler(0, 0, 240);
        originWindow = window;
    }

    private void FixedUpdate()
    {
        TurnTransform(GetRotationalSpeed());
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        if (allowFire)
        {
            allowFire = false;
            /*
            if (count % 3 == 0)
            {
                Instantiate(whiteArrow, coords.position, coords.rotation);
                Instantiate(whiteArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, window));
                Instantiate(whiteArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, -window));

                Instantiate(yellowArrow, coords.position, coords.rotation * q120);
                Instantiate(yellowArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, window));
                Instantiate(yellowArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, -window));

                Instantiate(yellowArrow, coords.position, coords.rotation * q240);
                Instantiate(yellowArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, window));
                Instantiate(yellowArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, -window));
            }
            else if (count % 3 == 1)
            {
                Instantiate(yellowArrow, coords.position, coords.rotation);
                Instantiate(yellowArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, window));
                Instantiate(yellowArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, -window));

                Instantiate(whiteArrow, coords.position, coords.rotation * q120);
                Instantiate(whiteArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, window));
                Instantiate(whiteArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, -window));

                Instantiate(yellowArrow, coords.position, coords.rotation * q240);
                Instantiate(yellowArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, window));
                Instantiate(yellowArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, -window));
            }
            else
            {
                Instantiate(yellowArrow, coords.position, coords.rotation);
                Instantiate(yellowArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, window));
                Instantiate(yellowArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, -window));

                Instantiate(yellowArrow, coords.position, coords.rotation * q120);
                Instantiate(yellowArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, window));
                Instantiate(yellowArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, -window));

                Instantiate(whiteArrow, coords.position, coords.rotation * q240);
                Instantiate(whiteArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, window));
                Instantiate(whiteArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, -window));
            }*/
            if (count % 17 == 0)
            {
                Instantiate(whiteArrow, coords.position, coords.rotation * q240);
                Instantiate(whiteArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, window));
                Instantiate(whiteArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, -window));

                Instantiate(whiteArrow, coords.position, coords.rotation * q120);
                Instantiate(whiteArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, window));
                Instantiate(whiteArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, -window));

                Instantiate(whiteArrow, coords.position, coords.rotation);
                Instantiate(whiteArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, window));
                Instantiate(whiteArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, -window));


            }
            else
            {
                Instantiate(yellowArrow, coords.position, coords.rotation);
                Instantiate(yellowArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, window));
                Instantiate(yellowArrow, coords.position, coords.rotation * Quaternion.Euler(0, 0, -window));

                Instantiate(yellowArrow, coords.position, coords.rotation * q120);
                Instantiate(yellowArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, window));
                Instantiate(yellowArrow, coords.position, coords.rotation * q120 * Quaternion.Euler(0, 0, -window));

                Instantiate(yellowArrow, coords.position, coords.rotation * q240);
                Instantiate(yellowArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, window));
                Instantiate(yellowArrow, coords.position, coords.rotation * q240 * Quaternion.Euler(0, 0, -window));
            }
            count++;
            /*
            window += expansion;
            if (window >= 120)
            {
                expansion *= -1;
            }
            else if (window <= originWindow)
            {
                expansion = Mathf.Abs(expansion);
            }*/
            yield return new WaitForSeconds(recoil);
            allowFire = true;
        }
    }
}
