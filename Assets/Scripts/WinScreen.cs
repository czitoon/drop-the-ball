using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {
    public Text txt;
	// Update is called once per frame
	void Update () {
        txt.text = "Time to Beat: " + Timer.timer.GetTime().ToString("0.00");
	}
}
