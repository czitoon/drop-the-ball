using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour
{
    public TextMeshProUGUI logText;
    public GameObject DebugMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Debug.isDebugBuild)
        {
            DebugMenu.SetActive(true);
        }
        else
        {
            DebugMenu.SetActive(false);
        }
    }


    void OnEnable()
    {
        // Subscribe to the log event
        Application.logMessageReceived += HandleLog;

    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Appends new logs to the TMPro component
        logText.text += "\n" + logString;
    }

}
