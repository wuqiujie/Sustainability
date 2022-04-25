using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public static List<Card> _deck = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < ReadCSV._cardList.Count(); i++)
        {
            if(ReadCSV._cardList[i]._isRoot == 1)
            {
                _deck.Add(ReadCSV._cardList[i]);
            }
        }
    }
    public static void UpdateDeck(int Id)
    {
        var item = _deck.SingleOrDefault(x => x.id == Id);
        if(item != null)
        {
            if(item._nextCards != null)
            {
                foreach(var i in item._nextCards)
                {
                    for (int j = 0; j < ReadCSV._cardList.Count(); j++)
                    {
                        if (ReadCSV._cardList[j].id == i)
                        {
                            _deck.Add(ReadCSV._cardList[j]);
                        }
                    }
                }
            }
            _deck.Remove(item);
        }
    }
}
