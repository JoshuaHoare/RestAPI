using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CharacterCreator;

public class StandardInputField : MonoBehaviour
{
    public TMP_InputField data;
    public CharacterCreateField type;
    string defaultString;

    private void Start()
    {
        data = gameObject.GetComponent<TMP_InputField>();

        defaultString = data.text;
    }

    public void Reset()
    {
        data.text = defaultString;
        Debug.Log(defaultString);
    }

    public StringFieldResult GetData()
    {
        return new StringFieldResult(data.text, type, data.text != defaultString);
    }
}

public struct StringFieldResult
{
    public string value;
    public CharacterCreateField type;
    public bool changed;

    public StringFieldResult(string value, CharacterCreateField type, bool changed)
    {
        this.value = value;
        this.changed = changed;
        this.type = type;
    }
}
