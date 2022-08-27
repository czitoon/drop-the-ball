using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlButton : MonoBehaviour {

    public int targetLVL;
    private Text txt;
    // Use this for initialization
    private void OnEnable() {
        txt = transform.GetChild(0).GetComponent<Text>();

        if (targetLVL <= SaveGame.save.GetHighestLevel() + 1) {
            transform.GetComponent<Button>().interactable = true;
            txt.color = Color.black;
            txt.text = SaveGame.save.GetLevelName(targetLVL) + "\nBest Time: " + SaveGame.save.GetBestTime(targetLVL).ToString("0.00");
        }
        else {
            transform.GetComponent<Button>().interactable = false;
            txt.color = Color.gray;
            txt.text = "Locked";
        }
        
    }
}
