using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNavController : MonoBehaviour
{
    public Vector3 _destination;
    public bool _reachedDestination;
    public float _stopDistance;
    public float _rotationSpeed;
    public float _maxSpeed;
    public float _movementSpeed;
    private float _deacceleration;
    public Vector3 _velocity;
    public Vector3 _lastPosition;

    public bool isbreak;
    [Header("Sensors")]
    public float sensorLength = 2f;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    private float heuristic;
   
    // Start is called before the first frame update
    void Start()
    {
        _reachedDestination = false;
        _movementSpeed = _maxSpeed;
        _rotationSpeed = 300f;
        _stopDistance = 0.2f;
/*        _acceleration = 3;*/
    }

    // Update is called once per frame
    void Update()
    {
        _deacceleration = _maxSpeed * _maxSpeed / (sensorLength * 2);
        Sensors();
        /*Debug.DrawLine(transform.position, transform.position + (transform.forward + new Vector3(0.1f,0,0)), Color.white);*/
        _lastPosition = transform.position;
        if (transform.position != _destination)
        {
            Vector3 destinationDirection = _destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= _stopDistance)
            {
                _reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                if(isbreak)
                {
                    if (_movementSpeed > 0)
                    {
                        _movementSpeed -= 7* heuristic *_deacceleration * Time.deltaTime;
                    }
                    transform.Translate(Vector3.forward * Mathf.Clamp(_movementSpeed, 0, _maxSpeed) * Time.deltaTime);
                }
                else
                {
                    if (_movementSpeed < _maxSpeed)
                    {
                        _movementSpeed += _deacceleration * Time.deltaTime;
                    }
                    transform.Translate(Vector3.forward * Mathf.Clamp(_movementSpeed, 0, _maxSpeed) * Time.deltaTime);
                }
/*                transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);*/
            }
            else
            {
                _reachedDestination = true;
            }
            //for animator
            /*            _velocity = (transform.position - _lastPosition) / Time.deltaTime;
                        _velocity.y = 0;
                        var velocityMag = _velocity.magnitude;
                        _velocity = _velocity.normalized;
                        var fwdDotProduct = Vector3.Dot(transform.forward, _velocity);
                        var rightDotProduct = Vector3.Dot(transform.right, _velocity);*/
        }
    }
    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
/*        Debug.DrawLine(sensorStartPos, sensorStartPos + transform.forward * sensorLength, Color.white);
        Debug.DrawLine(sensorStartPos + offset / 4, sensorStartPos + (transform.forward + offset) * sensorLength, Color.white);*/
/*        Debug.DrawLine(sensorStartPos, sensorStartPos + transform.forward * sensorLength, Color.white);
        Debug.DrawLine(sensorStartPos, sensorStartPos + transform.forward * sensorLength, Color.white);
        Debug.DrawLine(sensorStartPos, sensorStartPos + transform.forward * sensorLength, Color.white);
        Debug.DrawLine(sensorStartPos, sensorStartPos + transform.forward * sensorLength, Color.white);
        Debug.DrawLine(sensorStartPos, sensorStartPos + transform.forward * sensorLength, Color.white);*/
        //front sensor
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Citizen") || hit.collider.CompareTag("Vehicle"))
            {
                isbreak = true;
                calculateHeuristic(sensorStartPos, hit.point);
                /*Debug.DrawLine(sensorStartPos, hit.point, Color.yellow);*/
                return;
            }
        }
        if (Physics.Raycast(sensorStartPos, transform.forward + offset / 8, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Citizen") || hit.collider.CompareTag("Vehicle"))
            {
                isbreak = true;
                calculateHeuristic(sensorStartPos, hit.point);
                /*Debug.DrawLine(sensorStartPos + offset, hit.point, Color.yellow);*/
                return;
            }
        }
        //front 1st right sensor
        if (Physics.Raycast(sensorStartPos, transform.forward + offset / 4, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Citizen") || hit.collider.CompareTag("Vehicle"))
            {
                isbreak = true;
                calculateHeuristic(sensorStartPos, hit.point);
                /*Debug.DrawLine(sensorStartPos + offset, hit.point, Color.yellow);*/
                return;
            }
        }
        if (Physics.Raycast(sensorStartPos, transform.forward - offset / 8, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Citizen") || hit.collider.CompareTag("Vehicle"))
            {
                isbreak = true;
                calculateHeuristic(sensorStartPos, hit.point);
                /*Debug.DrawLine(sensorStartPos - offset, hit.point, Color.yellow);*/
                return;
            }
        }
        //front 1st left sensor
        if (Physics.Raycast(sensorStartPos, transform.forward - offset / 4, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Citizen") || hit.collider.CompareTag("Vehicle"))
            {
                isbreak = true;
                calculateHeuristic(sensorStartPos, hit.point);
                /*Debug.DrawLine(sensorStartPos - offset, hit.point, Color.yellow);*/
                return;
            }
        }
        isbreak = false;
    }
    public void calculateHeuristic(Vector3 start, Vector3 end)
    {
        Vector3 total = end - start;
        heuristic = 1 - (total.magnitude) / sensorLength;
    }
    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
        _reachedDestination = false;
    }
}
