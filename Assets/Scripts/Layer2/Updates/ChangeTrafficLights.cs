using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTrafficLights : MonoBehaviour
{
    private GameObject gameManager;

    public Material greenLight;
    public Material yellowLight;
    public Material redLight;

    public string currentLight;
    private int lightSetting;


    // Sets all lights to black.
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        greenLight.SetColor("_EmissionColor", Color.black);
        yellowLight.SetColor("_EmissionColor", Color.black);
        redLight.SetColor("_EmissionColor", Color.black);
    }

    // A coroutine for controlling traffic light colours.
    public IEnumerator ChangeLights()
    {
        lightSetting = 0;

        yield return new WaitUntil(() => gameManager.GetComponent<GameManager>().playing);

        while (gameManager.GetComponent<GameManager>().playing)
        {
            lightSetting++;

            // Updates light to green for 10 seconds.
            if (lightSetting == 1)
            {
                greenLight.SetColor("_EmissionColor", Color.green);
                redLight.SetColor("_EmissionColor", Color.black);

                currentLight = "green";
                yield return new WaitForSeconds(10);
            }
            // Updates light to yellow for 3 seconds.
            else if (lightSetting == 2)
            {
                yellowLight.SetColor("_EmissionColor", Color.yellow);
                greenLight.SetColor("_EmissionColor", Color.black);

                currentLight = "yellow";
                yield return new WaitForSeconds(3);
            }
            // Updates light to red for 10 seconds.
            else
            {
                redLight.SetColor("_EmissionColor", Color.red);
                yellowLight.SetColor("_EmissionColor", Color.black);

                currentLight = "red";
                yield return new WaitForSeconds(10);

                lightSetting = 0;
            }

            yield return null;
        }
    }
}
