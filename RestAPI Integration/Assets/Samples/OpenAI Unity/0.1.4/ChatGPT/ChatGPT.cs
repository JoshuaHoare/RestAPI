using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System.Linq;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        public static ChatGPT instance;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI textArea;

        [SerializeField] List<GPTEntry> entries = new List<GPTEntry>();
        [SerializeField] List<GPTCharacter> characters = new List<GPTCharacter>();
        
        [SerializeField] GameObject templateEntry;
        public Transform contentContainer;
        [SerializeField] TextMeshProUGUI characterName;
        [SerializeField] GameObject blocker;

        private OpenAIApi openai = new OpenAIApi();


        private GPTCharacter character;

        private string userInput;

        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }

        public void SetCharacter(GPTCharacter newCharacter)
        {
            if (blocker.activeSelf)
                blocker.SetActive(false);

            if (!characters.Contains(newCharacter))
                characters.Add(newCharacter);

            foreach (GPTCharacter chara in characters)
            {
                if (chara == newCharacter)
                    chara.Load();
                else
                    chara.Unload();
            }

            //load new char data
            character = newCharacter;
            characterName.text = newCharacter.characterName;
        }

        private async void SendReply()
        {
            userInput = inputField.text;

            character.Prompt(userInput);
        }
        
        public GPTEntry CreateEntry(string response, Transform contentContainer)
        {
            Debug.Log("Creating entry");
            GameObject item = Instantiate(templateEntry, contentContainer).gameObject;
            GPTEntry entry = item.GetComponent<GPTEntry>();
            entry.SetResponse(response);

            return entry;
        }
    }
}