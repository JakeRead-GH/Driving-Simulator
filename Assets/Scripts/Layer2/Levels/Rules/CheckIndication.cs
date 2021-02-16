using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIndication : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject player;
    private GameObject ruleChecker;

    private bool onCooldown;

    private int count;

    public string ruleBroken;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("Player");
        ruleChecker = GameObject.Find("RuleChecker");
    }

    public void Update()
    {
        if (player.GetComponent<CarController>().turningLeft && !player.GetComponent<CarController>().indicatingLeft)
        {
            count = 0;
            ruleBroken = "Didn't Indicate";
            //Debug.Log("Didn't indicate left");
        }
        else if (player.GetComponent<CarController>().turningRight && !player.GetComponent<CarController>().indicatingRight)
        {
            count = 0;
            ruleBroken = "Didn't Indicate";
            //Debug.Log("Didn't indicate right");
        }
        else
        {
            ruleBroken = "none";
        }        

        

        if (ruleBroken != "none" && !onCooldown)
        {
            Debug.Log(ruleBroken);
            ruleChecker.GetComponent<RuleChecker>().UpdateLists(ruleBroken);
            StartCoroutine(gameManager.GetComponent<GameManager>().DisplayBrokenRule(ruleBroken));

            onCooldown = true;
        }
        else if (onCooldown)
        {
            count++;

            if (count == 10)
            {
                onCooldown = false;
                count = 0;
            }
        }
    }
}
