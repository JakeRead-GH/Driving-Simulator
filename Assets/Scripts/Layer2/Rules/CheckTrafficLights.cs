using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrafficLights : MonoBehaviour
{
    private GameObject levelUpdater;
    private GameObject player;
    private GameObject gameManager;
    private GameObject ruleChecker;

    private Rigidbody playerRB;

    public bool checking;

    public string ruleBroken;
    private string lightColour;

    private float inverseZ;

    private void Awake()
    {
        levelUpdater = GameObject.Find("LevelUpdater");
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
        ruleChecker = GameObject.Find("RuleChecker");

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
                }
                else
                {
                    ruleBroken = "Ran Yellow Light (Could Have Stopped)";
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

            ruleChecker.GetComponent<RuleChecker>().UpdateLists(ruleBroken);
            StartCoroutine(gameManager.GetComponent<GameManager>().DisplayBrokenRule(ruleBroken));
        }
    }


    /* Checks the light colour when the user exits the traffic lights trigger.
       If it was red when they left then they ran the light and the rule is broken. */
    public void CheckGreenRed()
    {
        lightColour = levelUpdater.GetComponent<ChangeTrafficLights>().currentLight;

        if (lightColour == "green")
        {
            ruleBroken = "none";
        }
        else if (lightColour == "red")
        {
            ruleBroken = "Ran Red Light";
        }

        if (ruleBroken != "none")
        {
            Debug.Log(ruleBroken);

            ruleChecker.GetComponent<RuleChecker>().UpdateLists(ruleBroken);
            StartCoroutine(gameManager.GetComponent<GameManager>().DisplayBrokenRule(ruleBroken));
        }
    }
}
