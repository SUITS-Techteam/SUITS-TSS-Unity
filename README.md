# SUITS-TSS-Unity
Repo for the TSS Unity package. As of now, it provides classes for the data you will receive from the TSS, as well as the TSSConnection class, which is just a wrapper around NativeWebSockets which also deserializes messages from the TSS using Unity's JSONUtility. As the TSS is updated, this repo will also be updated to handle authorization, any new data the TSS sends, and anything else necessary to connect the HMD to the TSS. It should work in both the Unity editor and as UWP apps on the Hololens2.

Feel free to use it directly in your project, or just to get an idea of how you can go about communicating with the TSS.

# Quick Start
1. Add [NativeWebSocket](https://github.com/endel/NativeWebSocket) to your project following the directions it provides.
2. Add this package to your unity project. You could open the Unity Package Manager, chose to install from git url, and paste `https://github.com/SUITS-Techteam/SUITS-TSS-Unity.git` in when prompted. Or you can clone the repo and simply copy and paste it into your projects Asset's folder.
3. You should be able to use the package in your project now.
4. To look at a sample, import the sample called SampleTSSScene (you can import samples from Unity Package Manager or copy and paste the sample files)
5. Add the scene titled `SampleScene` from `Samples/SampleTSSScene/SampleScene.unity` to your project.
Replace the missing script with ConnScript
6. The sample should be ready to run. It just has a canvas with a text input field, a button, and some text boxes. If you are running the TSS Socket Server on the same device, leave the input field as it is and press the Connect button. If the TSS is running on another device on the network, type the ip address of the device into the input field, and then press the Connect button.
7. The project should connect to the TSS over websockets and start receiving data. On the TSS side, the socket server should print that a user has connected. The TSS should start sending data, and the data will be displayed as text in Unity.
