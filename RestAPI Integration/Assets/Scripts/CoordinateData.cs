using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateData : MonoBehaviour
{
    //Inspector Fields
    [Header("Coordinates")]
    [SerializeField] float latitude;
    [SerializeField] float longitude;

    [Header("Required Components")][Space(5)]
    [SerializeField] Transform selection;
    [SerializeField] Transform selectionPoint;

    //Private Fields
    private float radius;

    void Start()
    {
        radius = Vector3.Distance(transform.position, selectionPoint.position);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Click_Request();

    }

    void Click_Request()
    {        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            selection.localRotation = Quaternion.LookRotation((transform.InverseTransformPoint(hit.point) - transform.position), transform.up);

            //Latitude
            float dotProductY = Vector3.Dot((selectionPoint.position - transform.position).normalized, transform.up);
            latitude = Mathf.Lerp(90, -90, Mathf.InverseLerp(-1, 1, dotProductY));

            //Longitude
            Vector3 projectedPosition = Vector3.Lerp(transform.position + (-transform.up * radius), transform.position + (transform.up * radius), Mathf.InverseLerp(-1, 1, dotProductY));
            longitude = Vector3.SignedAngle((selectionPoint.position - projectedPosition), transform.right, transform.up);

            Logic.instance.UpdateLocation(longitude, latitude);
        }
    }

}
