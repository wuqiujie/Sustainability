using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDesk : MonoBehaviour
{
    public List<Card> deck;
    public static List<Card> staticDeck = new List<Card>();

  //  public GameObject[] currentDeck;
    public GameObject[] currentZone;
  //  public ThisCard currentPlayCard;

    public Card[] cards;

    public static int deskSize =8;

    public GameObject HandArea;
    public GameObject TableArea;
    public GameObject ZoneArea;

    public GameObject CardToHand;
    public GameObject CardToTable;

    public int count;
    public AudioManager audioManager;

    public void StartTurn()
    {
        deskSize = 6;
        count = 0;
        deck = new List<Card>();
        Shuffle();
        for (int i = 0; i < deskSize; i++)
        {
            /*int cardSize = ReadCSV._cardList.Count;
            deck.Add(ReadCSV._cardList[i]);*/
            //new
            int cardSize = DeckManager._deck.Count;
            deck.Add(DeckManager._deck[i]);
        }
        StartCoroutine(StartTurnByTime());
    }

    public static void Shuffle()
    {

        System.Random random = new System.Random();
        int cardSize = DeckManager._deck.Count;

        for (int j = 0; j < cardSize-1; j++)
        {
            int rd = random.Next(j, cardSize-1);
            Card temporary = DeckManager._deck[rd];
            DeckManager._deck[rd] = DeckManager._deck[j];
            DeckManager._deck[j] = temporary;
        }
    }

    IEnumerator StartTurnByTime()
    {
        
        for (int i = 0; i < 6; i++)
        {
            audioManager.PlayDealCard();
            yield return new WaitForSeconds(0.1f);
            var myCard = Instantiate(CardToHand, transform.position, transform.rotation);
            HandArea = GameObject.Find("HandArea");
            myCard.transform.SetParent(HandArea.transform);
           
        }
    }
    



    
    public void RandomCard()
    {
         StartCoroutine(RandomCardByTime());
    }
     IEnumerator RandomCardByTime()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var myCard = Instantiate(CardToHand, transform.position, transform.rotation);
            TableArea = GameObject.Find("TableArea");
            myCard.transform.SetParent(TableArea.transform);
        }
    }


    void Update()
    {
      
        staticDeck = deck;

      /*
        HandArea = GameObject.Find("HandArea");
        currentDeck = new GameObject[HandArea.transform.childCount];
        for (int i = 0; i < currentDeck.Length; i++)
        {
            currentDeck[i] = HandArea.transform.GetChild(i).gameObject;
        }

        /** Finish Choosing Cards **/
       /*
        if (HandArea.transform.childCount == 7)
        {
            TableArea = GameObject.Find("TableArea");
          

            for (int i = 0; i < TableArea.transform.childCount; i++)
            {
                count++;
                Destroy(TableArea.transform.GetChild(i).gameObject);
  
            }

            if (count == TableArea.transform.childCount)
            {
                
                TableArea.SetActive(false);
            }
        }
        
        
      
        ZoneArea = GameObject.Find("ZoneArea");
        currentZone = new GameObject[ZoneArea.transform.childCount];

        
        for (int i = 0; i < currentZone.Length; i++)
        {
             currentZone[i] = ZoneArea.transform.GetChild(i).gameObject; 

        }
     */  

        

    }




}
