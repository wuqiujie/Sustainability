using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 zoomAmount;
    public float zoomTime;

    public float movementSpeed;
    public float movementTime;
    public Vector3 newPos;
    public Vector3 newZoom;
    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position;
        newZoom = cameraTransform.localPosition + new Vector3(0f, 200f, -200f);
        zoomAmount = new Vector3(0f,5f,-5f);
        movementSpeed = 0.5f;
        zoomTime = 2f;
        movementTime = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
    }

    public void HandleMovementInput()
    {
        //translate
        newPos += transform.forward * movementSpeed * Input.GetAxis("Vertical");
        newPos += transform.right * movementSpeed * Input.GetAxis("Horizontal");
        //zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && cameraTransform.localPosition.y < 200)
        {
            newZoom += zoomAmount;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cameraTransform.localPosition.y > 10)
        {
            newZoom -= zoomAmount;
        }
        //new
        newZoom.y = Mathf.Clamp(newZoom.y, 10, 200);
        newZoom.z = Mathf.Clamp(newZoom.z, -200, -10);
        //
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * zoomTime);
    }
    //new 
    public void LookAtPos()
    {
        newPos = CityManager.buildingPos;
        newZoom = new Vector3(0, 30f, -30f);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * zoomTime);
    }
    public void ResetPos()
    {
        newPos = new Vector3(50f, 0f, 50f);
        newZoom = new Vector3(0, 100f, -100f);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * zoomTime);
    }
}
