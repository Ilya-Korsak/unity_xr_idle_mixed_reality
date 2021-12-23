using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.Events;
public class XRManager : MonoBehaviour
{
    public static XRManager instance = null;
    private List<InputFeatureUsage> featureUsages = new List<InputFeatureUsage>();
    private InputDevice headDevice;
    private float lastTime = 0;
    [SerializeField]
    private int _sleepTimeoutMins = 3;
    public int sleepTimeoutMins
    {
        get
        {
            return _sleepTimeoutMins;
        }
    }
    [ReadOnly]
    [SerializeField]
    private bool _isHeadsetIsWeared = false;
    public bool isHeadsetIsWeared
    {
        get
        {
            return _isHeadsetIsWeared;
        }
    }
    [ReadOnly]
    [SerializeField]
    private bool _isRestarting = false;
    public bool isRestarting
    {
        get
        {
            return _isRestarting;
        }
    }
    [ReadOnly]
    [SerializeField]
    private bool _isSleeping = false;
    public bool isSleeping
    {
        get
        {
            return _isSleeping;
        }
    }
    //Events
    public UnityEvent onSleep;
    public UnityEvent onWake;
    public UnityEvent onWaked;
    public UnityEvent onWear;
    public UnityEvent onTakenOff;
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(4);
        _isRestarting = true;
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        yield return null;

        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        yield return null;

        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        yield return null;

        XRGeneralSettings.Instance.Manager.StartSubsystems();
        yield return null;

        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            //Debug.Log("Successful restart.");
            _isRestarting = false;
            onWaked.Invoke();
        }
        else
        {
            Debug.LogError("Failure to restart OpenXRLoader after shutdown.");
        }
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitializeManager();
    }
    void InitializeManager()
    {
       //SOME START STUFF HERE
    }

    // Update is called once per frame
    void Update()
    {
        if (headDevice.name == null)
        {
            headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
            headDevice.TryGetFeatureUsages(featureUsages);
        }
        else
        {
            if (!_isRestarting)
            {
                headDevice.TryGetFeatureValue(featureUsages[0].As<bool>(), out bool value);
                if (!_isHeadsetIsWeared && value)
                {
                    if (lastTime >= 60 * _sleepTimeoutMins)
                    {
                        StartCoroutine(Restart());
                        _isSleeping = false;
                        onWake.Invoke();
                    }
                    else
                    {
                        _isHeadsetIsWeared = true;
                        onWear.Invoke();
                    }
                    _isHeadsetIsWeared = true;
                    lastTime = 0;
                }
                if (!value)
                {

                    if (lastTime >= 60 * _sleepTimeoutMins && !_isSleeping)
                    {
                        _isSleeping = true;
                        onSleep.Invoke();
                    }
                    lastTime += Time.deltaTime;
                    if (_isHeadsetIsWeared == true)
                    {
                        _isHeadsetIsWeared = false;
                        onTakenOff.Invoke();
                    }
                }
            }
        }
    }
}
