using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Assigned to player in Unity
    public Transform objectToFollow;
    // The offset of the camera for 3rd person. Assigned in Unity.
    public Vector3 offset;

    public float followSpeed = 10;
    public float lookSpeed = 10;

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
        Vector3 lookDirection = objectToFollow.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    //Moves the camera towards the player at a consistent speed. Creates camera offset.
    public void MoveToTarget()
    {
        Vector3 targetPosition = objectToFollow.position + objectToFollow.forward * offset.z + objectToFollow.right * offset.x + objectToFollow.up * offset.y;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }


}
