using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BobControl : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10;
    [SerializeField] private float _movementSpeed = 2;
    [SerializeField] private Animator _animator;
    //[SerializeField] private int _speedMulti = 2;
    [SerializeField] private int _movementID = Animator.StringToHash("MovementSpeed");

    [SerializeField] private UnityEngine.UI.Slider _blendSlider;

    private JoystickControl joystickControl;

    void Start()
    {
        joystickControl = GameObject.FindGameObjectWithTag("Joystick").GetComponent<JoystickControl>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    void Movement()
    {
        float horizontalInput = joystickControl.Horizontal();
        float verticalInput = joystickControl.Vertical();

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement.Normalize();

        transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, Time.deltaTime * _movementSpeed);
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, verticalInput));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

        float calculatedSpeed = Mathf.Clamp(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput), 0, 1);

        if (_blendSlider != null && horizontalInput != 0 || verticalInput != 0)
        {
            float sliderValue = _blendSlider.value;
            calculatedSpeed = sliderValue;
        }
        else
        {
            calculatedSpeed = Mathf.Clamp(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput), 0, 1);
        }

        _animator.SetFloat(_movementID, calculatedSpeed);
    }
}
