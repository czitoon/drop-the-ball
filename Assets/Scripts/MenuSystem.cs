using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour {

    public GameObject panel;
    public GameObject basic;
    public GameObject ingame;
    public GameObject inpause;
    public GameObject frame;
    public GameObject start;
    public GameObject pause;
    public GameObject win;
    public GameObject lose;
    public GameObject side;
    public GameObject options;
    public GameObject lvlselect;
    public GameObject help;

    public Controller control;

    private GameObject stasis;

    void HideAll() {
        panel.SetActive(false);
        basic.SetActive(false);
        ingame.SetActive(false);
        start.SetActive(false);
        pause.SetActive(false);
        inpause.SetActive(false);
        win.SetActive(false);
        lose.SetActive(false);
        side.SetActive(false);
        options.SetActive(false);
        lvlselect.SetActive(false);
        help.SetActive(false);
        frame.SetActive(false);
    }

    public void RotateCameraSlider(float value)
    {
        control.RotateCameraManual(value);
    }

    public void ShowFrame()
    {
        frame.SetActive(true);
    }

    public void HideFrame()
    {
        frame.SetActive(false);
    }

    void ShowBasic() {
        HideAll();
        panel.SetActive(true);
        basic.SetActive(true);
    }

    void ShowStasis() {
        ShowBasic();
        stasis.SetActive(true);
    }

    void ShowSide() {
        HideAll();
        side.SetActive(true);
    }

    public void ResetLevel() {
        control.ResetLevel();
        StartGame();
    }

    public void PauseGame()
    {
        control.FreezeTime();
        HideAll();
        inpause.SetActive(true);
        panel.SetActive(true);
    }

    public void PauseMenu() {
        control.FreezeTime();
        ShowBasic();
        pause.SetActive(true);
        stasis = pause;
    }

    public void PlayGame() {
        HideAll();
        ingame.SetActive(true);
        StartCoroutine("WaitThenResume");
    }

    private IEnumerator WaitThenResume() {
        yield return new WaitForSecondsRealtime(0.25f);
        control.RestoreTime();
    }

    public void WinGame() {
        control.FreezeTime();
        ShowBasic();
        win.SetActive(true);
        stasis = win;

        SaveGame.save.LevelComplete();
    }

    public void LoseGame() {
        control.FreezeTime();
        ShowBasic();
        lose.SetActive(true);
        stasis = lose;
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void StartGame() {
        control.FreezeTime();
        ShowBasic();
        start.SetActive(true);
        stasis = start;
    }

    public void OptionsMenu() {
        ShowSide();
        options.SetActive(true);
    }

    public void LevelSelect() {
        ShowSide();
        lvlselect.SetActive(true);
    }

    public void HelpMenu() {
        ShowSide();
        help.SetActive(true);
    }

    public void ReturnMenu() {
        ShowStasis();
    }

    public void LoadLevel(int lvl) {
        SceneManager.LoadScene(lvl);
    }

    public void LoadNextLevel() {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SaveGame.save.list.scenesNames.Length) LoadLevel(next);
        else LoadLevel(1);
    }
}
