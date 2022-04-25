using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiManager : MonoBehaviour
{
    public Sprite[] anger;
    public Sprite[] normal;
    public Sprite[] happy;
    public Sprite GetAnger()
    {
        return anger[Random.Range(0, anger.Length)];
    }
    public Sprite GetNormal()
    {
        return anger[Random.Range(0, normal.Length)];
    }
    public Sprite GethappyHappy()
    {
        return anger[Random.Range(0, happy.Length)];
    }
    public Sprite GetSprite()
    {
        switch(GameManager.total_life)
        {
            case 0:
                return anger[Random.Range(0, happy.Length)];
            case 1:
                if (Random.Range(0.0f, 1.0f) < 0.8f)
                {
                    return anger[Random.Range(0, happy.Length)];
                }
                else
                {
                    return normal[Random.Range(0, happy.Length)];
                }
            case 2:
                if (Random.Range(0.0f, 1.0f) < 0.6f)
                {
                    return anger[Random.Range(0, happy.Length)];
                }
                else
                {
                    return normal[Random.Range(0, happy.Length)];
                }
            case 3:
                if (Random.Range(0.0f, 1.0f) < 0.4f)
                {
                    return anger[Random.Range(0, happy.Length)];
                }
                else
                {
                    return normal[Random.Range(0, happy.Length)];
                }
            case 4:
                if (Random.Range(0.0f, 1.0f) < 0.2f)
                {
                    return anger[Random.Range(0, happy.Length)];
                }
                else
                {
                    return normal[Random.Range(0, happy.Length)];
                }
            case 5:
                return normal[Random.Range(0, happy.Length)];
            case 6:
                if (Random.Range(0.0f, 1.0f) < 0.3f)
                {
                    return happy[Random.Range(0, happy.Length)];
                }
                else
                {
                    return normal[Random.Range(0, happy.Length)];
                }
            case 7:
                if (Random.Range(0.0f, 1.0f) < 0.6f)
                {
                    return happy[Random.Range(0, happy.Length)];
                }
                else
                {
                    return normal[Random.Range(0, happy.Length)];
                }
            case 8:
                if (Random.Range(0.0f, 1.0f) < 0.8f)
                {
                    return happy[Random.Range(0, happy.Length)];
                }
                else
                {
                    return normal[Random.Range(0, happy.Length)];
                }
            case 9:
                if (Random.Range(0.0f, 1.0f) < 0.9f)
                {
                    return happy[Random.Range(0, happy.Length)];
                }
                else
                {
                    return normal[Random.Range(0, happy.Length)];
                }
            case 10:
                return happy[Random.Range(0, happy.Length)];
            default:
                return happy[Random.Range(0, happy.Length)];
        }
    }
}
