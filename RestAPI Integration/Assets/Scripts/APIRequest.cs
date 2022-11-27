using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIRequest : MonoBehaviour
{
    public static APIRequest instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    public void APICall(System.Action<Response, bool> callback)
    {
        StartCoroutine(ProcessRequest(callback));
    }

    private IEnumerator ProcessRequest(System.Action<Response, bool> callback)
    {
        string url = "http://api.open-notify.org/iss-now.json";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
                callback.Invoke(new Response(), false);
            else
                callback.Invoke(JsonUtility.FromJson<Response>(request.downloadHandler.text), true);

        }
    }
}

[Serializable]
public class Response
{
    public int timestamp;
    public IssPosition iss_position;
    public string message;

    [Serializable]
    public class IssPosition
    {
        public string longitude;
        public string latitude;
    }
}

[System.Serializable]
public class Data
{
    public bool realtime;
    public float longitude;
    public float latitude;

    public void Update(float _longitude, float _latitude)
    {
        realtime = true;

        longitude = _longitude;
        latitude = _latitude;
    }
}



