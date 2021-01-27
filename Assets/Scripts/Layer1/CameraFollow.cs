using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Assigned to player in Unity
    public Transform carThirdPerson;
    //public Transform carFirstPerson;

    // The offset of the camera for 3rd person. Assigned in Unity.
    public Vector3 thirdPersonOffset;
    //public Vector3 firstPersonOffset;
    private Vector3 lookDirection;
    private Vector3 targetPosition;

    private Quaternion rotation;

    private float followSpeed = 10;
    private float lookSpeed = 10;

    //private string perspective = "ThirdPerson";


    private void Awake()
    {
        thirdPersonOffset = new Vector3(0, 2.5f, -5);

        followSpeed = 10;
        lookSpeed = 10;
    }


    /* Runs every update after a fixed amount of time, unlike Update,
       which runs based on fps. Stops jittery movement. */
    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }

    // Turns the camera to look at the player. Camera moves at a consistent speed.
    public void LookAtTarget()
    {
        /*if (perspective == "ThirdPerson")
        {
            rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
        else if (perspective == "FirstPerson")
        {
            rotation = Quaternion.LookRotation(lookDirection);
        }*/
        rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        lookDirection = carThirdPerson.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    //Moves the camera towards the player at a consistent speed. Creates camera offset.
    public void MoveToTarget()
    {
        /*if (perspective == "ThirdPerson")
        {
            targetPosition = carThirdPerson.position + carThirdPerson.forward * thirdPersonOffset.z + carThirdPerson.right * thirdPersonOffset.x + carThirdPerson.up * thirdPersonOffset.y;
        }
        else if (perspective == "FirstPerson")
        {
            targetPosition = carFirstPerson.position + carFirstPerson.forward * firstPersonOffset.z + carFirstPerson.right * firstPersonOffset.x + carFirstPerson.up * firstPersonOffset.y;
        }*/

        targetPosition = carThirdPerson.position + carThirdPerson.forward * thirdPersonOffset.z + carThirdPerson.right * thirdPersonOffset.x + carThirdPerson.up * thirdPersonOffset.y;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }


}
