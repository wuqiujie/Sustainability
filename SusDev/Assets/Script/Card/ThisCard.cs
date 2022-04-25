using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour
{
    public Card thisCard = new Card();
    public int thisCardID;

    /***Card Info***/
    public int id;
    public string card_name;
    public string card_description;
    public Image card_image;
    public Sprite card_sprite;
    public int cost;
    public int type;
    public int construction;

    /*** Affect***/
    public int environment_index;
    public int life_expectancy_index;
    public int social_stability_index;
    public int economics_index;

    /*** long-term Affect***/
    public int LTEnvironment;
    public int LTLife;
    public int LTSocial;
    public int LTeconomics;
    public int LTBudget;

    public int numOfCandsInDesk;


    public bool canBeDestroyed;
    public GameObject Collection;
    public bool beInCollection;

    public int[] goals;
    public bool questionMark;
    public bool LTMark;


    void Start()
    {
        thisCard = ReadCSV._cardList[thisCardID];
        numOfCandsInDesk = PlayerDesk.deskSize;
        LTEnvironment = 0;
        LTLife = 0;
        LTSocial = 0;
        LTeconomics = 0;
        LTBudget = 0;
        questionMark = false;

    }

    void Update()
    {
       
        /***Card Info ***/
        id = thisCard.getID();
        card_name = thisCard.getCardName();
        card_description = thisCard.getCardDescription();
        cost = thisCard.getCost();
        type = thisCard.getType();
        construction = thisCard.getConstruction();
        card_sprite = thisCard.getCardSprite();
        card_image.sprite = card_sprite;

        environment_index = thisCard.getEnvironment();
        life_expectancy_index = thisCard.getLife_expectancy();
        social_stability_index = thisCard.getSocial_stability();
        economics_index = thisCard.getEconomics();

        goals = thisCard.getGoals();
        //update long-term effect
        if (thisCard.GetType() == typeof(SpecialCard))
        {
            UpdateLT((SpecialCard)thisCard);
            if(LTLife == 0 && LTSocial == 0 && LTeconomics == 0 && LTEnvironment == 0)
            {
                questionMark = true;
            }
            if (LTLife != 0 || LTSocial != 0 || LTeconomics != 0 || LTEnvironment != 0)
            {
                LTMark = true;
            }
        }

        if (this.tag == "Clone")
        {
            thisCard = PlayerDesk.staticDeck[numOfCandsInDesk - 1];
            numOfCandsInDesk -= 1;
            PlayerDesk.deskSize -= 1;
            this.tag = "Untagged";
        }
    }
    public void UpdateLT(SpecialCard s)
    {
        LTEnvironment = s.getLTEnvironment();
        LTLife = s.getLTLife();
        LTSocial = s.getLTSocial();
        LTeconomics = s.getLTEconomic();
        LTBudget = s.getLTCost();
    }

}
