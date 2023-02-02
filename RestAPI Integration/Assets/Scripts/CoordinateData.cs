using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateData : MonoBehaviour
{
    public static CoordinateData instance;
    //Inspector Fields
    [Header("Required Components")][Space(5)]
    [Header("Geographic Location Components")][Space(2)]
    [SerializeField] Transform geographicPivot;
    [SerializeField] Transform geographicSelection;

    [Header("Space Station Components")]
    [Space(2)]
    [SerializeField] Transform spaceStationPivot;
    [SerializeField] Transform spaceStation;

    //Private Fields
    private float radius;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        radius = Vector3.Distance(transform.position, geographicSelection.position);

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
            UpdateGeographicSelection(hit.point);
        }
    }

    void UpdateGeographicSelection(Vector3 position)
    {
        geographicPivot.localRotation = Quaternion.LookRotation((transform.InverseTransformPoint(position) - transform.position), transform.up);

        Vector2 coordinates = Coordinates(geographicSelection.position);
        Logic.instance.UpdateGeographicLocation(coordinates.x, coordinates.y);
    }

    public void UpdateSpaceStation(Vector2 coordinates)
    {
        spaceStationPivot.rotation = Rotation(coordinates);
    }

    Vector2 GetCoordinates(Vector3 position)
    {
        Vector3 planetUp = transform.up;
        Vector3 planetPosition = transform.position;
        Vector3 planetRight = transform.right;

        //Latitude
        float dotProductY = Vector3.Dot((position - planetPosition).normalized, planetUp);
        float latitude = Mathf.Lerp(90, -90, Mathf.InverseLerp(1, -1, dotProductY));

        //Longitude
        Vector3 projectedPosition = Vector3.Lerp(planetPosition + (-planetUp * radius), planetPosition + (planetUp * radius), Mathf.InverseLerp(-1, 1, dotProductY));
        float longitude = Vector3.SignedAngle((position - projectedPosition), planetRight, planetUp);

        return new Vector2(longitude, latitude);
    }

    Vector2 Coordinates(Vector3 position)
    {
        Vector3 planetPosition = transform.position;
       // Vector3 ProjectedLong = Vector3.ProjectOnPlane();
        Vector3 projectedVector = Vector3.ProjectOnPlane(position, transform.up);
        float longitude = 0;
        float latitude = 0;
        //Vector3.SignedAngle(projectedVector, transform.up, transform.right);
        //Vector3.SignedAngle(position - planetPosition, transform.up, transform.right);

        return new Vector2(longitude, latitude);
    }

    Quaternion Rotation(Vector2 coordinates)
    {
        //Quaternion yRotation = Quaternion.AngleAxis(coordinates.y, transform.forward);
        //Quaternion yRotation = Quaternion.AngleAxis(coordinates.x, -transform.up);

        Quaternion yRotation = Quaternion.AngleAxis(coordinates.x, -transform.up) * Quaternion.AngleAxis(coordinates.y, transform.forward);

        //Vector3 output = Quaternion;
        return yRotation * transform.rotation;
    }

}
