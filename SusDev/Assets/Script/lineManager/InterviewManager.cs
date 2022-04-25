using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class InterviewManager : MonoBehaviour
{
    public bool called;
    /*public string[] lines;*/
    public GameManager gameManager;
    public IncidentManager incidentManager;
    private Queue<string> ssg;
    public TextAsset socialStabilityGood;
    private Queue<string> ssb;
    public TextAsset socialStabilityBad;
    private Queue<string> leg;
    public TextAsset lifeExpectencyGood;
    private Queue<string> leb;
    public TextAsset lifeExpectencyBad;
    private Queue<string> epg;
    public TextAsset ecnomicProsperityGood;
    private Queue<string> epb;
    public TextAsset ecnomicProsperityBad;
    private Queue<string> eg;
    public TextAsset environmentGood;
    private Queue<string> eb;
    public TextAsset environmentBad;

    public Text goodlines;
    public Text badlines;
    public GameObject interviewCanvas;
    // Start is called before the first frame update
    void Start()
    {
        ssg = new Queue<string>();
        ssb = new Queue<string>();
        leg = new Queue<string>();
        leb = new Queue<string>();
        epg = new Queue<string>();
        epb = new Queue<string>();
        eg = new Queue<string>();
        eb = new Queue<string>();
        PushLinesIntoQuene(socialStabilityGood, ssg);
        PushLinesIntoQuene(socialStabilityBad, ssb);
        PushLinesIntoQuene(lifeExpectencyGood, leg);
        PushLinesIntoQuene(lifeExpectencyBad, leb);
        PushLinesIntoQuene(ecnomicProsperityGood, epg);
        PushLinesIntoQuene(ecnomicProsperityBad, epb);
        PushLinesIntoQuene(environmentGood, eg);
        PushLinesIntoQuene(environmentBad, eb);
    }

    public void InitiateInterview()
    {
        called = false;
        InterviewBegin();
    }

    public void InterviewBegin()
    { 
        interviewCanvas.SetActive(true);
        DisplayTextOnCanvas();
        /*StartCoroutine(EndInterview());*/
    }
    public void EndInterview()
    {
        interviewCanvas.SetActive(false);
        called = true;
    }
/*    IEnumerator EndInterview()
    {
        yield return new WaitForSeconds(5);
        
        //call random incidents
        interviewCanvas.SetActive(false);
        called = true;
        incidentManager.InitiateIncident();
    }*/
    public void DisplayTextOnCanvas()
    {
        if (!called)
        {
            goodlines.text = ChooseGoodLineToDisplay();
            badlines.text = ChooseBadLineToDisplay();
            /*called = true;*/
        }
    }
    public string ChooseGoodLineToDisplay()
    {
        int i = Random.Range(0, 1);
        if(i == 0)
        {
            int max = gameManager.turnController.environment_change;
            max = Mathf.Max(max, gameManager.turnController.environment_change);
            max = Mathf.Max(max, gameManager.turnController.social_change);
            max = Mathf.Max(max, gameManager.turnController.life_change);
            if (max == gameManager.turnController.economics_change)
            {
                if(epg.Count == 0)
                {
                    return "hh";
                }
                return epg.Dequeue();
            }
            if (max == gameManager.turnController.environment_change)
            {
                if (eg.Count == 0)
                {
                    return "hh";
                }
                return eg.Dequeue();
            }
            if (max == gameManager.turnController.social_change)
            {
                if (ssg.Count == 0)
                {
                    return "hh";
                }
                return ssg.Dequeue();
            }
            if (max == gameManager.turnController.life_change)
            {
                if (leg.Count == 0)
                {
                    return "hh";
                }
                return leg.Dequeue();
            }
            return "";
        }
        else
        {
            return "";
        }
    }
    public string ChooseBadLineToDisplay()
    {
        int min = GameManager.total_economics;
        min = Mathf.Min(min, GameManager.total_social_stability);
        min = Mathf.Min(min, GameManager.total_life);
        min = Mathf.Min(min, GameManager.total_environment);
        if(min == GameManager.total_economics)
        {
            if (epb.Count == 0)
            {
                return "hh";
            }
            return epb.Dequeue();
        }
        if(min == GameManager.total_social_stability)
        {
            if (ssb.Count == 0)
            {
                return "hh";
            }
            return ssb.Dequeue();
        }
        if(min == GameManager.total_life)
        {
            if (leb.Count == 0)
            {
                return "hh";
            }
            return leb.Dequeue();
        }
        if(min == GameManager.total_environment)
        {
            if (eb.Count == 0)
            {
                return "hh";
            }
            return eb.Dequeue();
        }
        return "";
    }
    public void PushLinesIntoQuene(TextAsset textAsset, Queue<string> linesQueue)
    {
        StringReader stringreader = new StringReader(textAsset.text);
        string line = stringreader.ReadLine();
        while(line != null)
        {
            linesQueue.Enqueue(line);
            line = stringreader.ReadLine();
        }
    }
}
