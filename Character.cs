using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    /*
     * changing movement
     */
    //[SerializeField] CharacterController characterController;
    [SerializeField] internal float health = 100;
    [SerializeField] internal float healthMult = 1;
    [SerializeField] internal bool invuln = false;
    [SerializeField] internal float invulnTimer = 0.5f;
    [SerializeField] internal GameObject[] BulletObjects;
    [SerializeField] internal GameObject[] rawBulletSpawners;
    [SerializeField] internal int life = 1;
    internal float vuln = 1f;
    internal Vector2[] bulletSpawners;
    internal float currentHealthMax;
    internal float healthMax;

    override protected void Awake()
    {
        base.Awake();
        /*
        if (!characterController)
        {
            characterController = gameObject.GetComponent<CharacterController>();
        }
        */

        healthMax = health * healthMult;
        currentHealthMax = healthMax;

        bulletSpawners = new Vector2[rawBulletSpawners.Length];
        for (int i = 0; i < rawBulletSpawners.Length; i++)
        {
            bulletSpawners[i] = rawBulletSpawners[i].transform.position;
        }
    }

    /*
    /// <summary>
    /// Moves character. Time.deltaTime already applied.
    /// </summary>
    /// <param name="inpMove">Direction</param>
    internal void MoveCharacter(Vector2 inpMove)
    {
        if (canMove)
        {
            characterController.Move(new Vector3(inpMove.x, inpMove.y) * GetSpeed() * Time.deltaTime);
        }
    }
    

    internal void MoveCharacterTo(Vector2 inpLoc)
    {
        Vector2 dir = inpLoc - new Vector2(coords.position.x, coords.position.y);
        dir = dir.normalized;
        MoveCharacter(dir);
    }
    

    /// <summary>
    /// The base for non-linear movement.
    /// Normal boost and Time.deltaTime applied automatically.
    /// Custom boosts multiplied by normal boost must always be lower than inpTime.
    /// </summary>
    /// <param name="inpLoc">Target location</param>
    /// <param name="boostX">Horizontal speed excluding normal boost</param>
    /// <param name="boostY">Vertical speed excluding normal boost</param>
    /// <param name="inpTime">Time it takes to reach target</param>
    internal void MoveCharacterTo(Vector2 inpLoc, float boostX, float boostY, float inpTime = 90)
    {
        float time = (1 / inpTime);
        Vector2 xLerp = Vector2.Lerp(new Vector2(coords.position.x, 0), new Vector2(inpLoc.x, 0), time * boostX);
        Vector2 yLerp = Vector2.Lerp(new Vector2(0, coords.position.y), new Vector2(0, inpLoc.y), time * boostY);
        Vector2 curPosition = new Vector2(xLerp.x, yLerp.y);

        MoveCharacter(curPosition);
    }
    */

    internal void MoveCharacter(Vector2 inpMove)
    {
        if (canMove)
        {
            body.MovePosition(coords.position + new Vector3(inpMove.x, inpMove.y) * GetSpeed() * Time.deltaTime);
        }
    }

    internal void Damage(float inpDmg, bool triggerInvuln = false)
    {
        float dummyHealth = health - (inpDmg * vuln);
        if (!invuln)
        {
            if (dummyHealth <= 0)
            {
                health = 0;
                life--;
                if (triggerInvuln)
                {
                    StartCoroutine(TriggerTemporaryInvuln());
                }
            }
            else
            {
                health = dummyHealth;
                if (triggerInvuln)
                {
                    StartCoroutine(TriggerTemporaryInvuln());
                }
            }
        }
        
    }

    IEnumerator TriggerTemporaryInvuln()
    {
        invuln = true;
        yield return new WaitForSeconds(invulnTimer);
    }

    internal void UpdateHealth(float percentage)
    {
        healthMult *= percentage;
        currentHealthMax *= healthMult;
        if (health > currentHealthMax)
        {
            health = currentHealthMax;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Entity bruh;
        if (collision.gameObject.TryGetComponent<Entity>(out bruh) && gameObject.GetComponent<Entity>().isFriendly != bruh.isFriendly)
        {
            Damage(bruh.GetDamage(), isFriendly);
        }
        //Debug.Log(collision.gameObject.name);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Entity bruh;
        if (collision.gameObject.TryGetComponent<Entity>(out bruh) && gameObject.GetComponent<Entity>().isFriendly != bruh.isFriendly)
        {
            Damage(bruh.GetDamage(), isFriendly);
        }
        Debug.Log(collision.gameObject.name);
    }
}
