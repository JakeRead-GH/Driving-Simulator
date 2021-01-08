using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    private GameObject gameManager;

    // Assigned in Unity.
    public Rigidbody carRb;
    public WheelCollider frontDriverWheel, frontPassengerWheel, rearDriverWheel, rearPassengerWheel;
    public Transform frontDriverTransform, frontPassengerTransform, rearDriverTransform, rearPassengerTransform;
    public TextMeshProUGUI speedometer;

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private float brakes;

    // Assigned in Unity.
    public float maxSteerAngle;
    public float motorForce;
    public float speed;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }


    // Allows the user to control the car while a level is loaded.
    public void FixedUpdate()
    {
        if (gameManager.GetComponent<GameManager>().playing)
        {
            GetInput();
            Steer();

            if (Input.GetKey(KeyCode.Space))
            {
                Brake();
            }
            else
            {
                Accelerate();
            }

            UpdateWheelPoses();
            UpdateSpeed();
        }
    }


    /* Gets an input using Unity's built in controls for arrow keys and wasd the
       output value is any float between -1 and 1 based on how long the keys are held. */
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }


    // Changes the steering angle to a max of 40 degrees based on user input.
    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        frontDriverWheel.steerAngle = steeringAngle;
        frontPassengerWheel.steerAngle = steeringAngle;
    }


    // Slams the brakes when space is pressed, as opposed to using s to slow normally.
    private void Brake()
    {
        brakes = 20000;
        frontDriverWheel.brakeTorque = brakes;
        frontPassengerWheel.brakeTorque = brakes;
    }


    // Changes the players acceleration to a max motor force of 50 based on user input.
    private void Accelerate()
    {
        frontDriverWheel.brakeTorque = 0;
        frontPassengerWheel.brakeTorque = 0;
        frontDriverWheel.motorTorque = motorForce * verticalInput;
        frontPassengerWheel.motorTorque = motorForce * verticalInput;
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
        speed = Mathf.RoundToInt(carRb.velocity.magnitude * 3.6f);
        speedometer.text = "Speed: " + speed + " kph";
    }
}
