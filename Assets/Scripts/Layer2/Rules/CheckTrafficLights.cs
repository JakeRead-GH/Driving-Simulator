using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrafficLights : MonoBehaviour
{
    private GameObject levelUpdater;
    private GameObject player;
    private GameObject gameManager;

    private Rigidbody playerRB;

    public bool checking;

    public string ruleBroken;
    private string lightColour;

    private float inverseZ;

    private void Start()
    {
        levelUpdater = GameObject.Find("LevelUpdater");
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");

        playerRB = player.GetComponent<Rigidbody>();
    }


    /* Checks the point of no return (PONR) rule, where if it's possible to stop during a yellow light
       then you have to. Checking begins if the traffic lights trigger is entered during a yellow light.
       the user must then stop before exiting the trigger or the rule is broken. The trigger can be adjusted
       in Unity to change the PONR. */
    public IEnumerator CheckPONR()
    {
        // Checks current light colour from ChangeTrafficLights script.
        lightColour = levelUpdater.GetComponent<ChangeTrafficLights>().currentLight;

        if (lightColour == "yellow")
        {
            while (checking)
            {
                // Inverse finds the transform local to the object rather than globally.
                inverseZ = playerRB.transform.InverseTransformDirection(playerRB.velocity).z;

                // Checks if the user stopped. If they didn't the rule is broken.
                if (inverseZ > -0.1f && inverseZ < 0.1f)
                {
                    ruleBroken = "none";
                    checking = false;
                    Debug.Log("PONR Passed");
                }
                else
                {
                    ruleBroken = "Had Time to Stop For Yellow Light But Didn't";
                }

                yield return null;
            }
        }
        else
        {
            ruleBroken = "none";
        }

        if (ruleBroken != "none")
        {
            Debug.Log(ruleBroken);
            StartCoroutine(gameManager.GetComponent<GameManager>().DisplayBrokenRule(ruleBroken));
            yield return ruleBroken;
        }
    }


    /* Checks the light colour when the user exits the traffic lights trigger.
       If it was red when they left then they ran the light and the rule is broken. */
    public string CheckGreenRed()
    {
        lightColour = levelUpdater.GetComponent<ChangeTrafficLights>().currentLight;

        if (lightColour == "green")
        {
            Debug.Log("Passed Green Light");
            ruleBroken = "none";
        }
        else if (lightColour == "red")
        {
            Debug.Log("Passed Red Light");
            ruleBroken = "Ran Red Light";
        }

        if (ruleBroken != "none")
        {
            Debug.Log(ruleBroken);
            StartCoroutine(gameManager.GetComponent<GameManager>().DisplayBrokenRule(ruleBroken));
            return ruleBroken;
        }
        else
        {
            return null;
        }
    }
}
