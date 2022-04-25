using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterviewCitizen : MonoBehaviour
{
    public GameObject[] LCitizen;
    public GameObject[] RCitizen;
    int Lindex = 0;
    int Rindex = 0;
    public GameObject LeftCitizen;
    public GameObject RightCitizen;

    private void Start()
    {
        Create();
    }
    public void Create()
    {
        Lindex = Random.Range(0, 3);
        Rindex = Random.Range(0, 3);

        var Left = Instantiate(LCitizen[Lindex],
                  LeftCitizen.transform.position,
                  LeftCitizen.transform.rotation);
        Left.transform.SetParent(LeftCitizen.gameObject.transform);
        var Right = Instantiate(RCitizen[Rindex],
                  RightCitizen.transform.position,
                  RightCitizen.transform.rotation);
       Right.transform.SetParent(RightCitizen.gameObject.transform);
    }



 
}
