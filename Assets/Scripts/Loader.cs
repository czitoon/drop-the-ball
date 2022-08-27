using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
	void Update () {
        SaveGame.save.ReadSave();
        SceneManager.LoadScene(SaveGame.save.NextLevel());
    }
}
