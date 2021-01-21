using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private GameObject ruleChecker;
    private GameObject levelGenerator;
    private GameObject levelUpdater;
    private GameObject controls;
    private GameObject endScreen;

    public TextMeshProUGUI errors;

    public List<string> brokenRules;
    public List<int> timesBroken;

    public bool playing;

    /* Controls various game aspects by initialising other controllers and disabling certain scripts
       by turning playing on and off. This is useful for disabling movement in a level selct screen
       in the future. */
    void Start()
    {
        player = GameObject.Find("Player");
        ruleChecker = GameObject.Find("RuleChecker");
        levelGenerator = GameObject.Find("LevelGenerator");
        levelUpdater = GameObject.Find("LevelUpdater");
        controls = GameObject.Find("Controls");
        endScreen = GameObject.Find("EndScreen");

        //controls.SetActive(true);
        //endScreen.SetActive(false);

        //levelGenerator.GetComponent<LevelGenerator>().GenerateLevel("StopSignTutorial");
        playing = true;
        levelUpdater.GetComponent<LevelUpdater>().StartUpdates();
    }

    public void TakeScreenshot()
    {
        Debug.Log("Took Screenshot");
        ScreenCapture.CaptureScreenshot("RuleBroken");
    }

    public void ShowEndScreen()
    {
        playing = false;

        //endScreen.SetActive(true);
        //controls.SetActive(false);

        for (int a = 0; a < brokenRules.Count; a++)
        {
            errors.text = brokenRules[a] + "\n";
            //timesBroken[a];
        }
    }
}
    