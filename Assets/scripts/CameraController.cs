using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float greetingScreen = 85f;
	public float fullScreen = 375f;
	public float boardScreen = 237f;
	public Camera MainMenucamera;
	public GameObject FlexableCanvas;
	private Animator CamAniController;
	private bool isStarted = false;
	private bool InfoOpen = false;
	private bool friendListOpen = false;
	private bool settingOpen = false;
	private bool richlist = false; 
	private bool store = false;

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

		if (Input.GetMouseButtonDown(0)) {
			//Debug.Log ("isStarted "+isStarted + "   InfoOpen "+InfoOpen + "   friendListOpen "+friendListOpen);
			if(isStarted == true && InfoOpen == true || friendListOpen == true ||
			   settingOpen == true || richlist ==true || store == true ){
				CamAniController.SetBool ("InfoOpen", false);
				CamAniController.SetBool ("friendListOpen", false);
				CamAniController.SetBool ("settingListOpen", false);
				CamAniController.SetBool ("richListOpen", false);
				CamAniController.SetBool ("storeOpen", false);
				FlexableCanvas.GetComponent<AlertController>().hideall();
			}else{
				Debug.Log("nothing opend");
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

	/*
		public void ZoomoutToFull(){
			MainMenucamera.orthographicSize = fullScreen;
			transform.position = new Vector3(0, 0, 0);
		}
		public void ZoomToGreeting(){
			transform.position = new Vector3(-380f, 93f, -10);
			MainMenucamera.orthographicSize = greetingScreen;
		}
		public void ZoomToInfo(){
			transform.position = new Vector3(-284f, 139f, -10);
			MainMenucamera.orthographicSize = boardScreen;
	}
	*/
}
