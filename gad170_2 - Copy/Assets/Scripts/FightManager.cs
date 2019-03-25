using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the outcome of a dance off between 2 dancers, determines the strength of the victory form -1 to 1
/// </summary>
public class FightManager : MonoBehaviour
{
    public Color drawCol = Color.gray;

    public float fightAnimTime = 2;

    private void OnEnable()
    {
        GameEvents.OnFightRequested += Fight;
    }

    private void OnDisable()
    {
        GameEvents.OnFightRequested -= Fight;
    }

    public void Fight(FightEventData data)
    {
        StartCoroutine(Attack(data.lhs, data.rhs));
    }

    IEnumerator Attack(Character lhs, Character rhs)
    {
        lhs.isSelected = true;
        rhs.isSelected = true;
        lhs.GetComponent<AnimationController>().Dance();
        rhs.GetComponent<AnimationController>().Dance();

        yield return new WaitForSeconds(fightAnimTime);
       
        float outcome = 0;
        //set outcome to 0 which mean no outcome
        Character winner = lhs, defeated = rhs;
        // setting a winner variable and a loser variables
    
       lhs.luck = Random.Range(1, 7);
        // set lhs luck for left hand side
        rhs.luck = Random.Range(1, 7);
        // set lhs luck for left hand side
        float rhsBoogie = rhs.rhythm + rhs.style * 2 + rhs.luck * 2;
        // calculate rhsBoogie through rhythm + style *2 + luck * 2
        float lhsBoogie = lhs.rhythm + lhs.style * 2 + lhs.luck * 2;
        // calculate rhsBoogie through rhythm + style *2 + luck * 2
        // your code between here
        rhs.luck = Random.Range(1, 7);
        //Randomise luck for rhs
       lhs.luck = Random.Range(1, 7);
        //randomise luck for lhs


        //debug the left hand side character stats
        Debug.Log("lhs rhythm = " + lhs.rhythm);
        Debug.Log("lhs style = " +  lhs.style);
        Debug.Log("lhs luck = " +  lhs.luck);
        //debug the right hand side character stats
        Debug.Log("rhs rhythm = " +  rhs.rhythm);
        Debug.Log("rhs style = " +  rhs.style);
        Debug.Log("rhs luck = " +  rhs.luck);
  

       

        if (rhsBoogie <= lhsBoogie)
        {
            outcome = 1;
            //check if lhs boogie is higher than right side boogie
            // set outcome to 1
        }
        else if (rhsBoogie >= lhsBoogie)
        {
            outcome = 2;
            //check if rhs boogie is higher than left side boogie
            // set outcome to 2
        }

        if (outcome == 1)
        {
            winner = lhs;
            defeated = rhs;
            // if outcome is 1 set winner to lhs, and set rhs to loser
        }
        else if (outcome == 2)
        {
            winner = rhs;
            defeated = lhs;
            // if outcome is 2 set winner to rhs, and set lhs to loser
        }

        Debug.LogWarning("Attack called, needs to use character stats to determine winner with win strength from 1 to -1. This can most likely be ported from previous brief work.");


        Debug.LogWarning("Attack called, may want to use the BattleLog to report the dancers and the outcome of their dance off.");

        var results = new FightResultData(winner, defeated, outcome);

        lhs.isSelected = false;
        rhs.isSelected = false;
        GameEvents.FightCompleted(results);

        yield return null;
    }
}
