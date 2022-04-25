using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEmoji : MonoBehaviour
{
    public Image emojiUI;
    public Image imageUsed;
    public EmojiManager emojiManager;
    public Transform cameraTransform;
    private Transform tri_head;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    private float maxDisY;
    float emojiDuration;
    float timer;
    bool spriteChanged;
    public Canvas uicanvas;
    // Start is called before the first frame update
    void Start()
    {
        //initialize the interval of sprite changing
        emojiDuration = Random.Range(3f, 5f);
        timer = emojiDuration;
        //initialize the sprite position
        tri_head = transform.GetChild(0).GetChild(0);
        //initialize the sprite of UI component
        imageUsed = Instantiate(emojiUI, uicanvas.transform).GetComponent<Image>();
        imageUsed.transform.position = Camera.main.WorldToScreenPoint(tri_head.position + offset);
        //initialize the UI component position and scale
        maxDisY = Mathf.Max(maxDisY, Mathf.Abs(tri_head.position.y - cameraTransform.position.y));
        float dist = (1 - Mathf.Abs(tri_head.position.y - cameraTransform.position.y) / maxDisY);
        dist = Mathf.Clamp(dist, 0.3f, 1.0f);
        imageUsed.transform.localScale = new Vector3(dist, dist, 0);
        imageUsed.sprite = emojiManager.anger[Random.Range(0, emojiManager.anger.Length)];
        spriteChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEmoji();
        imageUsed.transform.position = Camera.main.WorldToScreenPoint(tri_head.position + offset);
        maxDisY = Mathf.Max(maxDisY, Mathf.Abs(tri_head.position.y - cameraTransform.position.y));
        float dist = (1 - Mathf.Abs(tri_head.position.y - cameraTransform.position.y) / maxDisY);
        dist = Mathf.Clamp(dist,0.3f,1.0f);
        imageUsed.transform.localScale = new Vector3(dist, dist, 0);
    }
    void UpdateEmoji()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (!spriteChanged)
            {
                spriteChanged = true;
                StartCoroutine(EmojiFades());
            }
        }
    }
    void UpdateSprite()
    {
        imageUsed.sprite = emojiManager.GetSprite();
    }
    IEnumerator EmojiFades()
    {
        float time = 0;
        Color target = new Color(1f, 1f, 1f, 0f);
        Color startValue = imageUsed.color;
        while (time < 1f)
        {
            imageUsed.color = Color.Lerp(startValue, target, time);
            time += Time.deltaTime;
            yield return null;
        }
        imageUsed.color = target;
        StartCoroutine(EmojiEmerge());
    }
    IEnumerator EmojiEmerge()
    {
        UpdateSprite();
        float time = 0;
        Color target = new Color(1f, 1f, 1f, 1f);
        Color startValue = imageUsed.color;
        while (time < 1f)
        {
            imageUsed.color = Color.Lerp(startValue, target, time);
            time += Time.deltaTime;
            yield return null;
        }
        imageUsed.color = target;
        timer = emojiDuration;
        spriteChanged = false;
    }
}
