using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance { get { return instance; } }




    public AudioSource flapSound;
    public AudioSource soundBGM;

    public SoundType[] soundTypes;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        BgMusic(Sounds.BGM);
    }




    public void BgMusic(Sounds sound)
    {
        AudioClip clip = GetSoundClip(sound);

        if (clip != null)
        {
            soundBGM.clip = clip;
            soundBGM.Play();
        }
        else
        {
            Debug.LogError(" Clip not found");
        }
    }


    public void Play(Sounds sound)
    {
        AudioClip clip = GetSoundClip(sound);

        if (clip != null)
        {
            flapSound.clip = clip;
            flapSound.Play();
        }
        else
        {
            Debug.LogError(" Clip not found");
        }
    }


    private AudioClip GetSoundClip(Sounds sounds)
    {
        SoundType soundType = Array.Find(soundTypes, type => type._typeOfSound == sounds);
        if (soundType != null)
        {
            int random = UnityEngine.Random.Range(0, soundType.audioClip.Count-1);
            return soundType.audioClip[random];
        }
        else
        {
            Debug.LogError("SoundType not found for sound: " + sounds);
            return null;
        }

    }


    [Serializable]
    public class SoundType
    {
        public Sounds _typeOfSound;
        public List<AudioClip> audioClip;
    }


    public enum Sounds
    {
        ButtonClick,
        BranchSound,
        FoodtakeSound,
        PlayerDeath,
        BGM,
        FlapSound,
        CloudBurst,
        GoingOutsideCamera,
    }
}
