using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleChecker : MonoBehaviour
{
    private GameObject ruleChecker;

    public bool correctOrientation;

    private void Start()
    {
        ruleChecker = GameObject.Find("RuleChecker");
    }

    // Runs various coroutines and functions based on rule checking triggers from the trigger controller.
    public void CheckRules(string rule)
    {
        if (correctOrientation)
        {
            if (rule == "StopSignStart")
            {
                ruleChecker.GetComponent<CheckStopSign>().checking = true;
                StartCoroutine(ruleChecker.GetComponent<CheckStopSign>().CheckRule());
            }
            else if (rule == "TrafficLightsStart")
            {
                ruleChecker.GetComponent<CheckTrafficLights>().checking = true;
                StartCoroutine(ruleChecker.GetComponent<CheckTrafficLights>().CheckPONR());
            }
        }
        else if (correctOrientation == false)
        {
            if (rule == "StopSignEnd")
            {
                ruleChecker.GetComponent<CheckStopSign>().checking = false;
            }
            else if (rule == "TrafficLightsEnd")
            {
                ruleChecker.GetComponent<CheckTrafficLights>().checking = false;
                ruleChecker.GetComponent<CheckTrafficLights>().CheckGreenRed();
            }
        }

        if (rule == "SpeedLimit40Start")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(ruleChecker.GetComponent<CheckSpeedLimit>().CheckRule(40));
        }
        else if (rule == "SpeedLimit40End")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = false;
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
        else if (rule == "SpeedLimit60Start")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(ruleChecker.GetComponent<CheckSpeedLimit>().CheckRule(60));
        }
        else if (rule == "SpeedLimit60End")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = false;
        }
        else if (rule == "SpeedLimit70Start")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(ruleChecker.GetComponent<CheckSpeedLimit>().CheckRule(70));
        }
        else if (rule == "SpeedLimit70End")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = false;
        }
        else if (rule == "SpeedLimit80Start")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(ruleChecker.GetComponent<CheckSpeedLimit>().CheckRule(80));
        }
        else if (rule == "SpeedLimit80End")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = false;
        }
        else if (rule == "SpeedLimit100Start")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(ruleChecker.GetComponent<CheckSpeedLimit>().CheckRule(100));
        }
        else if (rule == "SpeedLimit100End")
        {
            ruleChecker.GetComponent<CheckSpeedLimit>().checking = false;
        }
    }
}
