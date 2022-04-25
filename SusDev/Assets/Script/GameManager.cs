using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    /**UI Bar**/
    public GameObject EnvBar;
    public GameObject LifeBar;
    public GameObject StableBar;
    public GameObject EconomyBar;
    public GameObject TurnBar;
    public GameObject BudgetBar;
   
    /**GameArea**/
    public GameObject HandArea;
    public GameObject IndexPanel;
    public GameObject IndicatorPanel;
    public GameObject CollectPosition;
    public GameObject gameStartButton;
    public GameObject playCardButton;
    public GameObject hoverIndexpanel;

    /**end **/
    public GameObject endCanvas;
    public GameObject endGoalPanel;
    public GameObject endIndexPanel;
    public GameObject EndEnvBar;
    public GameObject EndLifeBar;
    public GameObject EndStableBar;
    public GameObject EndEconomyBar;
   
    public Text GetGoalNum;


    /** Index **/
    [SerializeField]
    public static int total_environment;
    public static int total_life;
    public static int total_economics;
    public static int total_social_stability;

    /**Turn Info**/
    public int turnNum = 0;
    public static int budgetNum = 0;
    public Text turnText;
    public TurnController turnController;

    /**incident and interview**/
    public IncidentManager incidentManager;
    public InterviewManager interviewManager;
    public bool interview_called;
    public bool incident_called;
    public GameObject LCitizen;
    public GameObject RCitizen;

    /**Tutorial**/
    public GameObject TutorialArea;
    public TutorialController tutorial;
    public GameObject tutorialButton;
    public GameObject GoalPanel;

    public GameState state;

    public bool[] goalCollect;
    public List<int> collectID;

    //public GameObject collectionPanel;
    private bool endGame=false;

    public AudioManager audioManager;

    public GameObject wasd;
    public GameObject mouse;

    public enum GameState
    {
        GameStart,
        Tutorial,
        TurnStart,
        PlayCard,
        JudgeBudget,
        Calculate,
        CollectCard,
        interview,
        incident,
        TurnEnd,
        GameEnd
    }



    void Start()
    {
        state = GameState.Tutorial;
        HandArea = GameObject.Find("HandArea");
        gameStartButton.SetActive(false);
        total_environment = 1;
        total_life = 1;
        total_economics = 1;
        total_social_stability = 2;
        turnNum = 0;
        collectID = new List<int>();
    }

    void Update()
    {
      // Debug.Log("state: " + state);
    //    Debug.Log("Turn:" + turnNum);
        turnText.text =  turnNum +"/7";

        if (state == GameState.GameStart)
        {
            Game_Start();
            gameStartButton.SetActive(true);
        }
        if(state == GameState.Tutorial)
        {
            Tutorial();
        }
        if (state == GameState.TurnStart)
        {
            Start_Turn();
        }

        if (state == GameState.PlayCard)
        {
            Play_Card();
         
        }

        if (state == GameState.JudgeBudget)
        {
            JudgeBudgetCard();
        }
        
        if (state == GameState.Calculate)
        {
      
            CalculateCard();
        }
        
     
        if (state == GameState.CollectCard)
        {
            Collect_Card();
        }

        if (state == GameState.interview && !interview_called)
        {
            Interview();
            interview_called = true;
        }

        if (state == GameState.incident && !incident_called)
        {
            Incident();
            incident_called = true;
        }

        if (state == GameState.TurnEnd)
        {
            Turn_End();
        }

        if (turnNum == 1)
        {
            Wasd();
        }
        if (turnNum > 1)
        {
            wasd.SetActive(false);
            mouse.SetActive(false);
        }

        UI_Update();
  
        budgetNum = Math.Min(5, budgetNum);
        total_environment = Math.Max(0, total_environment);
        total_life = Math.Max(0, total_life);
        total_economics = Math.Max(0, total_economics);
        total_social_stability = Math.Max(0, total_social_stability);

        //Debug.Log("total_economics"+total_economics);
        //Debug.Log("total_environment" + total_environment);
        //Debug.Log("total_life" + total_life);
        //Debug.Log("total_social_stability" + total_social_stability);

    }

 
    public void Game_Start()
    {

        goalCollect = new bool[17];
        for (int i = 0; i < 17; i++)
        {
            goalCollect[i] = false;
        }


    }
    public void Game_Start_Button()
    {
       
        gameStartButton.SetActive(false);
        state = GameState.Tutorial;

    }
