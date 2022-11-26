using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    Vector3 prevPos = Vector3.zero;
    Vector3 delta = Vector3.zero;
    Vector3 mousePosition = Vector3.zero;

    [SerializeField]Transform planet;
    [SerializeField] float damp;
    private void Awake()
    {
        StartCoroutine(EaseIn());
    }
    private void Update()
    {
        mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            delta = (mousePosition - prevPos) * damp;

            transform.Rotate(transform.up, (Vector3.Dot(transform.up, Vector3.up) >= 0 ? -1 : 1) * Vector3.Dot(delta, Camera.main.transform.right), Space.World);
            transform.Rotate(Camera.main.transform.right, Vector3.Dot(delta, Camera.main.transform.up), Space.World);
        }

        prevPos = mousePosition;
    }

    IEnumerator EaseIn()
    {
        while (isActiveAndEnabled)
        {
            planet.rotation = Quaternion.Lerp(planet.transform.rotation, transform.rotation, Time.deltaTime);
            yield return null;
        }
    }

}
