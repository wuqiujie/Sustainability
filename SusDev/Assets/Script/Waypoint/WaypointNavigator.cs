using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    CharacterNavController _controller;
    public Waypoint _currentWP;
    int direction;
    bool shouldBranch = false;
    bool branchCD = false;
    private void Awake()
    {
        _controller = GetComponent<CharacterNavController>();
    }
    private void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f,1f));
        _controller.SetDestination(_currentWP.GetPosition());
    }
    // Update is called once per frame
    void Update()
    {
        if(_controller._reachedDestination)
        {
            if(_currentWP.branches != null && _currentWP.branches.Count > 0 && !branchCD)
            {
                shouldBranch = Random.Range(0f, 1f) <= _currentWP.branchRatio ? true : false;
            }
            if(shouldBranch && !branchCD && _currentWP.branches.Count > 0)
            {
                _currentWP = _currentWP.branches[Random.Range(0, _currentWP.branches.Count - 1)];
                branchCD = true;
                StartCoroutine(ToggleFlag());
            }
            else
            {
                if (direction == 0)
                {
                    if(_currentWP._nextWP != null)
                    {
                        _currentWP = _currentWP._nextWP;
                    }
                    else
                    {
                        _currentWP = _currentWP._prevWP;
                        direction = 1;
                    }
                }
                else if (direction == 1)
                {
                    if(_currentWP._prevWP != null)
                    {
                        _currentWP = _currentWP._prevWP;
                    }
                    else
                    {
                        _currentWP = _currentWP._nextWP;
                        direction = 0;
                    }
                }
            }
            _controller.SetDestination(_currentWP.GetPosition());
        }
    }
    private IEnumerator ToggleFlag()
    {
        float duration = 10f; 
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        branchCD = false;
    }
}