public void Tutorial()
    {
       // turnText.text = "Tutorial Time";
        GoalPanel.SetActive(false);
        IndexPanel.SetActive(false);
        IndicatorPanel.SetActive(false);
        CollectPosition.SetActive(false);
        HandArea.SetActive(false);

        for(int i =4; i < 21; i++)
        {
            hoverIndexpanel.transform.GetChild(i).gameObject.SetActive(false);
        }

        TutorialArea.SetActive(true);
        tutorialButton.SetActive(false);
        if (tutorial.index >= 22)
        {
            tutorialButton.SetActive(true);
        }
    }

    public void Tutorial_Button()
    {
        state = GameState.TurnStart;
        
        turnController.ZoneArea.SetActive(true);
        turnController.HandArea.SetActive(true);
        wasd.SetActive(true);
        mouse.SetActive(true);
    }
    public void Wasd()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            wasd.SetActive(false);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f || Input.GetAxis("Mouse ScrollWheel") < -0.0f)
        {
            mouse.SetActive(false);
        }
    }

    public void Start_Turn()
    {
        GoalPanel.SetActive(true);
        IndexPanel.SetActive(true);
        IndicatorPanel.SetActive(true);
        CollectPosition.SetActive(true);
        HandArea.SetActive(true);
        TutorialArea.SetActive(false);
        for (int i = 4; i < 21; i++)
        {
            hoverIndexpanel.transform.GetChild(i).gameObject.SetActive(true);
        }


        LT_Update();

        turnNum++;
        TurnBar.GetComponent<Bar>().env.ChangeEnv(turnNum);
       
        interview_called = false;
        incident_called = false;
        incidentManager.called = false;

        turnController.StartTurn();
        state = GameState.PlayCard;
    }

    
    public void Play_Card()
    {


        if (turnController.ZoneCount() > 0)
        {
            state = GameState.JudgeBudget;
        }
        else
        {
            state = GameState.PlayCard;
        }
   
       
    }

    public void JudgeBudgetCard()
    {
      
        if(turnController.ZoneCount() >1 )
        {
            if (budgetNum >= turnController.CurrentCard().GetComponent<ThisCard>().cost)
            {
                state = GameState.Calculate;
            }
            else
            {
                audioManager.PlayCannotPlay();
                HandArea = GameObject.Find("HandArea");
                turnController.ZoneArea.transform.GetChild(0).gameObject.transform.SetParent(HandArea.transform);
                state = GameState.PlayCard;
            }

        }
        else
        {
            state = GameState.PlayCard;
        }
    }
    public void CalculateCard()
    {
        playCardButton.SetActive(true);

        turnController.ShowGoal();
        for (int i = 0; i < 17; i++)
        {
            if (turnController.goalCollect[i])
            {
                goalCollect[i] = turnController.goalCollect[i];
            }

        }

        collectID = turnController.collectID;
        turnController.CalculateCard();
        UI_Update();
        Data_Update();
        //turnController.CityChange();
       
        state = GameState.PlayCard;
        
    }
    
  
    public void Play_Card_Button()
    {
        state = GameState.CollectCard;
    }

    
    
    public void Collect_Card()
    {
   
        playCardButton.SetActive(false);
        turnController.DestoryHandCard();
        state = GameState.interview;
    }
   


    public void Interview()
    {
        LCitizen.gameObject.SetActive(true);
        RCitizen.gameObject.SetActive(true);

        int Lindex = UnityEngine.Random.Range(0, 3);
        int Rindex = UnityEngine.Random.Range(0, 3);
        LCitizen.transform.GetChild(Lindex).gameObject.SetActive(true);
        RCitizen.transform.GetChild(Rindex).gameObject.SetActive(true);
       
      
        interviewManager.InitiateInterview();
    }
    public void EndInterviewButton()
    {
        interviewManager.EndInterview();

        for (int i = 0; i < 4; i++)
        {
            LCitizen.transform.GetChild(i).gameObject.SetActive(false);
            RCitizen.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (turnNum == 7)
        {
            Game_End();
        }
        else
        {
            state = GameState.incident;
        }
    }
    public void Incident()
    {
      
        incidentManager.InitiateIncident();
    }
    
    public void EndIncidentButton()
    {
        incidentManager.EndIncident();
        state = GameState.TurnEnd;
    }

    public void Turn_End()
    {
         state = GameState.TurnStart;
    }

    public void Game_End()
    {
        for (int i = 0; i < 21; i++)
        {
            hoverIndexpanel.transform.GetChild(i).gameObject.SetActive(false);
        }

        endGame = true;
        endCanvas.SetActive(true);
        endGoalPanel = GameObject.Find("EndGoalPanel");


        int gNum=0;
        goalCollect[16] = true;


        for (int i = 0; i < 17; i++)  
        {
            if (goalCollect[i] == true)
            {
                gNum++;
                endGoalPanel.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        int[] total_index = new int[4];
        total_index[0] = total_environment;
        total_index[1] = total_life;
        total_index[2] = total_economics;
        total_index[3] = total_social_stability;
        int maxValue = total_index.Max();
        int maxIndex = total_index.ToList().IndexOf(maxValue);
        endIndexPanel = GameObject.Find("EndIndexPanel");
        endIndexPanel.transform.GetChild(maxIndex).gameObject.SetActive(true);
       
        EndEnvBar.GetComponent<Bar>().env.envAmount = total_environment;
        EndLifeBar.GetComponent<Bar>().env.envAmount = total_life;
        EndStableBar.GetComponent<Bar>().env.envAmount = total_social_stability;
        EndEconomyBar.GetComponent<Bar>().env.envAmount = total_economics;

        GetGoalNum.text = gNum + " ";
        
    }

    public void UI_Update()
    {
        EnvBar.GetComponent<Bar>().env.envAmount = total_environment;
        LifeBar.GetComponent<Bar>().env.envAmount = total_life;
        StableBar.GetComponent<Bar>().env.envAmount = total_social_stability;
        EconomyBar.GetComponent<Bar>().env.envAmount = total_economics;

        BudgetBar.GetComponent<Bar>().env.envAmount = budgetNum;
        TurnBar.GetComponent<Bar>().env.envAmount = turnNum;
    }
    public void Data_Update()
    {
        total_economics += turnController.economics_change;
        total_environment += turnController.environment_change;
        total_life += turnController.life_change;
        total_social_stability += turnController.social_change;

        budgetNum -= turnController.budget_change;
    }

    public void LT_Update()
    {
        budgetNum += 3 + turnController.LTBudget;
        total_economics += turnController.LTeconomics;
        total_environment += turnController.LTEnvironment;
        total_life += turnController.LTLife;
        total_social_stability += turnController.LTSocial;

    }


}
