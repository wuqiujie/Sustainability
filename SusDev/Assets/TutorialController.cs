using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Sprite[] tutorial_sprites;


    public Image image;
    public int index = 0;
    public int people_index = 0;
    public GameObject button;
    public GameObject[] people;
    public GameObject[] character;
    public GameObject tutorialCanvas;
    public GameObject SampleCard;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite  = tutorial_sprites[0];
        
    }

    void Update()
    {
        //Debug.Log("index: " + index);
        if (Input.GetMouseButtonDown(0) && index<22)
        {
            foreach (Transform child in tutorialCanvas.transform)
            {
                if (child.tag == "appear")
                {
                    child.gameObject.SetActive(false);
                }
            }
            if (index < tutorial_sprites.Length)
            {
                image.sprite = tutorial_sprites[++index];
            }
            if((index>=0 && index <=13) || (index >= 16 && index<=22)){
                var ch = Instantiate(people[people_index], 
                    character[people_index].transform.position, 
                    people[people_index].transform.rotation);
                ch.transform.SetParent( tutorialCanvas.gameObject.transform);
                ch.tag = "appear";
                people_index++;
            }
           

        }
        if(index == 11)
        {
            SampleCard.SetActive(true);
        }
        if(index > 11)
        {
            SampleCard.SetActive(false);
        }
        if(index >= 22)
        {
            button.SetActive(true);
        }

    }


}
