using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour {

    public float failDepth;

    public GameObject ball;
    public GameObject maze;
    public GameObject cameraRig;
    public GameObject goal;
    public EventSystem events;

    public MenuSystem menu;

    private Vector3 ballStartPosition;
    private bool frozen = true;
    private Quaternion rotationOffset = new Quaternion();

    // Use this for initialization
    void Start() {
        // record start positions
        Input.gyro.enabled = true;
        ballStartPosition = ball.transform.position;

        StartCoroutine("WaitThenReset");

        menu.StartGame();
    }

    // Update is called once per frame
    void Update() {
        // First, check user interface
        if (frozen) {
            // Run the menu system (Handled in MenuSystem.cs). Still move the camera, though, for fun.
            RotateCamera();
        }
        else {
            // Rotate maze and/or camera rig
            InGameControl();
        }

        // Second, check loss condition
        CheckBallDropped();

    }


    void InGameControl()
    {
        // Position camera rig

        cameraRig.transform.position = ball.transform.position;

        if (Input.touchCount > 0)
        {
            menu.ShowFrame();
            // In game control of the maze.
            RotateMaze();
        }
        else
        {
            menu.HideFrame();
            // In game control of the camera.
            RotateCamera();
        }
    }   

    void RotateCamera()
    {
        // Set camera rig to gyro input
        Quaternion newRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y + rotationOffset.eulerAngles.y, Input.gyro.attitude.z, Input.gyro.attitude.w);


        cameraRig.transform.rotation = newRotation;
        // Correct gyro input for android orientation (may need updates to fix for iOS).
        cameraRig.transform.Rotate(0f, 0f, 180f, Space.Self);
        cameraRig.transform.Rotate(90f, 0f, 0f, Space.World);
    }

    public void RotateCameraManual(float value)
    {
        rotationOffset.eulerAngles = new Vector3(0f, value - Input.gyro.attitude.eulerAngles.y, 0f);
    }
    public Quaternion getRotationOffset()
    {
        return rotationOffset;
    }

    void RotateMaze()
    {
        // Set maze as child of camera rig, rotate, then restor heirarchy
        maze.transform.SetParent(cameraRig.transform);
        RotateCamera();
        maze.transform.SetParent(transform);
    }

    void CheckBallDropped()
    {
        if (frozen) return;
        if ((maze.transform.position.y - ball.transform.position.y) >= failDepth) {
            menu.LoseGame();
        }
    }

    private IEnumerator WaitThenReset() {
        yield return new WaitForSecondsRealtime(0.01f);
        ResetLevel();
    }
    public void ResetLevel()
    {
        // Put everything back to starting positions. 
        ball.transform.position = ballStartPosition;
        ball.transform.rotation = Quaternion.identity;
        maze.transform.position = Vector3.zero;
        maze.transform.rotation = Quaternion.identity;
        // Rotate maze so "front" faces player.
        ball.transform.SetParent(maze.transform);
        maze.transform.Rotate(0f, cameraRig.transform.eulerAngles.y, 0f, Space.World);
        ball.transform.SetParent(transform);
        cameraRig.transform.position = ball.transform.position;
        // Remove any velocity from the ball.
        ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void FreezeTime() {
        Time.timeScale = 0.0f;
        frozen = true;
    }

    public void RestoreTime() {
        Time.timeScale = 1.0f;
        frozen = false;
    }

    public bool IsFrozen() {
        return frozen;
    }
}
