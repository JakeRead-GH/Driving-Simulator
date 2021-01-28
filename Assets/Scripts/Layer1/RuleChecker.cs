using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleChecker : MonoBehaviour
{
    public List<string> brokenRules;
    public List<int> timesBroken;

    public bool correctOrientation;

    private int pos;
    private int count;

    // Runs various coroutines and functions based on rule checking triggers from the trigger controller.
    public void CheckRules(string rule)
    {
        if (correctOrientation)
        {
            if (rule == "StopSignStart")
            {
                gameObject.GetComponent<CheckStopSign>().checking = true;
                StartCoroutine(gameObject.GetComponent<CheckStopSign>().CheckRule());
            }
            else if (rule == "TrafficLightsStart")
            {
                gameObject.GetComponent<CheckTrafficLights>().checking = true;
                StartCoroutine(gameObject.GetComponent<CheckTrafficLights>().CheckPONR());
            }
        }
        else if (correctOrientation == false)
        {
            if (rule == "StopSignEnd")
            {
                gameObject.GetComponent<CheckStopSign>().checking = false;
            }
            else if (rule == "TrafficLightsEnd")
            {
                gameObject.GetComponent<CheckTrafficLights>().checking = false;
                gameObject.GetComponent<CheckTrafficLights>().CheckGreenRed();
            }
        }

        if (rule == "SpeedLimit40Start")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(gameObject.GetComponent<CheckSpeedLimit>().CheckRule(40));
        }
        else if (rule == "SpeedLimit40End")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = false;
        }
        else if (rule == "SpeedLimit50Start")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(gameObject.GetComponent<CheckSpeedLimit>().CheckRule(50));
        }
        else if (rule == "SpeedLimit50End")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = false;
        }
        else if (rule == "SpeedLimit60Start")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(gameObject.GetComponent<CheckSpeedLimit>().CheckRule(60));
        }
        else if (rule == "SpeedLimit60End")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = false;
        }
        else if (rule == "SpeedLimit70Start")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(gameObject.GetComponent<CheckSpeedLimit>().CheckRule(70));
        }
        else if (rule == "SpeedLimit70End")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = false;
        }
        else if (rule == "SpeedLimit80Start")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(gameObject.GetComponent<CheckSpeedLimit>().CheckRule(80));
        }
        else if (rule == "SpeedLimit80End")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = false;
        }
        else if (rule == "SpeedLimit100Start")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = true;
            StartCoroutine(gameObject.GetComponent<CheckSpeedLimit>().CheckRule(100));
        }
        else if (rule == "SpeedLimit100End")
        {
            gameObject.GetComponent<CheckSpeedLimit>().checking = false;
        }
    }


    public void UpdateLists(string ruleBroken)
    {
        if (brokenRules.Contains(ruleBroken))
        {
            pos = brokenRules.IndexOf(ruleBroken);
            count = timesBroken[pos];
            count++;
            Debug.Log(ruleBroken + ": " + count);
            timesBroken.Remove(pos);
            timesBroken.Insert(pos, count);
        }
        else
        {
            brokenRules.Add(ruleBroken);
            Debug.Log("Added " + ruleBroken);
            timesBroken.Add(1);
        }
    }
}
