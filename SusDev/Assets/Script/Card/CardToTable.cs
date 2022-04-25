using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToTable : MonoBehaviour
{
    public GameObject Table;
    public GameObject randamCard;
    
    void Start()
    {
        Table = GameObject.Find("TableArea");
        randamCard.transform.SetParent(Table.transform);
        randamCard.transform.localScale = Vector3.one;
        randamCard.transform.position= new Vector3 (transform.position.x,transform.position.y,-48);
        randamCard.transform.eulerAngles = new Vector3(25, 0, 0);

    }
}
