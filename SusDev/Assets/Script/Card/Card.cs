using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Card
{
    public MonoBehaviour monoBehaviour;
    /***Card***/
    public int id;
    public string card_name;
    public string card_description;
    public Sprite card_sprite;
    public int cost;
    public int type;
    public int construction;


    /*** Affect***/
    public int environment_index;
    public int life_expectancy_index;
    public int social_stability_index;
    public int economics_index;

    //goals and next related cards
    public int[] _goals;
    public int[] _nextCards;
    public int _isRoot;




    public Card()
    {
       
    }

    public Card(GameObject gameObject)
    {
        
    }
    public Card GetInstance(GameObject card)
    {
        return new Card(card);
    }

    public Card(int Id, string Card_name, string Card_description,
        int Cost, int Type, int Construction, Sprite Card_sprite,
       int Environment, int Life_expectancy,
       int Social_stability, int Economics, int[] goals, int[] nextCards, int isRoot)
    {
        id = Id;
        card_name = Card_name;
        card_description = Card_description;
        cost = Cost;
        type = Type;
        construction = Construction;
        card_sprite = Card_sprite;
        environment_index = Environment;
        life_expectancy_index = Life_expectancy;
        social_stability_index = Social_stability;
        economics_index = Economics;

        _goals = goals;
        _nextCards = nextCards;
        _isRoot = isRoot;
    }


    public int getID()
    {
        return id;
    }

    public string getCardName()
    {
        return card_name;
    }

    public string getCardDescription()
    {
        return card_description;
    }
    public int getCost()
    {
        return cost;
    }

    public int getType()
    {
        return type;
    }
    public int getConstruction()
    {
        return construction;
    }

    public Sprite getCardSprite()
    {
        return card_sprite;
    }

    public int[] getGoals()
    {
        return _goals;
    }

    public virtual int getEnvironment()
    {
        return environment_index;
    }

    public virtual int getLife_expectancy()
    {
        return life_expectancy_index;
    }
    public virtual int getSocial_stability()
    {
        return social_stability_index;
    }
    public virtual int getEconomics()
    {
        return economics_index;
    }

}
