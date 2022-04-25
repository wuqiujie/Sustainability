using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Start : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public GameObject AboutScene;
    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void AboutButton()
    {
        AboutScene.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
             AboutScene.SetActive(false);
           
        }

    }
}
