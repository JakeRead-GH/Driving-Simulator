using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject ruleChecker;
    private GameObject player;
    private GameObject titleScreen;
    private GameObject controlsScreen;
    private GameObject playingScreen;
    private GameObject settingsScreen;
    private GameObject endScreen;

    public TextMeshProUGUI errorsColumn;
    public TextMeshProUGUI numErrorsColumn;

    private string errors;
    private string numErrors;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        ruleChecker = GameObject.Find("RuleChecker");
        player = GameObject.Find("Player");
        titleScreen = GameObject.Find("TitleScreen");
        controlsScreen = GameObject.Find("ControlsScreen");
        playingScreen = GameObject.Find("PlayingScreen");
        settingsScreen = GameObject.Find("SettingsScreen");
        endScreen = GameObject.Find("EndScreen");
    }


    void Start()
    {
        titleScreen.SetActive(true);
        controlsScreen.SetActive(false);
        playingScreen.SetActive(false);
        settingsScreen.SetActive(false);
        endScreen.SetActive(false);
    }


    public void ControlsPressed()
    {
        titleScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }


    public void StartPressed()
    {
        titleScreen.SetActive(false);
        playingScreen.SetActive(true);

        gameManager.GetComponent<GameManager>().playing = true;
    }


    public void SettingsPressed()
    {
        titleScreen.SetActive(false);
        playingScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }


    public void ReturnToTitleScreen()
    {
        titleScreen.SetActive(true);
        controlsScreen.SetActive(false);
        playingScreen.SetActive(false);
        settingsScreen.SetActive(false);
        endScreen.SetActive(false);

        ruleChecker.GetComponent<RuleChecker>().brokenRules.Clear();
        ruleChecker.GetComponent<RuleChecker>().timesBroken.Clear();

        errors = null;
        numErrors = null;

        gameManager.GetComponent<GameManager>().playing = false;
        StartCoroutine(player.GetComponent<TriggerController>().ResetPosition());
    }


    public void EndScreen()
    {
        playingScreen.SetActive(false);
        endScreen.SetActive(true);

        errorsColumn.text = "";
        numErrorsColumn.text = "";

        gameManager.GetComponent<GameManager>().playing = false;

        for (int a = 0; a < ruleChecker.GetComponent<RuleChecker>().brokenRules.Count; a++)
        {
            errors += ruleChecker.GetComponent<RuleChecker>().brokenRules[a] + "\n" + "\n";
            numErrors += ruleChecker.GetComponent<RuleChecker>().timesBroken[a] + "\n" + "\n";
        }

        if (errors != null)
        {
            errorsColumn.text = errors;
            numErrorsColumn.text = numErrors;
        }
        else
        {
            errorsColumn.text = "None";
        }
    }
}
