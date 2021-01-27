using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateImage : MonoBehaviour
{
    private GameObject gameManager;
    private RawImage screenshotDisplay;
    public string screenshot;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        screenshotDisplay = gameObject.GetComponent<RawImage>();
    }

    
    public void UpdateDisplay()
    {
        screenshot = gameManager.GetComponent<GameManager>().screenshotName;
        screenshotDisplay = Resources.Load(screenshot) as RawImage;
    }
}
