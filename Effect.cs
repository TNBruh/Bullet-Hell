using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    //poison and shield's potency function//
    //(x-1)*(x^4+x+1)*log(x)/(x^4+x^2)//
    //weak and wither's potency function//
    //log(x^0.5)+x^0.1-1//
    
    //victim//
    internal GameObject target;

    //target's current health//
    internal double maxHealth;

    //target's current dmg//
    internal float maxDmg;

    //calculated stat reduction or boost//
    internal float dmg;

    //effect's id//
    internal int ID;

    //trigger repetitively//
    internal bool continuous = true;

    internal Coroutine execution;

    internal float timer = -69;
    internal int potency;
    internal string[] effectList = { "poison", "wither", "invuln", "shield", "vuln", "weak", "confuse" };

    
    internal void CreateEffect(GameObject inpTarget, int effectID, float inpDmg, float inpTimer, int inpPotency = 1)
    {

        GameObject objectForm = Instantiate(new GameObject(), target.transform);
        objectForm.tag = "Effect";
        Effect effect = objectForm.AddComponent<Effect>() as Effect;

        effect.target = inpTarget;
        effect.ID = effectID;
        effect.dmg = inpDmg;
        effect.timer = inpTimer;
        effect.potency = inpPotency;
        if (inpPotency < 1) 
        {
            effect.potency = 1;
        }

        if (string.Equals(effectList[effectID], "poison"))
        {
            effect.dmg = effect.CalcPoison();
        }
        else if (string.Equals(effectList[effectID], "wither"))
        {
            effect.dmg = effect.CalcWither();
            effect.continuous = false;
        }
        else if (string.Equals(effectList[effectID], "invuln"))
        {
            effect.continuous = false;
        }
        else if (string.Equals(effectList[effectID], "shield"))
        {
            effect.dmg = effect.CalcPoison();
            effect.continuous = false;
        }
        else if (string.Equals(effectList[effectID], "vuln"))
        {
            effect.dmg = effect.CalcWither();
            effect.continuous = false;
        }
        else if (string.Equals(effectList[effectID], "weak"))
        {
            effect.dmg = effect.CalcWither();
            effect.continuous = false;
        }
        else if (string.Equals(effectList[effectID], "confuse"))
        {

        }
    }

    ~Effect()
    {
        Debug.Log(effectList[ID] + "on" + target.name + " is destroyed");
    }

    //poison and shield's potency function//
    //(x-1)*(x^4+x+1)*log(x)/(x^4+x^2)//
    float CalcPoison()
    {
        return dmg+dmg*(((potency - 1) * (Mathf.Pow(potency, 4) + potency + 1) * Mathf.Log(potency)) / (Mathf.Pow(potency, 4) + Mathf.Pow(potency, 2)));
    }

    //weak, vuln, wither's potency function//
    //log(x^0.5)+x^0.1-1+1//
    //new function//
    //log(x^0.5)+0.1^x//
    float CalcWither()
    {
        //everything that is calculated here is immediately multiplied to the target's stat//

        float multiplier = (Mathf.Log(Mathf.Pow(potency, 0.5f)) + Mathf.Pow(0.1f, potency));
        if ((ID == 1) || (ID == 4) || (ID == 5))
        {
            return multiplier;
        }
        else
        {
            return 1;
        }
    }

    Vector3 CalcConfuse()
    {
        return new Vector3(Random.Range(-(float)potency, (float)potency), Random.Range(-(float)potency, (float)potency), 0);
    }

    private void Start()
    {
        if (timer != -69 && continuous)
        {
            if (ID == 0)
            {
                StartCoroutine(TriggerContinuous());
            }
            else
            {
                StartCoroutine(TriggerOnce());
            }
        }
    }

    IEnumerator TriggerContinuous()
    {
        for (float i = timer; i > 0; i -= Time.deltaTime)
        {
            if (ID == 0)
            {
                target.GetComponent<Character>().Damage(dmg, false);
            }
            yield return new WaitForFixedUpdate();
        }
        Normalize();
    }

    internal void Normalize()
    {
        Character targetScript = target.GetComponent<Character>();
        if (ID == 1)
        {
            targetScript.UpdateHealth(1 / dmg);
        }
        else if (ID == 2)
        {
            targetScript.invuln = false;
        }
        else if (ID == 4)
        {
            targetScript.vuln /= dmg;
        }
        else if (ID == 5)
        {
            targetScript.MultiplyAtk(1 / dmg, 0);
        }
        Destroy(gameObject);
    }

    IEnumerator TriggerOnce()
    {
        Character targetScript = target.GetComponent<Character>();
        if (ID == 1)
        {
            targetScript.UpdateHealth(dmg);
        }
        else if (ID == 2)
        {
            targetScript.invuln = true;
        }
        else if (ID == 4)
        {
            targetScript.vuln *= dmg;
        }
        else if (ID == 5)
        {
            targetScript.MultiplyAtk(dmg, 0);
        }

        for (float i = timer; i > 0; i -= Time.deltaTime)
        {
            yield return new WaitForFixedUpdate();
        }

        Normalize();
    }
}
