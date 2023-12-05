using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour {

    public static SaveGame save;

    private SaveData data;
    private string savePath;

    public ScenesList list;

    // make the save object into a singleton
    private void Awake() {
        if (save == null) {
            DontDestroyOnLoad(gameObject);
            save = this;
            savePath = Application.persistentDataPath + "/save.dat";
        }
        else if (save != this) {
            Destroy(gameObject);
            return;
        }

        
    }

    // Read save automatically.
    private void OnEnable() {
        ReadSave();
        ChangeVolume(data.volume);
        ChangeSoundEnabled(data.soundEnabled);
    }

    // Write save automatically.
    private void OnDisable() {
        WriteSave();
    }

    public void ReadSave() {
        if (list == null ) list = Resources.Load<ScenesList>("ScenesList");

        if (File.Exists(savePath)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            data = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        else {
            data = new SaveData();
            WriteSave();
        }
    }

    public void WriteSave() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);

        bf.Serialize(file, data);
        file.Close();
    }
    
    public int NextLevel() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            if (data.lvlsWon == list.scenesNames.Length - 1) return data.lvlsWon;
            else return data.lvlsWon + 1;
        }
        else if (SceneManager.GetActiveScene().buildIndex < data.lvlsWon) {
            return SceneManager.GetActiveScene().buildIndex + 1;
        }
        else if (data.lvlsWon < (list.scenesNames.Length - 1)) {
            return data.lvlsWon + 1;
        }
        else return 0;
    }
    
    public void ChangeVolume(float newVolume) {
        if ((newVolume <= 1.0f) && (newVolume >= 0.0f)) {
            data.volume = newVolume;
            if (data.soundEnabled) AudioListener.volume = data.volume;
        }
    }

    public void ChangeSoundEnabled(bool checkmark) {
        data.soundEnabled = checkmark;
        if (data.soundEnabled) AudioListener.volume = data.volume;
        else AudioListener.volume = 0f;
    }

    public void LevelComplete(float newTime) {
        if (data.times[SceneManager.GetActiveScene().buildIndex] > newTime) {
            data.times[SceneManager.GetActiveScene().buildIndex] = newTime;
        }
        if (SceneManager.GetActiveScene().buildIndex > data.lvlsWon) {
            data.lvlsWon++;
            data.times[SceneManager.GetActiveScene().buildIndex] = newTime;
        }
    }

    public int GetHighestLevel() {
        return data.lvlsWon;
    }

    public string GetLevelName(int num) {
        return list.scenesNames[num];
    }

    public float GetBestTime(int num) {
        return data.times[num];
    }

    public float GetVolume() {
        return data.volume;
    }

    public bool GetSoundEnabled() {
        return data.soundEnabled;
    }
}

[System.Serializable]
class SaveData {
    public bool soundEnabled = true;
    public float volume = 1f;

    public int lvlsWon = 0;
    public float[] times = new float[9];
}