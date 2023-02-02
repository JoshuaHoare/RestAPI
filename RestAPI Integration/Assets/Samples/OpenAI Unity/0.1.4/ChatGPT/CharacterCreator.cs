using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CharacterCreator : MonoBehaviour
{
    [SerializeField] List<StandardInputField> inputFields = new List<StandardInputField>();
    
    void Awake()
    {
        inputFields = gameObject.GetComponentsInChildren<StandardInputField>().ToList();   
    }

    public void OnClick_Create()
    {
        CharacterDetails tmpCharacter = new CharacterDetails();

        bool valid = true;
        foreach (StandardInputField field in inputFields)
        {
            StringFieldResult fields = field.GetData();
            tmpCharacter.Fill(fields.value, fields.type);

            if (valid)
                valid = field.GetData().changed;
        }

        if (valid)
            gameObject.SetActive(!CharacterManager.instance.CreateCharacter(tmpCharacter));
    }

    public void OnClick_Open()
    {
        foreach (StandardInputField field in inputFields)
        {
            field.Reset();
        }

        gameObject.SetActive(true);
    }

    public void OnClick_Cancel()
    {
        gameObject.SetActive(false);
    }
}

public class CharacterDetails
{
    public string name;
    public string race;
    public string voc;
    public string description;

    public void Fill(string data, CharacterCreateField type)
    {
        switch (type)
        {
            case CharacterCreateField.name:
                name = data;
                break;

            case CharacterCreateField.race:
                race = data;
                break;

            case CharacterCreateField.voc:
                voc = data;
                break;

            case CharacterCreateField.description:
                description = data;
                break;
        }
    }
}
