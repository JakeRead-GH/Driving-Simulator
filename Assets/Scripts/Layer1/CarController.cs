using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject ruleChecker;

    // Assigned in Unity.
    public Rigidbody playerRB;
    public WheelCollider frontDriverWheel, frontPassengerWheel, rearDriverWheel, rearPassengerWheel;
    public Transform frontDriverTransform, frontPassengerTransform, rearDriverTransform, rearPassengerTransform;
    public TextMeshProUGUI speedometer;
    public Material brakeLights;
    public Material reverseLights;
    public Material leftIndicatorMat;
    public Material rightIndicatorMat;
    public Slider gearStick;
    public Sprite leftIndicatorOff;
    public Sprite leftIndicatorOn;
    public Sprite rightIndicatorOff;
    public Sprite rightIndicatorOn;

    private Image leftIndicator;
    private Image rightIndicator;
    private Color transparentYellow;

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

    public bool turningLeft;
    public bool turningRight;
    public bool indicatingLeft;
    public bool indicatingRight;

    private bool acceleratorPressed;
    private bool brakePressed;
    private bool usingKeyboard;    
    private bool prepStopIndLeft;
    private bool prepStopIndRight;

    // Assigned in Unity.
    public float maxSteerAngle;
    public float motorForce;
    public float speed;


    private void Awake()
    {
        gear = "Drive";
        gameManager = GameObject.Find("GameManager");
        ruleChecker = GameObject.Find("RuleChecker");
        leftIndicator = GameObject.Find("LeftIndicatorButton").GetComponent<Image>();
        rightIndicator = GameObject.Find("RightIndicatorButton").GetComponent<Image>();
        transparentYellow = new Color(192, 121, 0);

        leftIndicatorMat.SetColor("_EmissionColor", Color.black);
        rightIndicatorMat.SetColor("_EmissionColor", Color.black);
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

        if (steeringAngle < -15)
        {
            turningLeft = true;
        }
        else
        {
            turningLeft = false;
        }

        if (steeringAngle > 20)
        {
            turningRight = true;
        }
        else
        {
            turningRight = false;
        }
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


    public void IndicateLeft()
    {
        if (!indicatingLeft)
        {
            indicatingLeft = true;
            indicatingRight = false;

            StartCoroutine(FlashLeftIndicator());
            StartCoroutine(CheckIfTurningLeft());
        }
        else
        {
            indicatingLeft = false;
        }
    }


    private IEnumerator FlashLeftIndicator()
    {
        while (indicatingLeft)
        {
            leftIndicator.sprite = leftIndicatorOn;
            leftIndicatorMat.SetColor("_EmissionColor", transparentYellow);
            yield return new WaitForSeconds(0.5f);

            leftIndicator.sprite = leftIndicatorOff;
            leftIndicatorMat.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(0.5f);
        }
    }


    private IEnumerator CheckIfTurningLeft()
    {
        while (indicatingLeft)
        {
            if (turningLeft && prepStopIndLeft == false)
            {
                prepStopIndLeft = true;
                Debug.Log("preparing to stop");
            }
            else if (!turningLeft && prepStopIndLeft)
            {
                prepStopIndLeft = false;
                indicatingLeft = false;
                Debug.Log("stopped");
            }

            yield return null;
        }

        yield return null;
    }


    public void IndicateRight()
    {
        if (!indicatingRight)
        {
            indicatingRight = true;
            indicatingLeft = false;

            StartCoroutine(FlashRightIndicator());
            StartCoroutine(CheckIfTurningRight());
        }
        else
        {
            indicatingRight = false;
        }
    }


    private IEnumerator FlashRightIndicator()
    {
        while (indicatingRight)
        {
            rightIndicator.sprite = rightIndicatorOn;
            rightIndicatorMat.SetColor("_EmissionColor", transparentYellow);
            yield return new WaitForSeconds(0.5f);

            rightIndicator.sprite = rightIndicatorOff;
            rightIndicatorMat.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(0.5f);
        }
    }


    private IEnumerator CheckIfTurningRight()
    {
        while (indicatingRight)
        {
            if (turningRight && prepStopIndRight == false)
            {
                prepStopIndRight = true;
                Debug.Log("preparing to stop");
            }
            else if (!turningRight && prepStopIndRight)
            {
                prepStopIndRight = false;
                indicatingRight = false;
                Debug.Log("stopped");
            }

            yield return null;
        }

        yield return null;
    }
}
