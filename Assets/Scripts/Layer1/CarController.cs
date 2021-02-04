using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private GameObject gameManager;

    // Assigned in Unity.
    public Rigidbody playerRB;
    public WheelCollider frontDriverWheel, frontPassengerWheel, rearDriverWheel, rearPassengerWheel;
    public Transform frontDriverTransform, frontPassengerTransform, rearDriverTransform, rearPassengerTransform;
    public TextMeshProUGUI speedometer;
    public Material brakeLights;
    public Material reverseLights;
    public Slider gearStick;

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private float brakes;
    private float inverseZ;
    private float lastClick = 0f;
    private float doubleClickInterval = 0.5f;

    private int directionSwitch = 1;

    private string clickType;
    private string gear;

    private bool acceleratorPressed;
    private bool brakePressed;
    private bool usingKeyboard;

    // Assigned in Unity.
    public float maxSteerAngle;
    public float motorForce;
    public float speed;


    private void Start()
    {
        gear = "Drive";
        gameManager = GameObject.Find("GameManager");
    }


    // Allows the user to control the car while a level is loaded.
    public void FixedUpdate()
    {
        if (gameManager.GetComponent<GameManager>().playing)
        {
            GetInput(directionSwitch);
            Steer();

            if (Input.GetKey(KeyCode.Space) || brakePressed)
            {
                if (clickType == "SingleClick")
                {
                    Accelerate(-2);
                }
                else
                {
                    Brake();
                }
            }
            else if (acceleratorPressed)
            {
                Accelerate(1);
            }
            else
            {
                directionSwitch = 1;
                frontDriverWheel.brakeTorque = 0;
                frontPassengerWheel.brakeTorque = 0;
            }

            UpdateWheelPoses();
            UpdateSpeed();
        }
    }

    public void ChangeGear()
    {
        if (gearStick.value == 0)
        {
            gear = "Drive";
            reverseLights.SetColor("_EmissionColor", Color.black);
        }
        else if (gearStick.value == 1)
        {
            gear = "Reverse";
            reverseLights.SetColor("_EmissionColor", Color.red * 0.5f);
        }
    }


    /* Gets an input using Unity's built in controls for arrow keys and wasd the
       output value is any float between -1 and 1 based on how long the keys are held. */
    private void GetInput(int directionSwitch)
    {
        //horizontalInput = SimpleInput.GetAxis("Horizontal") * directionSwitch;
        horizontalInput = SimpleInput.GetAxis("Horizontal");

        if (SimpleInput.GetKey(KeyCode.W) || SimpleInput.GetKey(KeyCode.S))
        {
            usingKeyboard = true;
            verticalInput = SimpleInput.GetAxis("Vertical");
            Accelerate(verticalInput);
        }
        else if (usingKeyboard)
        {
            frontDriverWheel.motorTorque = 0;
            frontPassengerWheel.motorTorque = 0;
        }
    }


    public void AcceleratorPressed()
    {
        acceleratorPressed = true;
        usingKeyboard = false;
    }


    public void BrakePressed()
    {
        brakePressed = true;
        usingKeyboard = false;
        brakeLights.SetColor("_EmissionColor", Color.red * 0.25f);

        if ((lastClick + doubleClickInterval) > Time.time)
        {
            clickType = "DoubleClick";
        }
        else
        {
            lastClick = Time.time;
            clickType = "SingleClick";
        }

        verticalInput = -1;
    }


    public void SpeedButtonReleased()
    {
        acceleratorPressed = false;
        brakePressed = false;

        brakeLights.SetColor("_EmissionColor", Color.black);

        frontDriverWheel.motorTorque = 0;
        frontPassengerWheel.motorTorque = 0;
    }


    // Changes the steering angle to a max of 40 degrees based on user input.
    private void Steer()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            steeringAngle = maxSteerAngle / 10 * horizontalInput;
        }
        else
        {
            steeringAngle = maxSteerAngle * horizontalInput;
        }

        frontDriverWheel.steerAngle = steeringAngle;
        frontPassengerWheel.steerAngle = steeringAngle;
    }


    // Slams the brakes when space is pressed, as opposed to using s to slow normally.
    private void Brake()
    {
        directionSwitch = -1;
        brakes = 20000;
        frontDriverWheel.brakeTorque = brakes;
        frontPassengerWheel.brakeTorque = brakes;
    }


    // Changes the players acceleration to a max motor force of 50 based on user input.
    private void Accelerate(float acceleration)
    {
        directionSwitch = 1;

        inverseZ = playerRB.transform.InverseTransformDirection(playerRB.velocity).z;
        frontDriverWheel.brakeTorque = 0;
        frontPassengerWheel.brakeTorque = 0;

        if (gear == "Drive")
        {
            frontDriverWheel.motorTorque = motorForce * acceleration;

            if (frontDriverWheel.motorTorque < 0 && inverseZ < 0 && usingKeyboard == false)
            {
                frontDriverWheel.motorTorque = 0;
            }

            frontPassengerWheel.motorTorque = motorForce * acceleration;

            if (frontPassengerWheel.motorTorque < 0 && inverseZ < 0 && usingKeyboard == false)
            {
                frontPassengerWheel.motorTorque = 0;
            }
        }
        else if (gear == "Reverse")
        {
            frontDriverWheel.motorTorque = (motorForce * acceleration * -1) / 2;

            if (frontDriverWheel.motorTorque > 0 && inverseZ > 0)
            {
                frontDriverWheel.motorTorque = 0;
            }

            frontPassengerWheel.motorTorque = (motorForce * acceleration * -1) / 2;

            if (frontPassengerWheel.motorTorque > 0 && inverseZ > 0)
            {
                frontPassengerWheel.motorTorque = 0;
            }
        }

        if (inverseZ < - 0.1 && usingKeyboard)
        {
            reverseLights.SetColor("_EmissionColor", Color.red * 0.5f);
        }
        else if (usingKeyboard)
        {
            reverseLights.SetColor("_EmissionColor", Color.black);
        }
    }


    // Changes the world position of each wheel.
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverWheel, frontDriverTransform);
        UpdateWheelPose(frontPassengerWheel, frontPassengerTransform);
        UpdateWheelPose(rearDriverWheel, rearDriverTransform);
        UpdateWheelPose(rearPassengerWheel, rearPassengerTransform);
    }

    // Updates the world position of a wheel to mimic the current steering angle.
    private void UpdateWheelPose(WheelCollider collider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion quat;

        collider.GetWorldPose(out pos, out quat);

        wheelTransform.position = pos;
        wheelTransform.rotation = quat;
    }


    // Updates the speedometer UI with kph.
    private void UpdateSpeed()
    {
        speed = Mathf.RoundToInt(playerRB.velocity.magnitude * 3.6f);
        speedometer.text = "Speed: " + speed + " kph";
    }
}
