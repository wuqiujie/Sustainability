using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalStayPressed : MonoBehaviour
{
    public bool shiftOn;
    public void ShiftClicked()
    {
        Debug.Log("goal : " + gameObject.name);
        Debug.Log(shiftOn);
        shiftOn = !shiftOn;
        if (shiftOn)
            gameObject.GetComponent<Animator>().SetBool("Selected", true);
        else
            gameObject.GetComponent<Animator>().SetBool("Normal", true);
    }
}
