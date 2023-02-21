using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS;

public class TSSConnectionSample : MonoBehaviour
{
    TSSConnection tss;

    // Start is called before the first frame update
    async void Start()
    {
        tss = new TSSConnection();
        var connecting = tss.ConnectToURI("ws://localhost:3001");

        // Create a function that takes asing TSSMsg parameter and returns void. For example:
        // public static void PrintInfo(TSS.Msgs.TSSMsg tssMsg) { ... }
        // Then just subscribe to the OnTSSTelemetryMsg
        tss.OnTSSTelemetryMsg += PrintInfo;
        
        // tss.OnOpen, OnError, and OnClose events just re-raise events from websockets.
        // Similar to OnTSSTelemetryMsg, create functions with the appropriate return type and parameters, and subscribe to them
        tss.OnOpen += () =>
        {
            Debug.Log("Websocket Opened");
        };

        tss.OnError += (string e) =>
        {
            Debug.Log("Websocket error: " + e);
        };

        tss.OnClose += (e) =>
        {
            Debug.Log("Websocket closed with close code " + e);
        };

        await connecting;
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the websocket once per frame
        tss.Update();
    }

    // An example handler for the OnTSSMsgReceived event which just serializes to JSON and prints it all out
    // Can be any function that returns void and has a single parameter of type TSS.Msgs.TSSMsg
    public static void PrintInfo(TSS.Msgs.TSSMsg tssMsg)
    {
        Debug.Log("Received the following telemetry data from the TSS:\n" + JsonUtility.ToJson(tssMsg, prettyPrint:true) ) ;
    }
}