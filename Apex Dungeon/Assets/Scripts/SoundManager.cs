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
    public AudioClip[] impactSounds;
    public AudioClip[] deathSounds;
    public AudioClip[] approvalSounds;
    public AudioClip[] stepSounds;
    public AudioClip[] coinSounds;
    public AudioClip[] stickSounds;
    public AudioClip[] monsterSounds;
    public AudioClip[] criticalSounds;

    public AudioClip levelUpSound;
    public AudioClip buttonSound;
    public AudioClip deathSound;
    public AudioClip bookOpen;
    public AudioClip bookClose;
    public AudioClip pageTurn;
    public AudioClip equipSound;
    public AudioClip unequipSound;
    public AudioClip potionSound;
    public AudioClip magicSound;
    public AudioClip mapSound;

    private AudioSource musicSource;
    private AudioSource soundSource;

    private void Awake(){
        if(sm == null)
        {
            sm = this;
        }else if(sm != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        musicSource = GetComponent<AudioSource>();
        soundSource = transform.GetChild(0).gameObject.GetComponent<AudioSource>();

        musicSource.volume = Data.musicVolume;
        soundSource.volume = Data.soundVolume;
    }

    public void UpdateMusicVolume()
    {
        musicSource.volume = Data.musicVolume * 0.05f;
    }

    public void UpdateSoundVolume()
    {
        soundSource.volume = Data.soundVolume;
    }

    public void UpdatePlaying()
    {
        Debug.Log("Update playing->" + musicSource.isPlaying);
        if (!Data.music && musicSource.isPlaying)
        {
            StopMusic();
        }
        if(Data.music && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void PlayTitleMusic(){
        if (!Data.music) return;
        if (musicSource.clip == titleMusic && musicSource.isPlaying) return;
        musicSource.volume = Data.musicVolume * 0.05f;
        musicSource.clip = titleMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayDungeonMusic()
    {
        Debug.Log("DungeonMusic");
        if (!Data.music) return;
        musicSource.volume = Data.musicVolume * 0.05f;
        musicSource.clip = randomClip(dungeonMusic);
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayMenuSound(){
        if (!Data.sound) return;
        soundSource.PlayOneShot(buttonSound);
    }

    public void PlayLevelUpSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(levelUpSound);
    }

    public void PlayDeathSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(deathSound, 0.3f);
    }

    public void PlayPickupSound(){
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(pickupSounds));
    }

    public void PlayHitSound(){
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(hitSounds), 0.3f);
    }

    public void PlayBookOpen()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(bookOpen);
    }

    public void PlayBookClose()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(bookClose);
    }

    public void PlayPageTurn()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(pageTurn);
    }

    public void PlayEquipSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(equipSound);
    }

    public void PlayUnequipSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(unequipSound);
    }

    public void PlayPotionSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(potionSound);
    }

    public void PlayMagicSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(magicSound);
    }

    public void PlayCriticalSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(criticalSounds), 0.5f);
    }

    public void PlayStepSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(stepSounds), 0.2f);
    }

    public void PlayCoinSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(coinSounds), 0.2f);
    }

    public void PlayApprovalSound()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(approvalSounds));
    }

    public void PlayDeathSound2()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(deathSounds));
    }

    public void PlayImpactSounds()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(impactSounds), 0.3f);
    }

    public void PlayStickSounds()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(stickSounds), 0.3f);
    }

    public void PlayMonsterSounds()
    {
        if (!Data.sound) return;
        soundSource.PlayOneShot(randomClip(monsterSounds), 0.3f);
    }

    public AudioClip randomClip(AudioClip[] clips){
        int i = Random.Range(0, clips.Length);
        return clips[i];
    }
    
}
