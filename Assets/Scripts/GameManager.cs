using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private GameObject ruleChecker;
    private GameObject levelGenerator;
    private GameObject levelUpdater;

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

        //levelGenerator.GetComponent<LevelGenerator>().GenerateLevel("StopSignTutorial");
        playing = true;
        levelUpdater.GetComponent<LevelUpdater>().StartUpdates();
    }
}
    