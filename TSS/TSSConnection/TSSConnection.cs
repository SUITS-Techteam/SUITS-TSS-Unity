using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

using NativeWebSocket;

namespace TSS {
    public delegate void TSSDeserializedTelemetryMsgEventHandler(TSS.Msgs.TSSMsg deserializedMsgFromTSS);
    public delegate void TSSConnectionMsgEventHandler(string connectionMsg);
    public class TSSConnection
    {
        string uri;
        private WebSocket websocket = null;
        public TSS.Msgs.HMDInfo hmd_info;
        public TSS.Msgs.HMDRegistration hmd_registration;
        public event TSSDeserializedTelemetryMsgEventHandler OnTSSTelemetryMsg;
        public event TSSConnectionMsgEventHandler OnTSSConnectionMsg;
        public event WebSocketCloseEventHandler OnClose;
        public event WebSocketErrorEventHandler OnError;
        public event WebSocketOpenEventHandler OnOpen;

        public void Update()
        {
            if (websocket == null)
            {
                return;
            }

#if !UNITY_WEBGL || UNITY_EDITOR
            if (websocket.State == WebSocketState.Open)
            {
                websocket.DispatchMessageQueue();
            }
#endif
        }

        public void SetURI(string uri)
        {
            this.uri = uri;
        }

        private async void OnApplicationQuit()
        {
            await websocket.Close();
        }

        public async Task ConnectToURI(string uri, string team_name, string username, string university, string user_guid)
        {
            this.uri = uri;
            websocket = new WebSocket(uri);

            this.hmd_info = new TSS.Msgs.HMDInfo(team_name, username, university, user_guid);
            this.hmd_registration = new TSS.Msgs.HMDRegistration(hmd_info);

            Debug.Log("HMD Registration: " + JsonUtility.ToJson(this.hmd_registration, prettyPrint: true));

            websocket.OnOpen += () =>
            {
                websocket.Send(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(this.hmd_registration)));
                OnOpen?.Invoke();
            };

            websocket.OnError += (e) =>
            {
                OnError?.Invoke(e);
            };

            websocket.OnClose += (e) =>
            {
                OnClose?.Invoke(e);
            };

            websocket.OnMessage += (bytes) =>
            {
                // getting the message as a string
                var message = System.Text.Encoding.UTF8.GetString(bytes);

                Debug.Log("received telemetry");

                // The following null-checks OnTelemetryMsg, deserializes the message into a TSSMsg object, and raises the OnTSSTelemetryMsg event
                OnTSSTelemetryMsg?.Invoke(JsonUtility.FromJson<TSS.Msgs.TSSMsg>(message));


            };

            // waiting for messages
            await websocket.Connect();
        }

        public void SendRoverNavigateCommand(float lat, float lon)
        {
            TSS.Msgs.RoverNavigateMsg rover_navigate_msg = new TSS.Msgs.RoverNavigateMsg(lat, lon);
            websocket.Send(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(rover_navigate_msg)));
        }

        public void SendRoverRecallCommand()
        {
            TSS.Msgs.RoverRecallMsg rover_recall_msg = new TSS.Msgs.RoverRecallMsg();
            websocket.Send(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(rover_recall_msg)));
        }
    }
}