using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public MenuSystem menu;
    public Collider ball;
    public AudioSource explode;

    void OnTriggerEnter(Collider other) {
        if (other == ball) {
            menu.WinGame();
            explode.Play();
        }
    }
}

// This script is only to handle the goals trigger and call the menu system.