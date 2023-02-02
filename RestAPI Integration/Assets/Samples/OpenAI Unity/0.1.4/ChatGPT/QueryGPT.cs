using UnityEngine;
using OpenAI;

public class QueryGPT : MonoBehaviour
{
    public static QueryGPT instance;

    private OpenAIApi openai = new OpenAIApi();

    void Awake()
    {
        instance = this;
    }

    public void Prompt(QueryData settings)
    {
        Query(settings);
    }

    private async void Query(QueryData prompt)
    {
        // Complete the instruction
        var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
        {
            Prompt = prompt.history,
            Model = "text-davinci-003",
            MaxTokens = 128
        });

        prompt.Respond(completionResponse.Choices[0].Text);
    }
}

public class QueryData
{
    [Header("Prompt Contents")]
    public string prompt;
    public string history;
    public string response;

    [Header("Prompt Settings")]
    public bool print;
    public GPTEntry entry;
    

    private System.Action<QueryData> callback;

    public QueryData(string _prompt, string _history, System.Action<QueryData> _callback, bool _print = true)
    {
        prompt = _prompt;
        history = _history + "\n Q:" + _prompt + "\n A:";
        callback = _callback;
        print = _print;
    }

    public void Respond(string _response)
    {
        response = _response;
        callback.Invoke(this);
    }
}
