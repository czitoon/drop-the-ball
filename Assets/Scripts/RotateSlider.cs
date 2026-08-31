using UnityEngine;
using UnityEngine.UI;

public class RotateSlider : MonoBehaviour
{

    public Controller control;

    public Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Only update the slider display if the player is not dragging it
        if (Input.touchCount < 1)
        {
            Quaternion rotangle = Input.gyro.attitude;
            Quaternion rotationChange = Quaternion.Euler(0, 0, 180);
            rotangle = rotangle * rotationChange;
            rotationChange = Quaternion.Euler(90, control.getRotationOffset(), 0);
            rotangle = rotationChange * rotangle;
            slider.SetValueWithoutNotify(rotangle.eulerAngles.y);
        }
    }
}
