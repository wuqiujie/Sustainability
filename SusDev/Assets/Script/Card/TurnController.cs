using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TurnController : MonoBehaviour
{
    /**Zone**/
    public GameObject ZoneArea;
    public GameObject TableArea;
    public GameObject HandArea;
    public GameObject Collection;

    public PlayerDesk playerDesk;
    public VFXManager vFX;
    public CityManager grid;

/*    Camera zoom effect*/
    public CameraController cc;

    /**index change**/
    public int environment_change;
    public int life_change;
    public int social_change;
    public int economics_change;
    public int budget_change;

    //long-term change
    public int LTEnvironment;
    public int LTLife;
    public int LTSocial;
    public int LTeconomics;
    public int LTBudget;

    public bool isMoved = false;
    public GameObject goalPanel;

    public GameObject hangtag_go;
    /**Collection**/
    public List<int> collectID;

    //public GameObject collectionItem;
    public GameObject currentPlayCard;
    public bool[] goalCollect;


    public GameObject indexAnimation0;
    public GameObject indexAnimation1;
    public GameObject indexAnimation2;
    public GameObject indexAnimation3;

    public AudioManager audioManager;
    public GameObject hangtagHigh;
    public GameObject hangtagDown;
    public void StartTurn()
    {
        cc.ResetPos();
        ZoneArea.SetActive(true);
        TableArea.SetActive(false);
        HandArea.SetActive(true);

        /***if there are not 0 will be long term**/
        environment_change = 0;
        life_change = 0;
        social_change = 0;
        economics_change = 0;
        budget_change = 0;

        collectID = new List<int>();
        goalCollect = new bool[17];


        playerDesk.StartTurn();
        // collectionItem.gameObject.GetComponent<CollectionItem>().setcollectionNum(0);
        currentPlayCard = new GameObject();
        isMoved = false;

        for (int i = 0; i < goalPanel.transform.childCount; i++)
        {
            goalPanel.transform.GetChild(i).gameObject.SetActive(false);

        }
        StartCoroutine(HangtagDown());
    }

  
    public void ShowGoal()
    {
        currentPlayCard = ZoneArea.transform.GetChild(0).gameObject;
        if (currentPlayCard.GetComponent<ThisCard>().goals[0] != -1)
        {
            int[] goals = currentPlayCard.GetComponent<ThisCard>().goals;

            for (int i = 0; i < goals.Length; i++)
            {
                goalPanel.transform.GetChild(goals[i] - 1).gameObject.SetActive(true);
                goalCollect[goals[i] - 1] = true;
            }

        }
    }
    public void DestroyGoal()
    {
        for (int i = 0; i < goalPanel.transform.childCount; i++)
        {
            goalPanel.transform.GetChild(i).gameObject.SetActive(false);

        }
    }

    IEnumerator HangtagDown()
    {
        float time = 0;
        Vector3 startPosition = hangtagHigh.transform.position;
        Vector3 endPosition = hangtagDown.transform.position;
        while (time < 1.5f)
        {
            hangtag_go.gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, time / 1.5f);
            time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(HangtagUp());
    }

    IEnumerator HangtagUp()
    {
        float time = 0;
        Vector3 endPosition = hangtagHigh.transform.position;
        Vector3 startPosition = hangtagDown.transform.position;
        while (time < 1.5f)
        {
            hangtag_go.gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, time / 1.5f);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void IndexAnimation(int a0 , int a1, int a2, int a3)
    {
        Animator m_Animator0;
        Animator m_Animator1;
        Animator m_Animator2;
        Animator m_Animator3;
        if (a0 > 0)
        {
            indexAnimation0.SetActive(true);
            m_Animator0 = indexAnimation0.GetComponent<Animator>();
            m_Animator0.SetTrigger("Positive");
        }
        if (a0 < 0)
        {
            indexAnimation0.SetActive(true);
            m_Animator0 = indexAnimation0.GetComponent<Animator>();
            m_Animator0.SetTrigger("Negative");
        }
        if (a1 > 0)
        {
            indexAnimation1.SetActive(true);
            m_Animator1 = indexAnimation1.GetComponent<Animator>();
            m_Animator1.SetTrigger("Positive");
        }
        if (a1 < 0)
        {
            indexAnimation1.SetActive(true);
            m_Animator1 = indexAnimation1.GetComponent<Animator>();
            m_Animator1.SetTrigger("Negative");
        }
        if (a2 > 0)
        {
            indexAnimation2.SetActive(true);
            m_Animator2 = indexAnimation2.GetComponent<Animator>();
            m_Animator2.SetTrigger("Positive");
        }
        if (a2 < 0)
        {
            indexAnimation2.SetActive(true);
            m_Animator2 = indexAnimation2.GetComponent<Animator>();
            m_Animator2.SetTrigger("Negative");
        }
        if (a3 > 0)
        {
            indexAnimation3.SetActive(true);
            m_Animator3 = indexAnimation3.GetComponent<Animator>();
            m_Animator3.SetTrigger("Positive");
        }
        if (a3 < 0)
        {
            indexAnimation3.SetActive(true);
            m_Animator3 = indexAnimation3.GetComponent<Animator>();
            m_Animator3.SetTrigger("Negative");
        }
       /*
        indexAnimation0.SetActive(false);
        indexAnimation1.SetActive(false);
        indexAnimation2.SetActive(false);
        indexAnimation3.SetActive(false);
       */
    }
    public void CalculateCard()
    {

            currentPlayCard = CurrentCard();
            DeckManager.UpdateDeck(currentPlayCard.GetComponent<ThisCard>().id);
            Destroy(currentPlayCard.gameObject.GetComponent<Animator>());
            Destroy(currentPlayCard.gameObject.GetComponent<Hover>());
            Destroy(currentPlayCard.gameObject.GetComponent<CardDrag>());

            environment_change = currentPlayCard.GetComponent<ThisCard>().environment_index;
            life_change = currentPlayCard.GetComponent<ThisCard>().life_expectancy_index;
            social_change = currentPlayCard.GetComponent<ThisCard>().social_stability_index;
            economics_change = currentPlayCard.GetComponent<ThisCard>().economics_index;
            budget_change = currentPlayCard.GetComponent<ThisCard>().cost;

            IndexAnimation(environment_change, life_change, economics_change, social_change);
            ChangeEnviroment(environment_change);


            LTEnvironment += currentPlayCard.GetComponent<ThisCard>().LTEnvironment;
            LTLife += currentPlayCard.GetComponent<ThisCard>().LTLife;
            LTSocial += currentPlayCard.GetComponent<ThisCard>().LTSocial;
            LTeconomics += currentPlayCard.GetComponent<ThisCard>().LTeconomics;
            LTBudget += currentPlayCard.GetComponent<ThisCard>().LTBudget;

           // IndexAnimation(LTEnvironment, LTLife, LTeconomics, LTSocial);
            //ChangeEnviroment(LTEnvironment);
            
            currentPlayCard.tag = "Calculated";

            collectID.Add(currentPlayCard.GetComponent<ThisCard>().id);
            StartCoroutine(MoveCard(currentPlayCard));
            StartCoroutine(DestroyCurrentCard(currentPlayCard));

        int construc = currentPlayCard.GetComponent<ThisCard>().construction;

        CityChange(construc);

        if (environment_change>0 || life_change>0 
            || social_change>0 || economics_change > 0)
        {
            audioManager.PlayIndex_Increase();
        }
    }
    public void ChangeEnviroment(int change)
    {
        vFX.UpdateEnvironment(change);
    }
    public void CityChange(int type)
    {
        switch (type)
        {
            case 0:
                grid.InstantiateConstruction(1, 1, 2);
                grid.InstantiateConstruction(0, 0, 6);
                break;
            case 3:
                grid.InstantiateConstruction(2, 3, 2);
                cc.LookAtPos();
                break;
            case 4:
                grid.InstantiateConstruction(2, 4, 2);
                cc.LookAtPos();
                break;
            case 5:
                grid.InstantiateConstruction(2, 5, 2);
                cc.LookAtPos();
                break;
            case 6:
                grid.InstantiateConstruction(2, 6, 2);
                cc.LookAtPos();
                break;
            case 7:
                grid.InstantiateConstruction(2, 7, 2);
                cc.LookAtPos();
                break;
            case 8:
                grid.InstantiateConstruction(0, 8, 4);
                cc.LookAtPos();
                break;
            case 9:
                grid.InstantiateConstruction(0, 9, 4);
                cc.LookAtPos();
                break;
            default:
                
                break;

        }
      
       
    }
    public GameObject CurrentCard()
    {
       
        if (ZoneArea.transform.GetChild(0).tag != "Calculated")
        {
            return ZoneArea.transform.GetChild(0).gameObject;

        }
        return null;

    }
    IEnumerator DestroyCurrentCard(GameObject currentPlayCard)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(currentPlayCard);
    }

    public int ZoneCount()
    {
        return ZoneArea.transform.childCount;
    }
    IEnumerator MoveCard(GameObject cg)
    {
        float time = 0;
        Vector3 startPosition = cg.transform.position; 
        Vector3 collectionPosition = Collection.transform.position;
        while (time < 0.5f)
        {
            cg.gameObject.transform.position = Vector3.Lerp(startPosition, collectionPosition, time / 0.5f);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = collectionPosition;
        DestroyGoal();
    }

    
  

    public void DestoryHandCard()
    {
        /**Destory**/
        foreach (Transform child in HandArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    
  
    public void DestroyCard()
    {
      
        Destroy(ZoneArea.transform.GetChild(0));
    }

    





}
