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

        public async Task ConnectToURI(string uri)
        {
            this.uri = uri;
            websocket = new WebSocket(uri);

            websocket.OnOpen += () =>
            {
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
                // getting the message as a string)
                var message = System.Text.Encoding.UTF8.GetString(bytes);
                if (message == "{\"event\":\"connection\",\"payload\":\"None\"}")
                {
                    OnTSSConnectionMsg?.Invoke(message);
                }

                // The following null-checks OnTelemetryMsg, deserializes the message into a TSSMsg object, and raises the OnTSSTelemetryMsg event
                // May be better off to just provide an example of using native websockets and deserializing, as well as classes for the tss message
                OnTSSTelemetryMsg?.Invoke(JsonUtility.FromJson<TSS.Msgs.TSSMsg>(message));
            };

            // waiting for messages
            await websocket.Connect();
        }

    }
    
}