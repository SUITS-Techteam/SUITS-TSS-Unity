using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS;

public class ConnScript : MonoBehaviour
{
    TSSConnection tss;

    int msgCount = 0;

    TMPro.TMP_Text statusBox;
    TMPro.TMP_Text gpsMsgBox;
    TMPro.TMP_Text imuMsgBox;
    TMPro.TMP_Text evaMsgBox;

    TMPro.TMP_InputField inputField;

    string openStatus = "";
    string connectionStatus = "";
    string errorStatus = "";
    string closeStatus = "";
    string tssUri = "";

    // Start is called before the first frame update
    async void Start()
    {
        tss = new TSSConnection();
        inputField = GameObject.Find("Socket URI Input Field").GetComponent<TMPro.TMP_InputField>();

        //statusBox = GameObject.Find("Status box").GetComponent<TMPro.TMP_Text>();
        statusBox = null;
        gpsMsgBox = GameObject.Find("GPS Msg Box").GetComponent<TMPro.TMP_Text>();
        imuMsgBox = GameObject.Find("IMU Msg Box").GetComponent<TMPro.TMP_Text>();
        evaMsgBox = GameObject.Find("EVA Msg Box").GetComponent<TMPro.TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        // Updates the websocket once per frame
        tss.Update();
        
        if (statusBox == null) return;
        statusBox.text =
            "tssUri: " + tssUri + "\n" +
            "openStatus: " + openStatus + "\n" +
            "connectionStatus" + connectionStatus + "\n" +
            "errorStatus: " + errorStatus + "\n" +
            "closeStatus: " + closeStatus + "\n" +
            "msg #: " + msgCount + "\n";

    }

    public async void Connect()
    {
        tssUri = inputField.text;
        var connecting = tss.ConnectToURI(tssUri);
        Debug.Log("Connecting to " + tssUri);
        // Create a function that takes asing TSSMsg parameter and returns void. For example:
        // public static void PrintInfo(TSS.Msgs.TSSMsg tssMsg) { ... }
        // Then just subscribe to the OnTSSTelemetryMsg
        tss.OnTSSTelemetryMsg += (telemMsg) =>
        {
            msgCount++;
            Debug.Log("msgCount: " + msgCount);

            if (telemMsg.gpsmsgs.Count > 0)
            {
                gpsMsgBox.text = "GPS Msg: " + JsonUtility.ToJson(telemMsg.gpsmsgs[0], prettyPrint: true);
            } else
            {
                gpsMsgBox.text = "No GPS Msg received";
            }

            if (telemMsg.imumsgs.Count > 0)
            {
                imuMsgBox.text = "IMU Msg: " + JsonUtility.ToJson(telemMsg.imumsgs[0], prettyPrint: true);
            }
            else
            {
                imuMsgBox.text = "No IMU Msg received";
            }

            if (telemMsg.simulationstates.Count > 0)
            {
                evaMsgBox.text = "EVA Msg: " + JsonUtility.ToJson(telemMsg.simulationstates[0], prettyPrint: true);
            }
            else
            {
                evaMsgBox.text = "No EVA Msg received";
            }
        };

        // tss.OnOpen, OnError, and OnClose events just re-raise events from websockets.
        // Similar to OnTSSTelemetryMsg, create functions with the appropriate return type and parameters, and subscribe to them
        tss.OnOpen += () =>
        {
            openStatus = "Open";
        };

        tss.OnError += (string e) =>
        {
            errorStatus = "Error occured: " + e;
        };

        tss.OnClose += (e) =>
        {
             closeStatus = "Closed with code: " + e;
        };

        await connecting;

    }

    public void UpdateUri()
    {
        tssUri = inputField.text;
    }

    // An example handler for the OnTSSMsgReceived event which just serializes to JSON and prints it all out
    // Can be any function that returns void and has a single parameter of type TSS.Msgs.TSSMsg
    public static void PrintInfo(TSS.Msgs.TSSMsg tssMsg)
    {
        Debug.Log("Received the following telemetry data from the TSS:\n" + JsonUtility.ToJson(tssMsg, prettyPrint: true));
    }
}