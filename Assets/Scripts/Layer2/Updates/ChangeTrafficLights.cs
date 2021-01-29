using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTrafficLights : MonoBehaviour
{
    private GameObject gameManager;

    public Material greenLight;
    public Material yellowLight;
    public Material redLight;

    public Material greenLight2;
    public Material yellowLight2;
    public Material redLight2;

    public string currentLightSG;
    public string currentLightSR;

    private int lightSettingSG;
    private int lightSettingSR;


    // Sets all lights to black.
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // A coroutine for controlling traffic light colours.
    public IEnumerator ChangeLightsStartGreen()
    {
        lightSettingSG = 0;

        greenLight.SetColor("_EmissionColor", Color.black);
        yellowLight.SetColor("_EmissionColor", Color.black);
        redLight.SetColor("_EmissionColor", Color.black);

        while (gameManager.GetComponent<GameManager>().playing)
        {
            lightSettingSG++;

            // Updates light to green for 10 seconds.
            if (lightSettingSG == 1)
            {
                greenLight.SetColor("_EmissionColor", Color.green);
                redLight.SetColor("_EmissionColor", Color.black);

                currentLightSG = "green";
                yield return new WaitForSeconds(10);
            }
            // Updates light to yellow for 3 seconds.
            else if (lightSettingSG == 2)
            {
                yellowLight.SetColor("_EmissionColor", Color.yellow);
                greenLight.SetColor("_EmissionColor", Color.black);

                currentLightSG = "yellow";
                yield return new WaitForSeconds(3);
            }
            // Updates light to red for 13 seconds.
            else
            {
                redLight.SetColor("_EmissionColor", Color.red);
                yellowLight.SetColor("_EmissionColor", Color.black);

                currentLightSG = "red";
                yield return new WaitForSeconds(13);

                lightSettingSG = 0;
            }

            yield return null;
        }
    }


    public IEnumerator ChangeLightsStartRed()
    {
        lightSettingSR = 0;

        greenLight2.SetColor("_EmissionColor", Color.black);
        yellowLight2.SetColor("_EmissionColor", Color.black);
        redLight2.SetColor("_EmissionColor", Color.black);

        while (gameManager.GetComponent<GameManager>().playing)
        {
            lightSettingSR++;

            // Updates light to red for 10 seconds.
            if (lightSettingSR == 1)
            {
                redLight2.SetColor("_EmissionColor", Color.red);
                yellowLight2.SetColor("_EmissionColor", Color.black);

                currentLightSR = "red";
                yield return new WaitForSeconds(13);
            }
            // Updates light to green for 10 seconds.
            else if (lightSettingSR == 2)
            {
                greenLight2.SetColor("_EmissionColor", Color.green);
                redLight2.SetColor("_EmissionColor", Color.black);

                currentLightSR = "green";
                yield return new WaitForSeconds(10);
            }
            // Updates light to yellow for 3 seconds.
            else
            {
                yellowLight2.SetColor("_EmissionColor", Color.yellow);
                greenLight2.SetColor("_EmissionColor", Color.black);

                currentLightSR = "yellow";
                yield return new WaitForSeconds(3);

                lightSettingSR = 0;
            }

            yield return null;
        }
    }
}
