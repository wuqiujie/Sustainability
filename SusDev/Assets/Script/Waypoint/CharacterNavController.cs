using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavController : MonoBehaviour
{
    public Vector3 _destination;
    public bool _reachedDestination;
    public float _stopDistance;
    public float _rotationSpeed;
    public float _maxSpeed;
    public float _movementSpeed;
    public float _maxmovementSpeed;
    private float _deacceleration;
    public Vector3 _velocity;
    public Vector3 _lastPosition;

    public bool isbreak;
    [Header("Sensors")]
    public float sensorLength = 1f;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    private float heuristic;

    private Animator AC;
    private float ACtimer;
    private float timeRemaining;
    // Start is called before the first frame update
    void Start()
    {
        _reachedDestination = false;
        _maxmovementSpeed = 0.5f;
        _rotationSpeed = 360f;
        _stopDistance = 0.2f;
        _deacceleration = 4;
        AC = GetComponent<Animator>();
        ACtimer = 3f;
        timeRemaining = ACtimer;
        CalculateAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        /* _deacceleration = _maxSpeed * _maxSpeed / (sensorLength * 2);*/
        UpdateAnimator();
        Sensors();
        _lastPosition = transform.position;
        if(transform.position != _destination)
        {
            Vector3 destinationDirection = _destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;
            if (destinationDistance >= _stopDistance)
            {
                _reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                if (isbreak)
                {
                    if (_movementSpeed > 0)
                    {
                        _movementSpeed -= 4 * _deacceleration * Time.deltaTime;
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
    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
        _reachedDestination = false;
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
            /*Debug.DrawLine(sensorStartPos, sensorStartPos + transform.forward * sensorLength, Color.white);*/
            if (hit.collider.CompareTag("Vehicle"))
            {
                isbreak = true;
                calculateHeuristic(sensorStartPos, hit.point);
                /*Debug.DrawLine(sensorStartPos, hit.point, Color.yellow);*/
                return;
            }
        }
        if (Physics.Raycast(sensorStartPos + offset, transform.forward + offset, out hit, sensorLength))
        {
            /*Debug.DrawLine(sensorStartPos + offset, sensorStartPos + (transform.forward+offset) * sensorLength, Color.white);*/
            if (hit.collider.CompareTag("Vehicle"))
            {
                isbreak = true;
                calculateHeuristic(sensorStartPos, hit.point);
                /*Debug.DrawLine(sensorStartPos + offset, hit.point, Color.yellow);*/
                return;
            }
        }
        if (Physics.Raycast(sensorStartPos - offset, transform.forward - offset, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Vehicle"))
            {
                /*Debug.DrawLine(sensorStartPos - offset, sensorStartPos + (transform.forward - offset) * sensorLength, Color.white);*/
                isbreak = true;
                calculateHeuristic(sensorStartPos, hit.point);
                /*Debug.DrawLine(sensorStartPos - offset, hit.point, Color.yellow);*/
                return;
            }
        }

        isbreak = false;
    }
    private void UpdateAnimator()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            CalculateAnimator();
            timeRemaining = ACtimer;
        }
    }
    private void CalculateAnimator()
    {
        switch (GameManager.total_life)
        {
            case 0:
                DisableStates();
                AC.SetBool("_isSitSad", true);
                _maxSpeed = 0f;
                break;
            case 1:
                if(Random.Range(0.0f,1.0f) < 0.5f)
                {
                    DisableStates();
                    AC.SetBool("_isSitSad", true);
                    _maxSpeed = 0f;
                }
                else
                {
                    DisableStates();
                    AC.SetBool("_isWalkSad", true);
                    _maxSpeed = _maxmovementSpeed / 2;
                }
                break;
            case 2:
                if (Random.Range(0.0f, 1.0f) < 0.25f)
                {
                    DisableStates();
                    AC.SetBool("_isSitSad", true);
                    _maxSpeed = 0;
                }
                else
                {
                    DisableStates();
                    AC.SetBool("_isWalkSad", true);
                    _maxSpeed = _maxmovementSpeed / 2;
                }
                break;
            case 3:
                DisableStates();
                AC.SetBool("_isWalkSad", true);
                _maxSpeed = _maxmovementSpeed / 2;
                break;
            case 4:
                if (Random.Range(0.0f, 1.0f) < 0.25f)
                {
                    DisableStates();
                    AC.SetBool("_isWalkNormal", true);
                    _maxSpeed = _maxmovementSpeed;
                }
                else
                {
                    DisableStates();
                    AC.SetBool("_isWalkSad", true);
                    _maxSpeed = _maxmovementSpeed / 2;
                }
                break;
            case 5:
                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    DisableStates();
                    AC.SetBool("_isWalkNormal", true);
                    _maxSpeed = _maxmovementSpeed;
                }
                else
                {
                    DisableStates();
                    AC.SetBool("_isWalkSad", true);
                    _maxSpeed = _maxmovementSpeed / 2;
                }
                break;
            case 6:
                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    DisableStates();
                    AC.SetBool("_isWalkNormal", true);
                    _maxSpeed = _maxmovementSpeed;
                }
                else
                {
                    DisableStates();
                    AC.SetBool("_isWalkSad", true);
                    _maxSpeed = _maxmovementSpeed / 2;
                }
                break;
            case 7:
                DisableStates();
                AC.SetBool("_isWalkNormal", true);
                _maxSpeed = _maxmovementSpeed;
                break;
            case 8:
                if (Random.Range(0.0f, 1.0f) < 0.8f)
                {
                    DisableStates();
                    AC.SetBool("_isWalkNormal", true);
                    _maxSpeed = _maxmovementSpeed;
                }
                else
                {
                    DisableStates();
                    AC.SetBool("_isRun", true);
                    _maxSpeed = _maxmovementSpeed * 1.5f;
                }
                break;
            case 9:
                if (Random.Range(0.0f, 1.0f) < 0.6f)
                {
                    DisableStates();
                    AC.SetBool("_isWalkNormal", true);
                    _maxSpeed = _maxmovementSpeed;
                }
                else
                {
                    DisableStates();
                    AC.SetBool("_isRun", true);
                    _maxSpeed = _maxmovementSpeed * 1.5f;
                }
                break;
            case 10:
                /*if (Random.Range(0.0f, 1.0f) < 0.4f)
                {
                    DisableStates();
                    AC.SetBool("_isWalkNormal", true);
                    _maxSpeed = _maxmovementSpeed;
                }
                else
                {
                    DisableStates();
                    AC.SetBool("_isRun", true);
                    _maxSpeed = _maxmovementSpeed * 1.5f;
                }*/
                DisableStates();
                AC.SetBool("_isRun", true);
                _maxSpeed = _maxmovementSpeed * 1.5f;
                break;
            default:
                break;
        }

    }
    private void DisableStates()
    {
        AC.SetBool("_isSitSad",false);
        AC.SetBool("_isWalkSad", false);
        AC.SetBool("_isWalkNormal", false);
        AC.SetBool("_isRun", false);
    }
    private void calculateHeuristic(Vector3 start, Vector3 end)
    {
        Vector3 total = end - start;
        heuristic = 1 - (total.magnitude) / sensorLength;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Vehicle")
        {
            transform.position = _destination;
            _reachedDestination = true;
        }
    }
}
