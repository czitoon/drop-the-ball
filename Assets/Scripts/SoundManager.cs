using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public void ChangeVolume(float newVolume) {
        SaveGame.save.ChangeVolume(newVolume);
    }

    public void ChangeSoundEnabled(bool checkmark) {
        SaveGame.save.ChangeSoundEnabled(checkmark);
    }
}
