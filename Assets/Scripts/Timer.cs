using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float start;
    private float elapsed;
    private Text display;

    public static Timer timer;

	// Use this for initialization
	void Start () {
        start = Time.time;
        display = GetComponent<Text>();
        timer = this;
    }

    // Update is called once per frame
    void Update () {
        elapsed = Time.time - start;
        display.text = elapsed.ToString("0.00");
	}

    public float GetTime() {
        return elapsed;
    }

    public void Restart() {
        start = Time.time;
    }
}
