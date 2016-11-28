using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;

public class EnterRoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void enterRoom(){
		TcpClient client = new TcpClient ();
		try{
			client.Connect(IPAddress.Parse("139.224.59.3"),8080);
		}catch(Exception ex){
			Debug.Log("客户端连接异常"+ex.Message);
		}
		
	}

}
