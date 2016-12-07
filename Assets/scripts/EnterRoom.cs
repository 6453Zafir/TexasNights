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
	private int operation = 0;


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

	string sitdownData = @"
        {
			""code"":101,
			""data"":{
   	   	 	}
		}
        ";
	string standupData = @"
        {
			""code"":102,
			""data"":{
   	   	 	}
		}
        ";
	string leaveData = @"
        {
			""code"":103,
			""data"":{
   	   	 	}
		}
        ";
	string changeroomData = @"
        {
			""code"":104,
			""data"":{
    			""type"":1
   	   	 	}
		}
        ";

	string followStake = @"
		{
			 ""code"":108, 
			""data"":null
		}
		";

	public void ClientSend(){
		string token = GameManager.instance.userOj.token;
		enterRoomAddress = "ws://139.224.59.3:8080/poker/websocket/"+token;
		websocket = new WebSocket (enterRoomAddress);
		websocket.Opened += new EventHandler (websocket_enterRoom);
		websocket.Opened += new EventHandler (websocket_followStake);
		websocket.Closed += new EventHandler (websocket_Closed);
		websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs> (websocket_MessageReceived);
		websocket.Open ();
	}


	private void websocket_enterRoom(object sender,EventArgs e){

		websocket.Send (enterRoomData);
	}

	private void websocket_followStake(object sender,EventArgs e){
		websocket.Send (followStake);
	}
	private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
	{
		Debug.Log (e.Message);
		message.text = e.Message;
	}

	private void websocket_Closed(object sender, EventArgs e){
		websocket.Close();
	}

	
}
