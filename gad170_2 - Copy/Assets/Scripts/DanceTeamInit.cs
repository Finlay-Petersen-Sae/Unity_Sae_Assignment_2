using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This class generates and assigns names to the 2 dance teams in our dance off battle.
/// It also controls the number of dancers on each team via the inspector
/// It also uses the name generator to pass character names to the teams so they can initialise
/// </summary>
public class DanceTeamInit : MonoBehaviour
{
    public DanceTeam teamA, teamB;

    public GameObject dancerPrefab;
    public int dancersPerSide = 3;
    public CharacterNameGenerator nameGenerator;

    private void OnEnable()
    {
        GameEvents.OnBattleInitialise += InitTeams;
    }
    private void OnDisable()
    {
        GameEvents.OnBattleInitialise -= InitTeams;
    }

    void InitTeams()
    {
        Debug.LogWarning("InitTeams called, needs to generate names for the teams and set them with teamA.SetTroupeName");

       
        teamA.SetTroupeName("Dance Patrol"); 
        // set team A troupe name 
        teamB.SetTroupeName("Boogie Fever");
        // set team B troupe name

        
        nameGenerator.GenerateNames(6);
        // use the name generator to generate 6 names
        CharacterName[] teamANames = nameGenerator.GenerateNames(3);
        // take 3 names from the list to use for Team A
        CharacterName[] teamBNames = nameGenerator.GenerateNames(3);
      
      
        teamA.InitaliseTeamFromNames(dancerPrefab, 1, teamANames);
        // initialize team A names
        teamB.InitaliseTeamFromNames(dancerPrefab, 1, teamBNames);
        // initialize team B names
        Debug.LogWarning("InitTeams called, needs to create character names via CharacterNameGenerator and get them into the team.InitaliseTeamFromNames");
    }
}
