### What is this repository for? ###

* Here some usecase how to wake up VR headset from Unity

### How do I get set up? ###

* Go to Settings -> Mixed Reality -> Head display -> Sleep timeout: 3 minutes
* Every simple magic happend in XRManager.cs (Scripts folder)
* Also you have to setup Sleep timeout (same time like in settings) in XRManager.sleepTimeoutMins
* Just run it in Unity
* You can look to XRManagerUsageExample.cs to see how to use XRManager 

### What else can I do with XRManager ###

* onSleep - calls when headset going to sleep
* onWake - calls when headset is waking up
* onWaked - calls when headset is waked up and ready
* onWear - calls when headset sensor is triggered (when player wear it)
* onTakenOff - calls when headset sensor is "empty" (when player take off headset)

### What values I can get with XRManager ###

* Get property isRestarting - when headset is waking up it takes a time, while it happend this propery is TRUE (Read only)
* Get property isHeadsetIsWeared - when headset is on players's head value is TRUE! (Read only too)
* Get property isSleeping - when headset is sleeping value is TRUE (Read only, surprise)
