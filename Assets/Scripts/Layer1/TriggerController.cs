using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject ruleChecker;

    private void Start()
    {
        ruleChecker = GameObject.Find("RuleChecker");
    }

    /* Sends commands to the rule checker whenever the player enters a rule trigger.
       These commands begin the checks for that rule type. */
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StopSignTile"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("StopSignStart");
        }
        else if (other.CompareTag("50Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit50Start");
        }
        else if (other.CompareTag("TrafficLightsTile"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("TrafficLightsStart");
        }
    }


    /* Sends commands to the rule checker whenever the player leaves a rule trigger.
       These commands conclude the checks for that rule type or run rules that only
       require one check. */
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StopSignTile"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("StopSignEnd");
        }
        else if (other.CompareTag("50Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit50End");
        }
        else if (other.CompareTag("TrafficLightsTile"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("TrafficLightsEnd");
        }
    }
}
