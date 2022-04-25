using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWaypointNavigator : MonoBehaviour
{
    CarNavController _controller;
    public Waypoint _currentWP;
    bool shouldBranch = false;
    bool branchCD = false;
    private void Awake()
    {
        _controller = GetComponent<CarNavController>();
    }
    private void Start()
    {
        _controller.SetDestination(_currentWP.GetPosition());
    }
    // Update is called once per frame
    void Update()
    {
        if (_controller._reachedDestination)
        {
            if (_currentWP.branches != null && _currentWP.branches.Count > 0 && !branchCD)
            {
                shouldBranch = Random.Range(0f, 1f) <= _currentWP.branchRatio ? true : false;
            }

            if (shouldBranch && !branchCD && _currentWP.branches.Count > 0)
            {
                _currentWP = _currentWP.branches[Random.Range(0, _currentWP.branches.Count - 1)];
                branchCD = true;
                StartCoroutine(ToggleFlag());
            }
            else
            {
                _currentWP = _currentWP._nextWP;
            }
            _controller.SetDestination(_currentWP.GetPosition());
        }
    }
    private IEnumerator ToggleFlag()
    {
        float duration = 3f; // 3 seconds you can change this 
                              //to whatever you want
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        branchCD = false;
    } 
}
