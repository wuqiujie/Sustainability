using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReadCSV : MonoBehaviour
{
    public TextAsset _cardDB;
    public TextAsset _scardDB;
    public static List<Card> _cardList = new List<Card>();
    private void Awake()
    {
        _cardDB = Resources.Load<TextAsset>("CardDatabase");
        _scardDB = Resources.Load<TextAsset>("CardDatabaseSpecial");
        Read();
        ReadSpecial();
    }
    public void Read()
    {
        string[] data = _cardDB.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int rows = data.Length / 14 - 1;
        for (int i = 0; i < rows; i++)
        {
            int id = int.Parse(data[14 * (i + 1)]);
            string Card_name = data[14 * (i + 1) + 1];
            string Card_description = data[14 * (i + 1) + 2];
            Card_description = Card_description.Replace("*",",");
            int Cost = int.Parse(data[14 * (i + 1) + 3]);
            int Type = int.Parse(data[14 * (i + 1) + 4]);
            int Construction = int.Parse(data[14 * (i + 1) + 5]);
            string Card_sprite = data[14 * (i + 1) + 6];
            int Environment = int.Parse(data[14 * (i + 1) + 7]);
            int Life_expectancy = int.Parse(data[14 * (i + 1) + 8]);
            int Social_stability = int.Parse(data[14 * (i + 1) + 9]);
            int Economics = int.Parse(data[14 * (i + 1) + 10]);
            int[] Goals = null;
            //if (data[14 * (i + 1) + 11] != "-1" && data[14 * (i + 1) + 11] != "")
            if (data[14 * (i + 1) + 11] != "")
            {
                Goals = Array.ConvertAll(data[14 * (i + 1) + 11].Split('*'), int.Parse);
            }
            int[] NextCards = null;
            if (data[14 * (i + 1) + 12] != "-1" && data[14 * (i + 1) + 12] != "")
            {
                NextCards = Array.ConvertAll(data[14 * (i + 1) + 12].Split('/'), int.Parse);
            }
            int IsRoot = int.Parse(data[14 * (i + 1) + 13]);

            _cardList.Add(new Card(id, Card_name, Card_description, Cost, Type, Construction, Resources.Load<Sprite>(Card_sprite), Environment, Life_expectancy, Social_stability, Economics, Goals, NextCards, IsRoot));
        }
    }
    public void ReadSpecial()
    {
        string[] data = _scardDB.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int rows = data.Length / 24 - 1;
        for (int i = 0; i < rows; i++)
        {
            int id = int.Parse(data[24 * (i + 1)]);
            string Card_name = data[24 * (i + 1) + 1];
            string Card_description = data[24 * (i + 1) + 2];
            Card_description = Card_description.Replace("*", ",");
            int Cost = int.Parse(data[24 * (i + 1) + 3]);
            int Type = int.Parse(data[24 * (i + 1) + 4]);
            int Construction = int.Parse(data[24 * (i + 1) + 5]);
            string Card_sprite = data[24 * (i + 1) + 6];
            int Environment = int.Parse(data[24 * (i + 1) + 7]);
            int Life_expectancy = int.Parse(data[24 * (i + 1) + 8]);
            int Social_stability = int.Parse(data[24 * (i + 1) + 9]);
            int Economics = int.Parse(data[24 * (i + 1) + 10]);

            int[] Goals = null;
            // if (data[24 * (i + 1) + 11] != "-1" && data[24 * (i + 1) + 11] != "")
            if (data[24 * (i + 1) + 11] != "")
            {
                Goals = Array.ConvertAll(data[24 * (i + 1) + 11].Split('*'), int.Parse);
            }
            int[] NextCards = null;
            if (data[24 * (i + 1) + 12] != "-1" && data[24 * (i + 1) + 12] != "")
            {
                NextCards = Array.ConvertAll(data[24 * (i + 1) + 12].Split('/'), int.Parse);
            }
            int IsRoot = int.Parse(data[24 * (i + 1) + 13]);

            int conditionIndex = int.Parse(data[24 * (i + 1) + 14]);
            int conditionThresh = int.Parse(data[24 * (i + 1) + 15]);
            int changeIndex = int.Parse(data[24 * (i + 1) + 16]);
            int conditionTrue = int.Parse(data[24 * (i + 1) + 17]);
            int conditionFalse = int.Parse(data[24 * (i + 1) + 18]);

            int LTenvironment = int.Parse(data[24 * (i + 1) + 19]);
            int LTlife = int.Parse(data[24 * (i + 1) + 20]);
            int LTsocial = int.Parse(data[24 * (i + 1) + 21]);
            int LTeconomic = int.Parse(data[24 * (i + 1) + 22]);
            int LTcost = int.Parse(data[24 * (i + 1) + 23]);
            SpecialCard sCard = new SpecialCard(id, Card_name, Card_description, Cost, Type, Construction, Resources.Load<Sprite>(Card_sprite), Environment, Life_expectancy, Social_stability, Economics,
                Goals, NextCards, IsRoot,
                conditionIndex, conditionThresh, changeIndex, conditionTrue, conditionFalse, LTenvironment, LTlife, LTsocial, LTeconomic, LTcost);
            _cardList.Add(sCard);
        }
    }
}
