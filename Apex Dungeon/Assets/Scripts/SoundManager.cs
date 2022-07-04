using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public static SoundManager sm = null;
    public AudioClip titleMusic;
    public AudioClip[] dungeonMusic;
    public AudioClip[] hitSounds;
    public AudioClip[] pickupSounds;
    public AudioClip levelUpSound;
    public AudioClip goldSound;
    public AudioClip buttonSound;

    private AudioSource audioSource;

    private void Awake(){
        if(sm == null)
        {
            sm = this;
        }else if(sm != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayTitleMusic(){
        audioSource.volume = 0.3f;
        audioSource.clip = titleMusic;
        audioSource.Play();
    }

    public void PlayMenuSound(){
        audioSource.PlayOneShot(buttonSound, 3f);
    }

    public void PlayPickupSound(){
        audioSource.PlayOneShot(randomClip(pickupSounds), 3f);
    }

    public void PlayDungeonMusic(){
        audioSource.volume = 0.3f;
        audioSource.clip = randomClip(dungeonMusic);
        audioSource.Play();
    }

    public void StopMusic(){
        audioSource.Stop();
    }

    public void PlayHitSound(){
        audioSource.PlayOneShot(randomClip(hitSounds), 2f);
    }

    public AudioClip randomClip(AudioClip[] clips){
        int i = Random.Range(0, clips.Length);
        return clips[i];
    }
    
}
