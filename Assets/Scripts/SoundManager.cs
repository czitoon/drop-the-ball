using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource musicSource;

    public void Start()
    {
        ChangeMusicEnabled(SaveGame.save.GetMusicEnabled());
    }

    public void ChangeVolume(float newVolume) {
        SaveGame.save.ChangeVolume(newVolume);
        musicSource.volume = newVolume;
    }

    public void ChangeSoundEnabled(bool checkmark) {
        SaveGame.save.ChangeSoundEnabled(checkmark);
        Debug.Log("Sound enabled: " + SaveGame.save.GetSoundEnabled());
    }

    public void ChangeMusicEnabled(bool checkmark)
    {
        SaveGame.save.ChangeMusicEnabled(checkmark);
        if (checkmark)
        {
            musicSource.Play();
            // Debug.Log("Music play called");
        }
        else
        {
            musicSource.Stop();
            // Debug.Log("Music stop called");
        }
    }
}
