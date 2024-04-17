using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public AudioSource bgSound;
    public AudioSource SFXsound;
    public static float BGMvolume = 0.5f;
    public static SoundManager instance;
    public void Start()
    {
    
        bgSound.volume = BGMvolume;
        slider.value = bgSound.volume;
        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }*/
        
    }

    private void Update()
    {
        bgSound.volume = slider.value;
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.Play();
    }

    public void BgSoundStop()
    {
        bgSound.Stop();
    }

    public void SFX(AudioClip clip)
    {
        SFXsound.clip = clip;
        SFXsound.Play();
    }
    
    public void Apply()
    {
        BGMvolume = bgSound.volume;
    }

}
