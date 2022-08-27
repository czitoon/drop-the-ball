using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSlider : MonoBehaviour {

    public Slider slide;

	// Use this for initialization
	void Start () {
        slide.value = SaveGame.save.GetVolume();
	}
	
}
