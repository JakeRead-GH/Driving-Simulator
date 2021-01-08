using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Loads different levels from level select screen. Currently unused.
    public void GenerateLevel(string level)
    {
        if (level == "StopSignTutorial")
        {
            gameObject.GetComponent<LoadStopSignTutorial>().Load();
        }
    }
}
