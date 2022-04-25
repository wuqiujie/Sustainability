using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragZone : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler
{

    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(pointerEventData.pointerDrag == null)
        {
            return;
        }
        CardDrag card_Drag = pointerEventData.pointerDrag.GetComponent<CardDrag>();
        if (card_Drag != null)
        {
            card_Drag.cardplaceholderParent = this.transform;
        }

    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null)
        {
            return;
        }

        CardDrag card_Drag = pointerEventData.pointerDrag.GetComponent<CardDrag>();
        if (card_Drag != null && card_Drag.cardplaceholderParent==this.transform)
        {
            card_Drag.cardplaceholderParent = card_Drag.parentToRenturnTo;
        }
    }
    

    public void OnDrop(PointerEventData pointerEventData)
    {
        CardDrag card_Drag = pointerEventData.pointerDrag.GetComponent<CardDrag>();
        if (card_Drag != null)
        {
            card_Drag.parentToRenturnTo = this.transform;
        }
    }

}
