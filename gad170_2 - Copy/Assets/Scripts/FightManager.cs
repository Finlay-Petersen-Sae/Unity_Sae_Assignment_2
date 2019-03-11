using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the outcome of a dance off between 2 dancers, determines the strength of the victory form -1 to 1
/// 
/// TODO:
///     Handle GameEvents.OnFightRequested, resolve based on stats and respond with GameEvents.FightCompleted
///         This will require a winner and defeated in the fight to be determined.
///         This may be where characters are set as selected when they are in a dance off and when they leave the dance off
///         This may also be where you use the BattleLog to output the status of fights
///     This may also be where characters suffer mojo (hp) loss when they are defeated
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
        //DO this second
        //can reuse code from battle in battle handler
        float outcome = 0;
       lhs.luck = Random.Range(1, 7);
        rhs.luck = Random.Range(1, 7);
        float rhsBoogie = rhs.rhythm + rhs.style * 2 + rhs.luck * 2;
        float lhsBoogie = lhs.rhythm + lhs.style * 2 + lhs.luck * 2;
        // your code between here
       rhs.luck = Random.Range(1, 7);
       lhs.luck = Random.Range(1, 7);


        //debug the left hand side character stats
        Debug.Log("lhs rhythm = " + lhs.rhythm);
        Debug.Log("lhs style = " +  lhs.style);
        Debug.Log("lhs luck = " +  lhs.luck);

        Debug.Log("rhs rhythm = " +  rhs.rhythm);
        Debug.Log("rhs style = " +  rhs.style);
        Debug.Log("rhs luck = " +  rhs.luck);
        //defaulting to draw 

        // somewhere here you are setting otcome
        //todo: when you set outcome 
        // also set swinner and defeated
        //rough example
        // outcome = 1: // for my version 1 means LHS won
        //winner = lhs;
        //defeated = rhs;
        outcome = 1;
        Character winner = lhs, defeated = rhs;
        Debug.LogWarning("Attack called, needs to use character stats to determine winner with win strength from 1 to -1. This can most likely be ported from previous brief work.");


        Debug.LogWarning("Attack called, may want to use the BattleLog to report the dancers and the outcome of their dance off.");

        var results = new FightResultData(winner, defeated, outcome);

        lhs.isSelected = false;
        rhs.isSelected = false;
        GameEvents.FightCompleted(results);

        yield return null;
    }
}
