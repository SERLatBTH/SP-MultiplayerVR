
# SP-MultiplayerVR
This project was created in the course PA1456 at BTH.
The plan is to make a meeting place in VR.

# How to build?
* Download the latest version of Unity LTS, when this project was created it was Unity 2018.4.19f.
* Clone the project `git clone https://github.com/SERLatBTH/SP-MultiplayerVR`.
* Open the project in Unity.
* To get multiplayer working go to https://normcore.io/ and sign up for a free account, create an application and save down the API key.
* In Unity, click on "Realtime + VR Player" in the Hierarchy column. Enter the API-key you got form Normcore under the tab API key.
* To be able to build for Oculus Quest, you need to install the packages, `Oculus Android` and `OpenVR(Desktop)`which you can do by clicking on "Window" in the top bar and then "Package manager".
* Now you are ready to build!
# How to run?
* When you´ve built the project you need to transfer it to the Quest, to be able to do this you need to have `Android Studio` and the android API version 25 installed. This can be done by following this guide: https://circuitstream.com/blog/oculus-quest-unity-setup/.
* Then you need to put the Oculus Quest into developer mode, allowing us to transfer unsigned APKs to the device. This can be done by following this guide: https://developer.oculus.com/documentation/native/android/mobile-device-setup/.
* When the device is in developer mode you should be able to just press Build and Run in Unity and it should build and push the latest version built to the device!
# How to use?
When you have the app up and running you should just be able to launch it on a second device and the players should appear in the same environment.
* To use the laser pointer, use your left hand and aim at anything in the VR world, press down the left trigger and the laser pointer should appear.
* To begin drawing, use your right hand and press down the right trigger to start drawing!
* Voice chat is automatically enabled when two or more users are in the same room, and you can communicate with each other over the network.