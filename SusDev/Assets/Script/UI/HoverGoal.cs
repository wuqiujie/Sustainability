using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverGoal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject goal; 
    public GameObject tooltipImage;

    public void Start()
    {
        Color temp = gameObject.GetComponent<Image>().color;
        temp.a = 0f;
        goal.GetComponent<Image>().color = temp;
        tooltipImage.gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
       
        Color temp = gameObject.GetComponent<Image>().color;
        temp.a = 1f;
        goal.GetComponent<Image>().color = temp;
        tooltipImage.gameObject.SetActive(true);

    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Color temp = gameObject.GetComponent<Image>().color;
        temp.a = 0f;
        goal.GetComponent<Image>().color = temp;
        tooltipImage.gameObject.SetActive(false);
    }
}
