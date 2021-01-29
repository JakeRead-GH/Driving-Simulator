using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private GameObject ruleChecker;
    private GameObject levelGenerator;
    private GameObject levelUpdater;
    private GameObject controls;
    private GameObject endScreen;
    private GameObject screenshotDisplay;
    private GameObject brokenRuleTextObject;

    public TextMeshProUGUI brokenRuleText;
    public TextMeshProUGUI errors;

    private Texture2D screenshotTexture;

    public bool playing;

    private string folderPath;
    public string screenshotName;

    private int screenshotNumber = 0;

    private void Awake()
    {
        player = GameObject.Find("Player");
        ruleChecker = GameObject.Find("RuleChecker");
        levelGenerator = GameObject.Find("LevelGenerator");
        levelUpdater = GameObject.Find("LevelUpdater");
        controls = GameObject.Find("Controls");
        endScreen = GameObject.Find("EndScreen");
        screenshotDisplay = GameObject.Find("ScreenshotDisplay");
        brokenRuleTextObject = GameObject.Find("BrokenRuleText");
    }

    /* Controls various game aspects by initialising other controllers and disabling certain scripts
       by turning playing on and off. This is useful for disabling movement in a level selct screen
       in the future. */
    void Start()
    {
        //levelGenerator.GetComponent<LevelGenerator>().GenerateLevel("StopSignTutorial");

        brokenRuleTextObject.SetActive(false);
        /*folderPath = Directory.GetCurrentDirectory() + "/Assets/Resources/";
        Debug.Log(folderPath);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }*/

    }

    public IEnumerator DisplayBrokenRule(string brokenRule)
    {
        brokenRuleText.text = brokenRule;
        brokenRuleTextObject.SetActive(true);

        yield return new WaitForSeconds(3);

        brokenRuleTextObject.SetActive(false);
    }

    public IEnumerator TakeScreenshot()
    {
        screenshotName = "Screenshot" + screenshotNumber + ".png";
        screenshotNumber++;
        ScreenCapture.CaptureScreenshot(Path.Combine(folderPath, screenshotName));
        Debug.Log("Took Screenshot");

        yield return null;

        screenshotDisplay.GetComponent<UpdateImage>().UpdateDisplay();
        Debug.Log("Updated Display");

        //byte[] file = File.ReadAllBytes(folderPath);
        //screenshotTexture = new Texture2D(4, 4);
        //screenshotTexture.LoadImage(file);
        //Debug.Log("Created Texture");
    }
}
    