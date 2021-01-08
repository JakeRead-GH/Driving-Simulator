using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReusableFunctions : MonoBehaviour
{
    // A reusable timer function. Currently unused.
    public IEnumerator Countdown(int time)
    {
        int remainingTime;

        remainingTime = time;

        while (remainingTime > 0)
        {
            Debug.Log(remainingTime);
            yield return new WaitForSeconds(1);
            remainingTime--;
        }
    }
}
