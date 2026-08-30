using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateSlider : MonoBehaviour
{

    public Controller control;

    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.SetValueWithoutNotify(Input.gyro.attitude.eulerAngles.y + control.getRotationOffset().eulerAngles.y);
    }

    // Update is called once per frame
    void Update()
    {
        slider.SetValueWithoutNotify(Input.gyro.attitude.eulerAngles.y + control.getRotationOffset().eulerAngles.y);
    }
}
