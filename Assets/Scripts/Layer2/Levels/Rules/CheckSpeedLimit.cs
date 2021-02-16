using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpeedLimit : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManager;
    private GameObject ruleChecker;

    public bool checking;
    public bool dontSendRuleBreak;

    public string ruleBroken;

    private float speed;
    private int tolerance = 5;

    private void Awake()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
        ruleChecker = GameObject.Find("RuleChecker");
    }


    /* A coroutine to test for speed limits. Starts when the user enters a speed limit
       trigger and ends when they leave it. A tolerance of +-5 is allowed. Reusable
       with any speed limit. */
    public IEnumerator CheckRule(int speedLimit)
    {
        ruleBroken = "none";

        while (checking)
        {
            speed = player.GetComponent<CarController>().speed;

            if (speed > speedLimit + tolerance && dontSendRuleBreak == false)
            {
                ruleBroken = "Speeding";
                Debug.Log(ruleBroken);
                StartCoroutine(gameManager.GetComponent<GameManager>().DisplayBrokenRule(ruleBroken));
                dontSendRuleBreak = true;
            }
            else if (speed < speedLimit + tolerance && dontSendRuleBreak)
            {
                dontSendRuleBreak = false;
            }

            yield return null;
        }

        if (ruleBroken != "none")
        {
            Debug.Log(ruleBroken);

            ruleChecker.GetComponent<RuleChecker>().UpdateLists(ruleBroken);
            StartCoroutine(gameManager.GetComponent<GameManager>().DisplayBrokenRule(ruleBroken));
        }
    }
}