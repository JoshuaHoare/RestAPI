using OpenAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class GPTCharacter : MonoBehaviour
{
    public List<GPTEntry> entries = new List<GPTEntry>();
    [SerializeField] TextAsset jsonExample;
    [SerializeField] CData characterData;

    [Header("Character Details")]
    public string characterName;
    [SerializeField] string race;
    [SerializeField] string voc;
    [SerializeField] string desc;

    [Header("Data Settings")]
    [SerializeField] Transform dataContainer;
    [SerializeField] Transform contentContainer;
    [SerializeField] List<string> commands = new List<string>();
    private string _history;
    public bool active;

    string NewCharacter()
    {
        return "[INTRO] You are a character in a game of dungeons and dragons, you act in the first person and your name is as follows exactly: \"" + characterName + "\". Introduce yourself in 15 words or less." + "\n A:";
    }

    public GPTCharacter(string name, string race, string voc, string desc)
    {
        this.characterName = name;
        this.race = race;
        this.voc = voc;
        this.desc = desc;
    }

    public void Init(GPTCharacter data)
    {
        characterData = JsonUtility.FromJson<CData>(jsonExample.ToString());
        
        this.characterName = data.characterName;
        this.race = data.race;
        this.voc = data.voc;
        this.desc = data.desc;
        this._history = data._history;

        ChatGPT.instance.SetCharacter(this);
        Debug.Log(characterName);

        Prompt(NewCharacter());

        //Debug.Log(JsonUtility.FromJson<CData>(jsonExample.ToString()).name);
    }

    public void OnClick_Load()
    {
        ChatGPT.instance.SetCharacter(this);
    }

    public void Load()
    {
        active = true;

        if (!contentContainer)
            contentContainer = ChatGPT.instance.contentContainer;

        foreach(GPTEntry entry in entries) 
        {
            entry.transform.SetParent(contentContainer);
            entry.gameObject.SetActive(true);
        }
    }

    public void Unload()
    {
        active = false;

        foreach (GPTEntry entry in entries)
        {
            entry.transform.SetParent(dataContainer);
            entry.gameObject.SetActive(false);
        }
    }

    public void Prompt(string _prompt)
    {
        if (!ContainsCommand(_prompt))
        {
            GPTEntry playerEntry = ChatGPT.instance.CreateEntry("<b>Player:</b> " + _prompt, contentContainer);
            entries.Add(playerEntry);
        }

        QueryData newQuery = new QueryData(_prompt, _history, ProcessPrompt, !ContainsCommand(_prompt) || _prompt.Contains("[INTRO]"));
        QueryGPT.instance.Prompt(newQuery);
    }

    public void ProcessPrompt(QueryData response)
    {
        Debug.Log(response.print);

        if (response.print)
        {
            GPTEntry entry = ChatGPT.instance.CreateEntry("<b>" + characterName + ":</b> " + response.response.Trim(), contentContainer);
            Debug.Log(response.response);
            entry.gameObject.SetActive(active);
            entries.Add(entry);
        }

        _history = response.history;
    }

    bool ContainsCommand(string _prompt)
    {
        string segment = _prompt.Length > 12 ? _prompt.Substring(0, 12) : _prompt;

        if (segment != "INTRO")
        {
            foreach (string command in commands)
            {
                if (segment.Contains(command))
                {
                    return true;
                }
            }
        }
        
        return false;
    }


}


public enum CharacterCreateField
{
    name,
    race,
    voc,
    description
}
