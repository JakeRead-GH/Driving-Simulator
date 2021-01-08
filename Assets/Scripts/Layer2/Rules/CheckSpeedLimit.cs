using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpeedLimit : MonoBehaviour
{
    private GameObject player;

    public bool checking;
    public bool ruleBroken;

    private float speed;
    private int tolerance = 5;

    private void Start()
    {
        player = GameObject.Find("Player");
    }


    /* A coroutine to test for speed limits. Starts when the user enters a speed limit
       trigger and ends when they leave it. A tolerance of +-5 is allowed. Reusable
       with any speed limit. */
    public IEnumerator CheckRule(int speedLimit)
    {
        ruleBroken = false;

        while (checking)
        {
            speed = player.GetComponent<CarController>().speed;

            if (speed < speedLimit - tolerance || speed > speedLimit + tolerance)
            {
                ruleBroken = true;
            }

            yield return null;
        }

        Debug.Log(ruleBroken);
        yield return ruleBroken;
    }
}
