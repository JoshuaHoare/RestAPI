using OpenAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterCreator;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    [SerializeField] int len;
    public Dictionary<string, GPTCharacter> characters = new Dictionary<string, GPTCharacter>();

    [Header("Character Components")]
    [SerializeField] Transform content;
    [SerializeField] GameObject tempalateCharacter;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// We want to create a character if no character already exists 
    /// given the provided name.
    /// </summary>
    /// <returns>whether or not a valid character has been created</returns>
    public bool CreateCharacter(CharacterDetails character)
    {
        if (characters.ContainsKey(character.name))
            return false;
        else
        {
            GPTCharacter newCharacter = new GPTCharacter(character.name, character.race, character.voc, character.description);
            GameObject listItem = Instantiate(tempalateCharacter, content);
            listItem.GetComponent<GPTCharacter>().Init(newCharacter);

            characters.Add(character.name, newCharacter);
            len = characters.Count;
            return true;
        }
    }


}
