using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebScrape : MonoBehaviour
{
    [SerializeField] bool request;
    [SerializeField] string page;

    private void Update()
    {
        if (request)
        {
            request = false;
            Request(page);
        }
    }

    public void Request(string url)
    {
        StartCoroutine(Get(url));
    }

    IEnumerator Get(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            
            Debug.Log(ValidResponse(request) ? ("Error: " + request.error) : ("Recieved: " + request.downloadHandler.text));

            string output = request.downloadHandler.text;
            int startingIndex = output.IndexOf("<span class=\"package-price\"");

            string startPoint = output.Substring(startingIndex);
            int spanLength = startPoint.LastIndexOf("</span>");

            Debug.Log(startPoint);
        }      
        
    }

    bool ValidResponse(UnityWebRequest response)
    {
        return (response.isNetworkError || response.isHttpError);
    }

    string CostPer(string input)
    {

        return "f";
    }
}
