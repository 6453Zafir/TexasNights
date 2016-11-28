using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;

public class PlayController : MonoBehaviour {
	public GameObject PlayerRoot;
	public Transform[] Players = new Transform[6];

	public GameObject People;
	private GameObject[] playerPoker = new GameObject[6];
	private int currentPlayerNum = 1; 

	private Animator PlayTimeAnimation;
	private bool isDealed = false;

	// Use this for initialization
	void Start () {
		PlayTimeAnimation = GetComponent<Animator>();
		for (int i = 0; i<PlayerRoot.transform.childCount; i++) {
			Players[i] = PlayerRoot.transform.GetChild(i).transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//allReady = PlayTimeAnimation.GetBool("isStarted");

		if (true) {
			PlayTimeAnimation.SetBool("isStarted",true);
			if(!isDealed){
				Invoke("deal",0);
				Invoke("movePeopleArm",0);
				isDealed = true;
			}else{
				Debug.Log("Everyobdy has their cards now");
			}	
		} else {
			//等待玩家
			PlayTimeAnimation.SetBool("isStarted",false);
			Debug.Log("Can not continue, lack of player");
		}
	}





	public void deal(){
		for (int i = 0; i < Players.Length; i++) {
			if(i==3){
				playerPoker [i] = Instantiate (Resources.Load ("PokerFront"))as GameObject;
				//playerPoker[i].transform.GetChild(0).GetComponent<Image>()
				playerPoker [i].transform.SetParent(Players[i],false);
				playerPoker [i].transform.localPosition = new Vector3 (70, 20, 0);
			}else{
				playerPoker [i] = Instantiate (Resources.Load ("PokerBack"))as GameObject;
				playerPoker[i].transform.SetParent(Players[i],false);
				playerPoker [i].transform.localPosition = new Vector3 (50, -20, 0);
			}
		}
		return;
	}
	public void movePeopleArm(){
		Animation moveArm = People.transform.GetChild (2).transform.GetComponent<Animation> ();
		moveArm.Play ();
		moveArm["moveArm"].speed = 1;
		Debug.Log("arm should move now");
		return;
	}

	public void EnterRoom(){
		TcpClient client = new TcpClient ();
		try{
			client.Connect(IPAddress.Parse("139.224.59.3"),8080);
		}catch(Exception ex){
			Debug.Log("客户端连接异常"+ex.Message);
		}

	}

}
