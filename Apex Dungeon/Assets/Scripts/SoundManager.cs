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

    public void UpdateVolume()
    {
        audioSource.volume = Data.musicVolume;
    }

    public void UpdatePlaying()
    {
        if (!Data.music && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        if(Data.music && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PlayTitleMusic(){
        if (!Data.music) return;
        if (audioSource.clip == titleMusic && audioSource.isPlaying) return;
        audioSource.volume = Data.musicVolume;
        audioSource.clip = titleMusic;
        audioSource.Play();
    }

    public void PlayMenuSound(){
        if (!Data.sound) return;
        audioSource.PlayOneShot(buttonSound, Data.soundVolume * 3f);
    }

    public void PlayPickupSound(){
        if (!Data.sound) return;
        audioSource.PlayOneShot(randomClip(pickupSounds), Data.soundVolume * 3f);
    }

    public void PlayDungeonMusic(){
        if (!Data.music) return;
        audioSource.volume = Data.musicVolume;
        audioSource.clip = randomClip(dungeonMusic);
        audioSource.Play();
    }

    public void StopMusic(){
        audioSource.Stop();
    }

    public void PlayHitSound(){
        if (!Data.sound) return;
        audioSource.PlayOneShot(randomClip(hitSounds), Data.soundVolume * 3f);
    }

    public AudioClip randomClip(AudioClip[] clips){
        int i = Random.Range(0, clips.Length);
        return clips[i];
    }
    
}
