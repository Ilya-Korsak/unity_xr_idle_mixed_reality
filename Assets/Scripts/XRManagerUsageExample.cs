using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRManagerUsageExample : MonoBehaviour
{
    public void OnWakeTest()
    {
        Debug.Log("WAKEUPPP!!!");
    }
    public void OnWakedTest()
    {
        Debug.Log("WAKED AND READY");
    }
    public void OnWearTest()
    {
        Debug.Log("HEADCRAB");
        //Accessing fields
        Debug.Log(XRManager.instance.sleepTimeoutMins);
        Debug.Log(XRManager.instance.isSleeping);
        Debug.Log(XRManager.instance.isRestarting);
        Debug.Log(XRManager.instance.isHeadsetIsWeared);
    }
    public void TakenOffTest()
    {
        Debug.Log("OnTakenOff");
    }
    public void OnSleepTest()
    {
        Debug.Log("OnTakenOff");
    }
}
