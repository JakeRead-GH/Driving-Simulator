using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrafficLights : MonoBehaviour
{
    private GameObject levelUpdater;
    private GameObject player;

    private Rigidbody playerRB;

    public bool checking;
    public bool ruleBroken;

    private string lightColour;
    private float inverseZ;

    private void Start()
    {
        levelUpdater = GameObject.Find("LevelUpdater");
        player = GameObject.Find("Player");

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

        while (checking && lightColour == "yellow")
        {
            // Inverse finds the transform local to the object rather than globally.
            inverseZ = playerRB.transform.InverseTransformDirection(playerRB.velocity).z;

            // Checks if the user stopped. If they didn't the rule is broken.
            if (inverseZ > -0.1f && inverseZ < 0.1f)
            {
                ruleBroken = false;
                checking = false;
            }
            else
            {
                ruleBroken = true;
            }

            yield return null;
        }

        Debug.Log(ruleBroken);
        yield return ruleBroken;
    }


    /* Checks the light colour when the user exits the traffic lights trigger.
       If it was red when they left then they ran the light and the rule is broken. */
    public bool CheckGreenRed()
    {
        lightColour = levelUpdater.GetComponent<ChangeTrafficLights>().currentLight;

        if (lightColour == "green")
        {
            ruleBroken = false;
        }
        else if (lightColour == "red")
        {
            ruleBroken = true;
        }

        Debug.Log(ruleBroken);
        return ruleBroken;
    }
}
