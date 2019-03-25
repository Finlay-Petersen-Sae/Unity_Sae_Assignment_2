using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The BattlesSystem handles the organisation of rounds, selecting the dancers to dance off from each side.
/// It then hands off to the fightManager to determine the outcome of 2 dancers dance off'ing.
/// </summary>
public class BattleSystem : MonoBehaviour
{
    public DanceTeam TeamA,TeamB;
    public Character teamAfighter;
    //make a character variable teamAfighter
    public Character teamBfighter;
    //make a character variable teamBfighter

    public float battlePrepTime = 2;
    public float fightWinTime = 2;

    private void OnEnable()
    {
        GameEvents.OnRequestFighters += RoundRequested;
        GameEvents.OnFightComplete += FightOver;
    }

    private void OnDisable()
    {
        GameEvents.OnRequestFighters -= RoundRequested;
        GameEvents.OnFightComplete -= FightOver;
    }

    void RoundRequested()
    {
        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(DoRound());
    }

    IEnumerator DoRound()
    {
        yield return new WaitForSeconds(battlePrepTime);

        if (TeamA.activeDancers.Count > 0 && TeamB.activeDancers.Count > 0)
        {
            Debug.LogWarning("DoRound called, it needs to select a dancer from each team to dance off and put in the FightEventData below");
            int teamArandomFighter = Random.Range(0, TeamA.activeDancers.Count);
            // make an interger that selects a random dancer in the team A list
            teamAfighter = TeamA.activeDancers[teamArandomFighter];
            //use that random dancer prefab and store it in the teamAfighter variable
            int teamBrandomFighter = Random.Range(0, TeamB.activeDancers.Count);
            // make an interger that selects a random dancer in the team B list
            teamBfighter = TeamB.activeDancers[teamBrandomFighter];
            //use that random dancer prefab and store it in the teamBfighter variable
            GameEvents.RequestFight(new FightEventData(teamAfighter, teamBfighter));
        }
        else
        {
           if(TeamA.activeDancers.Count >= 0)
            {
                GameEvents.BattleFinished(TeamA);
                TeamA.EnableWinEffects();
                Debug.Log("DoRound called, but we have a winner so Game Over");
                // if team A has more than 0 active dancers and team B has 0 dancers
                // enable win effects on team A
            }
           else if(TeamB.activeDancers.Count >= 0)
            {
                GameEvents.BattleFinished(TeamB);
                TeamB.EnableWinEffects();
                Debug.Log("DoRound called, but we have a winner so Game Over");
                // if team B has more than 0 active dancers and team A has 0 dancers
                // enable win effects on team B
            }
            else
            {
                Debug.Log("We Have a Tie");
                // if both have more than 0 dancers make it a tie
                //fail safe code
            }          
        }
    }

    void FightOver(FightResultData data)
    {
        Debug.LogWarning("FightOver called, may need to check for winners and/or notify teams of zero mojo dancers");

        Debug.Log(data.outcome);
        if(data.outcome >= 0)
        {
            data.winner.myTeam.EnableWinEffects();
            //play win effects on the winning team
            data.defeated.myTeam.RemoveFromActive(data.defeated);
            //play lose effects on the losing team
        }
        //defaulting to starting a new round to ease development
        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(HandleFightOver());
    }

    IEnumerator HandleFightOver()
    {
        yield return new WaitForSeconds(fightWinTime);
        TeamA.DisableWinEffects();
        TeamB.DisableWinEffects();
        Debug.LogWarning("HandleFightOver called, may need to prepare or clean dancers or teams and checks before doing GameEvents.RequestFighters()");
        GameEvents.RequestFighters();
    }
}
