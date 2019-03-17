using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data object that can generate a number of dancer names
/// 
/// TODO:
///     Needs to generate the number of requested <b>unique</b> character names for the dancers in our battle
/// </summary>
[CreateAssetMenu(menuName = "Battle Objects/Character Name Generator")]
[System.Serializable]
public class CharacterNameGenerator : ScriptableObject
{
    [Header("Possible first names")]
    [SerializeField]
    public List<string> firstNames;
    [Header("Possible last names")]
    [SerializeField]
    public List<string> lastNames;
    [Header("Possible nicknames")]
    [SerializeField]
    public List<string> nicknames;
    [Header("Possible adjectives to describe the character")]
    [SerializeField]
    public List<string> descriptors;

    public CharacterName[] GenerateNames(int namesNeeded)
    {
        CharacterName[] names = new CharacterName[namesNeeded];

        // access the first firstName
        Debug.Log(firstNames[0]);

        //TODO - filling this with empty names so the rest of our code is safe to run without need for many null checks
        CharacterName emptyName = new CharacterName(string.Empty, string.Empty, string.Empty, string.Empty);
        for (int i = 0; i < names.Length; i++)
        {
            //TODO for week 7 - do this 4th
            //- make use of the firstNames, lastNames, nicknames and Descriptors list
            names[i] = new CharacterName("first", "last", "nickname", "descriptor");
        }

        Debug.LogWarning("CharacterNameGenerator called, it needs to fill out the names array with unique randomly constructed character names");

        return names;
    }
}