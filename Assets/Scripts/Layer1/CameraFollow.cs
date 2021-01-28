using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraFollow : MonoBehaviour
{
    public GameObject thirdPersonCam;
    public GameObject firstPersonCam;
    public GameObject rearMirrorCam;

    public TextMeshProUGUI mirrorButtonText;

    // Assigned to player in Unity
    public Transform carTransform;

    // The offset of the camera for 3rd person. Assigned in Unity.
    private Vector3 offset;
    private Vector3 lookDirection;
    private Vector3 targetPosition;

    private Quaternion rotation;

    private float followSpeed;
    private float lookSpeed;

    public bool thirdPerson;
    private bool checkingMirrors;


    private void Awake()
    {
        thirdPersonCam = GameObject.Find("ThirdPersonCam");
        firstPersonCam = GameObject.Find("FirstPersonCam");
        rearMirrorCam = GameObject.Find("RearMirrorCam");

        offset = new Vector3(0, 2.5f, -5);

        followSpeed = 10;
        lookSpeed = 10;

        thirdPerson = true;

        firstPersonCam.SetActive(false);
        rearMirrorCam.SetActive(false);
    }


    /* Runs every update after a fixed amount of time, unlike Update,
       which runs based on fps. Stops jittery movement. */
    private void FixedUpdate()
    {
        if (thirdPerson)
        {
            LookAtTarget();
            MoveToTarget();
        }
    }


    // Turns the camera to look at the player. Camera moves at a consistent speed.
    public void LookAtTarget()
    {
        lookDirection = carTransform.position - transform.position;
        rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }


    //Moves the camera towards the player at a consistent speed. Creates camera offset.
    public void MoveToTarget()
    {
        targetPosition = carTransform.position + carTransform.forward * offset.z + carTransform.right * offset.x + carTransform.up * offset.y;
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }


    public void ChangePerspective()
    {
        if (thirdPerson)
        {
            thirdPerson = false;
            thirdPersonCam.SetActive(false);
            firstPersonCam.SetActive(true);
        }
        else
        {
            thirdPerson = true;
            firstPersonCam.SetActive(false);
            thirdPersonCam.SetActive(true);
        }        
    }


    public void CheckMirror()
    {
        if (checkingMirrors == false)
        {
            checkingMirrors = true;
            mirrorButtonText.text = "Front View";

            if (thirdPerson)
            {
                thirdPersonCam.SetActive(false);
                rearMirrorCam.SetActive(true);
            }
            else
            {
                firstPersonCam.SetActive(false);
                rearMirrorCam.SetActive(true);
            }
        }
        else
        {
            checkingMirrors = false;
            mirrorButtonText.text = "Mirror View";

            if (thirdPerson)
            {
                rearMirrorCam.SetActive(false);
                thirdPersonCam.SetActive(true);
            }
            else
            {
                rearMirrorCam.SetActive(false);
                firstPersonCam.SetActive(true);
            }
        }
    }
}
