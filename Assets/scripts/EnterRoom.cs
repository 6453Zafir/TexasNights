using UnityEngine;
using System.Collections;

using WebSocket4Net;
using System.Net.Sockets;

using WebSocket4Net.Protocol;
using System;
using LitJson;
using UnityEngine.UI;

public class enterRoom : MonoBehaviour {
	public Text message;
	private string token;
	private string enterRoomAddress;


	private string userToken;
	public WebSocket websocket;

	string enterRoomData = @"
        {
			""code"":100,
			""data"":{
    			""type"":1
   	   	 	}
		}
        ";

	public void ClientSend(){
		string token = GameManager.instance.userOj.token;
		enterRoomAddress = "ws://139.224.59.3:8080/poker/websocket/"+token;
		websocket = new WebSocket (enterRoomAddress);
		websocket.Opened += new EventHandler (websocket_Opened);
		websocket.Closed += new EventHandler (websocket_Closed);
		websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs> (websocket_MessageReceived);
		websocket.Open ();
	}


	private void websocket_Opened(object sender,EventArgs e){
		websocket.Send (enterRoomData);
	}

	private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
	{
		//Debug.Log ("message from the server: " + e.Message);
		message.text = e.Message;
	}

	private void websocket_Closed(object sender, EventArgs e){
		websocket.Close();
	}
}
