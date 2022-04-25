using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public class AudioBuffer
    {
        public string name;
        public float time;
    }

    static AudioManager s_Instance;
    public static AudioManager Instance => s_Instance;

    public AudioSource BgmAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip DealCard;
    public AudioClip BGMClip;
    public AudioClip RamdomIncident;
    public AudioClip Index_increase;
    public AudioClip CannotPlay;


    public float bufferTime;
    public List<AudioBuffer> audioBuffers = new List<AudioBuffer>();

    void Awake()
    {
        if (s_Instance != null)
        {
            Destroy(this);
            return;
        }

        s_Instance = this;
    }
    private void Start()
    {
        BgmAudioSource.clip = BGMClip;
    }
    private void Update()
    {
        if (audioBuffers.Capacity > 0)
        {
            foreach (AudioBuffer b in audioBuffers)
            {
                b.time -= Time.deltaTime;
            }
        }
    }

    public void PlayDealCard()
    {
        sfxAudioSource.PlayOneShot(DealCard);
    }
    public void PlayRandomIncident()
    {
        sfxAudioSource.PlayOneShot(RamdomIncident);
    }
    public void PlayIndex_Increase()
    {
        sfxAudioSource.PlayOneShot(Index_increase);
    }
    public void PlayCannotPlay()
    {
        sfxAudioSource.PlayOneShot(CannotPlay);
    }

}
