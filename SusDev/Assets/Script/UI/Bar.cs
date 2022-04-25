using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Bar : MonoBehaviour
{
    public Env env;
    private Image barImage;
    public int ENV_MAX = 10;
    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();
        env = new Env();
    }

    public void ChangeEnv(int amount)
    {
        env.ChangeEnv(amount);
    }

    private void Update()
    {
        barImage.fillAmount = env.GetEnvNormalized(ENV_MAX);
        //print(env.GetEnvNormalized());
    }
}

public class Env
{
  //  public const int ENV_MAX = 10;
    public float envAmount;



    public void ChangeEnv(int amount)
    {
            envAmount += amount;
    }



    public float GetEnvNormalized(int ENV_MAX)
    {
        return envAmount / ENV_MAX;
    }
}