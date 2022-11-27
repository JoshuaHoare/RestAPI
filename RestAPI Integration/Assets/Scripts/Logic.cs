using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public static Logic instance;

    [Header("Selected Geographic Location")]
    [SerializeField] float geographicLongitude;
    [SerializeField] float geographicLatitude;

    [Header("International Space Station")] [Space(5)]
    [SerializeField] float stationLongitude;
    [SerializeField] float stationLatitude;

    public List<System.Action<float, float>> listeners = new List<System.Action<float, float>>();

    private void Awake()
    {
        if (!instance)
            instance = this;

        StartCoroutine(TrackSpaceStation());
    }

    #region Geographic Location Tracking

    public void UpdateGeographicLocation(float _longitude, float _latitude)
    {
        geographicLongitude = _longitude;
        geographicLatitude = _latitude;

        if (listeners.Count > 0)
        {
            foreach (System.Action<float, float> listner in listeners)
            {
                listner.Invoke(_longitude, _latitude);
            }
        }
    }

    #endregion

    #region Space Station Tracking

    IEnumerator TrackSpaceStation()
    {
        yield return new WaitForEndOfFrame();

        while (true)
        {
            APIRequest.instance.APICall(ApiResponse);
            yield return new WaitForSeconds(5);
        }
    }

    void ApiResponse(Response result, bool success)
    {
        if (success)
        {
            stationLongitude = float.Parse(result.iss_position.longitude);
            stationLatitude = float.Parse(result.iss_position.latitude);
        }
        else
            Debug.LogWarning("Failed to recieve a valid api response");
    }

    #endregion
}
