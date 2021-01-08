using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleChecker : MonoBehaviour
{
    private GameObject ruleChecker;

    private void Start()
    {
        ruleChecker = GameObject.Find("RuleChecker");
    }

    // Runs various coroutines and functions based on rule checking triggers from the trigger controller.
    public void CheckRules(string rule)
    {
        if (rule == "StopSignStart")
        {
            ruleChecker.GetComponent<CheckStopSign>().checking = true;
            StartCoroutine(ruleChecker.GetComponent<CheckStopSign>().CheckRule());
        }
        else if (rule == "StopSignEnd")
        {
            ruleChecker.GetComponent<CheckStopSign>().checking = false;
        }
        else if (rule == "SpeedLimit50Start")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(ruleChecker.GetComponent<CheckSpeedLimit>().CheckRule(50));
        }
        else if (rule == "SpeedLimit50End")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = false;
        }
        else if (rule == "TrafficLightsStart")
        {
            ruleChecker.GetComponent<CheckTrafficLights>().checking = true;
            StartCoroutine(ruleChecker.GetComponent<CheckTrafficLights>().CheckPONR());
        }
        else if (rule == "TrafficLightsEnd")
        {
            ruleChecker.GetComponent<CheckTrafficLights>().checking = false;
            ruleChecker.GetComponent<CheckTrafficLights>().CheckGreenRed();
        }
    }
}
