using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    public Transform parentToRenturnTo = null;
    public Transform cardplaceholderParent = null;
    GameObject cardPlaceholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        cardPlaceholder = new GameObject();
        cardPlaceholder.transform.SetParent(this.transform.parent);
        LayoutElement layout = cardPlaceholder.AddComponent<LayoutElement>();
        layout.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        layout.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        layout.flexibleWidth = 0;
        layout.flexibleHeight = 0;

        cardPlaceholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToRenturnTo = this.transform.parent;
        cardplaceholderParent = parentToRenturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

        if(cardPlaceholder.transform.parent != cardPlaceholder)
        {
            cardPlaceholder.transform.SetParent(cardplaceholderParent);
        }
        int newSiblingIndex = cardplaceholderParent.childCount;

        for(int i=0; i < cardplaceholderParent.childCount; i++)
        {
            if (this.transform.position.x < cardplaceholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (cardPlaceholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }
        cardPlaceholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent( parentToRenturnTo );
        this.transform.SetSiblingIndex( cardPlaceholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(cardPlaceholder);
       
    }
    
}
