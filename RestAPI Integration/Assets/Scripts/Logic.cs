using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public static Logic instance;

    float longitude;
    float latitude;

    public List<System.Action<float, float>> listeners = new List<System.Action<float, float>>();

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    public void UpdateLocation(float _longitude, float _latitude)
    {
        longitude = _longitude;
        latitude = _latitude;

        if (listeners.Count > 0)
        {
            foreach (System.Action<float, float> listner in listeners)
            {
                listner.Invoke(longitude, latitude);
            }
        }
    }
}
