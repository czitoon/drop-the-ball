using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetToggle : MonoBehaviour {

    public Toggle tog;

    // Use this for initialization
    void Start() {
        tog.isOn = SaveGame.save.GetSoundEnabled();
    }

}
