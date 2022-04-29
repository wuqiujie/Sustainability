using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class CollectionUIManager : MonoBehaviour
{
    public GameObject[] cardSlot;
    public GameObject[] goalSlot;
    public GameObject[] turnPageButton;
    public GameManager gameManager;
    public List<int> collectedCards;
    public GameObject desPanel;
    public GameObject[] relatedGoalSlot;
    public GameObject scrollRect;
    private int collectionPage;
    public GameObject goal17Panel;
    public static List<Card> wholeCollection = ReadCSV._cardList;
    Vector3 cardPanelPos;
    void Start()
    {

        //cardPanelPos = scrollRect.transform.GetChild(0).transform.position;
        collectedCards = new List<int>();
        wholeCollection = wholeCollection.OrderBy(x => x.id).ToList();
        //Debug.Log("card amount: " + wholeCollection.Count);
        for (int i = 0; i < wholeCollection.Count; i++)
        {
            //check if all cards are loaded correctly
        //    Debug.Log("----------");
       //     Debug.Log("card id: " + wholeCollection[i].getID());
        //    Debug.Log("card name: " + wholeCollection[i].getCardName());
        //    Debug.Log("card description: " + wholeCollection[i].getCardDescription());

            string printGoals = "card related goals: ";
            foreach (var item in wholeCollection[i]._goals)
            {
                printGoals += item.ToString() + ",";
            }
       //     Debug.Log(printGoals);

        }


    }
    private void Update()
    {
        collectedCards = gameManager.collectID;
        DisplayCards();
    }

    private void DisplayCards()
    {
        
        for (int i = 0; i < wholeCollection.Count; i++)
        {
            //display the cards

            cardSlot[i].GetComponent<Image>().sprite = wholeCollection[i].getCardSprite();
            //display the card if it has already been played
            if (collectedCards.Contains(i))
            {
                //hide the lock mask to reveal the whole card
                cardSlot[i].transform.GetChild(0).gameObject.SetActive(false);
                //enable click to shohw more detailed info
                cardSlot[i].GetComponent<Button>().enabled = true;
                //place the played card at the front
                cardSlot[i].transform.SetSiblingIndex(0);
            }


        }
    }

    public void Search(int _searchgoal)
    {
        scrollRect.transform.GetChild(0).transform.position = new Vector3(963,105,0);
        if (_searchgoal == 8)
        {
            goal17Panel.gameObject.SetActive(false);
            scrollRect.GetComponent<ScrollRect>().enabled = true;
        }
        if (_searchgoal == 17)
        {
            goal17Panel.gameObject.SetActive(true);
        }
        else
        {
            goal17Panel.gameObject.SetActive(false);
            scrollRect.GetComponent<ScrollRect>().enabled = false;
        }
            for (int i = 0; i < wholeCollection.Count; i++)
            {
                if (Array.Exists(wholeCollection[i]._goals, element => element == _searchgoal))
                {
                    cardSlot[i].gameObject.SetActive(true);
                }
                else
                {
                    cardSlot[i].gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < 16 & i != (_searchgoal - 1); i++)
            {
                goalSlot[i].GetComponent<Animator>().SetBool("Normal", true);
            }
       


        DisplayCards();
    }

    public void UpdateAlpha()
    {
        for (int i = 0; i < 17; i++)
        {
            if (gameManager.goalCollect[i] == true)
            {
                var collectedColor = goalSlot[i].transform.GetChild(1).GetComponent<Image>().color;
                collectedColor.a = 0.5f;
                goalSlot[i].transform.GetChild(1).GetComponent<Image>().color = collectedColor;
            }
            else
            {
                var tempColor = goalSlot[i].transform.GetChild(1).GetComponent<Image>().color;
                tempColor.a = 0;
                goalSlot[i].transform.GetChild(1).GetComponent<Image>().color = tempColor;
            }

        }

    }
    public void NextPage()
    {
        collectionPage++;
        UpdateAlpha();
        TurnPage(collectionPage);
    }

    public void PreviousPage()
    {
        collectionPage--;
        UpdateAlpha();
        TurnPage(collectionPage);
    }
    public void TurnPage(int _page)
    {
        //Debug.Log("current page: " + _page);
        if(_page == 0)
        {
            goalSlot[16].gameObject.SetActive(false);
            for (int i = 0; i < 8; i++)
            {
                goalSlot[i].gameObject.SetActive(true);
                goalSlot[i + 8].gameObject.SetActive(false);
            }
            turnPageButton[0].gameObject.SetActive(false);
            turnPageButton[1].gameObject.SetActive(true);
            Search(1);
            goalSlot[0].GetComponent<Animator>().SetBool("Pressed", true);

        }

        if (_page == 1)
        {
            goalSlot[16].gameObject.SetActive(false);
            for (int i = 0; i < 8; i++)
            {
                goalSlot[i].gameObject.SetActive(false);
                goalSlot[i + 8].gameObject.SetActive(true);
            }
            turnPageButton[1].gameObject.SetActive(true);
            turnPageButton[0].gameObject.SetActive(true);
            Search(9);
            goalSlot[8].GetComponent<Animator>().SetBool("Pressed", true);
        }

        if (_page == 2)
        {
            goalSlot[16].gameObject.SetActive(true);
            for (int i = 0; i < 8; i++)
            {
                goalSlot[i].gameObject.SetActive(false);
                goalSlot[i + 8].gameObject.SetActive(false);
                
            }
            turnPageButton[1].gameObject.SetActive(false);
            turnPageButton[0].gameObject.SetActive(true);
            Search(17);
            goalSlot[16].GetComponent<Animator>().SetBool("Pressed", true);
        }
    }

    public void AddCard(int _cardid)
    {
        collectedCards.Add(_cardid);
        string result = "collected cards: ";
        foreach(var item in collectedCards)
        {
            result += item.ToString() + ",";
        }
        Debug.Log(result);
    }

    public void ShowDescription(int _cardid)
    {
        desPanel.gameObject.SetActive(true);
        desPanel.gameObject.transform.GetChild(1).GetComponent<Text>().text = wholeCollection[_cardid].getCardDescription();
        desPanel.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = wholeCollection[_cardid].getCardSprite();
        
        for(int i = 0; i < relatedGoalSlot.Length; i++)
        {
            relatedGoalSlot[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < wholeCollection[_cardid]._goals.Length; i++)
        {
            //Debug.Log(wholeCollection[_cardid]._goals[i]);
            relatedGoalSlot[wholeCollection[_cardid]._goals[i]-1].gameObject.SetActive(true);

        }

    }

    public void HideDescrptioin()
    {
        desPanel.gameObject.SetActive(false);
    }
}
