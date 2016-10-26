using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float greetingScreen = 85f;
	public float fullScreen = 375f;
	public float boardScreen = 237f;
	public Camera MainMenucamera;
	public GameObject FlexableCanvas;
	public float secsToNext= 0.01f;

	private bool isLogin = false;
	private Animator CamAniController;
	private bool isStarted = false;
	private bool InfoOpen = false;
	private bool friendListOpen = false;
	private bool settingOpen = false;
	private bool richlist = false; 
	private bool store = false;
	private bool isChooseOpen = false;

	void Start () {	
		CamAniController = GetComponent<Animator>();
	}

	void Update () {
		isStarted = CamAniController.GetBool ("isStarted");
		InfoOpen = CamAniController.GetBool ("InfoOpen");
		friendListOpen = CamAniController.GetBool ("friendListOpen");
		settingOpen = CamAniController.GetBool ("settingListOpen");
		richlist = CamAniController.GetBool ("richListOpen");
		store = CamAniController.GetBool("storeOpen");
		isChooseOpen = FlexableCanvas.GetComponent<AlertController> ().isChooseOpen;	 
		if(isStarted){
			if(isLogin){
				if (Input.GetMouseButtonDown(0)){
					if(InfoOpen || friendListOpen || settingOpen || richlist || store || isChooseOpen ){
						CamAniController.SetBool ("InfoOpen", false);
						CamAniController.SetBool ("friendListOpen", false);
						CamAniController.SetBool ("settingListOpen", false);
						CamAniController.SetBool ("richListOpen", false);
						CamAniController.SetBool ("storeOpen", false);
						FlexableCanvas.GetComponent<AlertController>().isChooseOpen = false;
						FlexableCanvas.GetComponent<AlertController>().hideall();
					}else{
						Debug.Log("nothing opend");
						 }
					}
			}else{
				Invoke("ZoomToInfo",0);
				FlexableCanvas.GetComponent<AlertController>().alertInfo();
				isLogin = true;
				Debug.Log("start the game but haven't login");					
				}
		}else{
			if(isLogin){
				Debug.Log("has login but somehow didn't able to start the game");
			}else{
				Debug.Log("have't start the game :(");
			}
		}	
	}

	public void ZoomToFull(){
		CamAniController.SetBool ("isStarted", true);
	}
	public void ZoomToInfo(){
		CamAniController.SetBool ("InfoOpen", true);
	}
	public void ZoomToFriendList(){
		CamAniController.SetBool ("friendListOpen", true);
	}
	public void ZoomToSetting(){
		CamAniController.SetBool ("settingListOpen", true);
	}
	public void SlideToRichlist(){
		CamAniController.SetBool ("richListOpen", true);
	}
	public void SlideToStore(){
		CamAniController.SetBool ("storeOpen", true);
	}
	
}
