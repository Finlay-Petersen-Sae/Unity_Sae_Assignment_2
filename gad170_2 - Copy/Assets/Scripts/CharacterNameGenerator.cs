using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data object that can generate a number of dancer names
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
 
        CharacterName emptyName = new CharacterName(string.Empty, string.Empty, string.Empty, string.Empty);
        for (int i = 0; i < names.Length; i++)
        {
            
            //find random first name
            int randomFirstNameIndex = Random.Range(0, firstNames.Count);
            //find random last name
            int randomLastNameIndex = Random.Range(0, lastNames.Count);
            //find random nickname
            int randomNicknameIndex = Random.Range(0, nicknames.Count);
            //find random decriptor
            int randomDescriptorsIndex = Random.Range(0, descriptors.Count);
            //set names
            names[i] = new CharacterName(firstNames[randomFirstNameIndex], lastNames[randomLastNameIndex], nicknames[randomNicknameIndex], descriptors[randomDescriptorsIndex]);
        }

        Debug.LogWarning("CharacterNameGenerator called, it needs to fill out the names array with unique randomly constructed character names");

        return names;
    }
}