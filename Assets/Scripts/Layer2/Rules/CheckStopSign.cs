using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStopSign : MonoBehaviour
{
    private GameObject player;

    private Rigidbody playerRB;

    public bool checking;
    public bool ruleBroken;

    private float inverseZ;

    private int requiredChecks = 3;
    private int checksPassed;

    private void Start()
    {
        player = GameObject.Find("Player");

        playerRB = player.GetComponent<Rigidbody>();
    }

    /* A coroutine that tests for rule breaks at stop signs. The user must
       come to a complete stop for at least 3 seconds to pass. It begins
       when the user enters a stop sign trigger and ends when they leave it. */
    public IEnumerator CheckRule()
    {
        checksPassed = 0;
        ruleBroken = true;

        while (checking)
        {
            // Inverse finds the transform local to the object rather than globally.
            inverseZ = playerRB.transform.InverseTransformDirection(playerRB.velocity).z;

            /* Checks if the user stopped. If they didn't the rule is broken.
               One check occurs every second and a minimum of 3 are required
               to ensure the user has waited for 3 seconds. */
            if (inverseZ > -0.1f && inverseZ < 0.1f)
            {
                checksPassed++;
                Debug.Log(checksPassed);
            }

            yield return new WaitForSeconds(1);
        }

        if (checksPassed >= requiredChecks)
        {
            ruleBroken = false;
        }

        Debug.Log(ruleBroken);
        yield return ruleBroken;
    }



}
