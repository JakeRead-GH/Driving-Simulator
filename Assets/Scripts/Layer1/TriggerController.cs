using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private GameObject ruleChecker;
    private GameObject cameras;
    private Vector3 startingPos;
    private Quaternion startingRot;
    private Rigidbody playerRB;

    private void Start()
    {
        ruleChecker = GameObject.Find("RuleChecker");
        cameras = GameObject.Find("ThirdPersonCam");
        playerRB = gameObject.GetComponent<Rigidbody>();

        startingPos = gameObject.transform.position;
        startingRot = gameObject.transform.rotation;
    }

    /* Sends commands to the rule checker whenever the player enters a rule trigger.
       These commands begin the checks for that rule type. */
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerEntrance"))
        {
            ruleChecker.GetComponent<RuleChecker>().correctOrientation = true;
        }
        else if (other.CompareTag("TriggerExit"))
        {
            ruleChecker.GetComponent<RuleChecker>().correctOrientation = false;
        }
        else if (other.CompareTag("StopSignTile"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("StopSignStart");
        }
        else if (other.CompareTag("40Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit40Start");
        }
        else if (other.CompareTag("50Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit50Start");
        }
        else if (other.CompareTag("60Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit60Start");
        }
        else if (other.CompareTag("70Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit70Start");
        }
        else if (other.CompareTag("80Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit80Start");
        }
        else if (other.CompareTag("100Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit100Start");
        }
        else if (other.CompareTag("TrafficLightsTile"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("TrafficLightsStart");
        }
        else if (other.CompareTag("KillBox"))
        {
            StartCoroutine(ResetPosition());
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
        else if (other.CompareTag("40Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit40End");
        }
        else if (other.CompareTag("50Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit50End");
        }
        else if (other.CompareTag("60Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit60End");
        }
        else if (other.CompareTag("70Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit70End");
        }
        else if (other.CompareTag("80Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit80End");
        }
        else if (other.CompareTag("100Zone"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("SpeedLimit100End");
        }
        else if (other.CompareTag("TrafficLightsTile"))
        {
            ruleChecker.GetComponent<RuleChecker>().CheckRules("TrafficLightsEnd");
        }
    }


    public IEnumerator ResetPosition()
    {
        gameObject.transform.position = startingPos;
        gameObject.transform.rotation = startingRot;

        if (cameras.GetComponent<CameraFollow>().thirdPerson)
        {
            cameras.GetComponent<CameraFollow>().rearMirrorCam.SetActive(false);
            cameras.GetComponent<CameraFollow>().thirdPersonCam.SetActive(true);
        }
        else
        {
            cameras.GetComponent<CameraFollow>().rearMirrorCam.SetActive(false);
            cameras.GetComponent<CameraFollow>().firstPersonCam.SetActive(true);
        }

        gameObject.GetComponent<CarController>().gearStick.value = 0;

        playerRB.constraints = RigidbodyConstraints.FreezeAll;
        playerRB.velocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(1);

        playerRB.constraints = RigidbodyConstraints.None;
    }
}
