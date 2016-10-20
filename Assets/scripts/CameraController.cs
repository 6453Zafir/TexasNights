using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float greetingScreen = 0.85f;
	public float fullScreen = 3.75f;
	public float boardScreen = 2.37f;
	public Camera MainMenucamera;
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
				GameObject.Find("InteractElements").GetComponent<InteractScripts>().ShowAllButton();			
			}else{
				Debug.Log("nothing opend");
			}
		}
	}

	public void ZoomToFullAnimation(){
		CamAniController.SetBool ("isStarted", true);
	}

	public void ZoomToInfoAnimation(){
		CamAniController.SetBool ("InfoOpen", true);
	}
	public void ZoomToFriendListAnimation(){
		CamAniController.SetBool ("friendListOpen", true);
	}
	public void ZoomToSettingAnimation(){
		CamAniController.SetBool ("settingListOpen", true);
	}
	public void SlideToRichlist(){
		CamAniController.SetBool ("richListOpen", true);
	}
	public void SlideToStore(){
		CamAniController.SetBool ("storeOpen", true);
	}


	public void ZoomoutToFull(){
		MainMenucamera.orthographicSize = fullScreen;
		transform.position = new Vector3(0, 0, 0);
	}
	public void ZoomToGreeting(){
		transform.position = new Vector3(-3.8f, 0.93f, 0);
		MainMenucamera.orthographicSize = greetingScreen;
	}

	public void ZoomToInfo(){
		transform.position = new Vector3(-2.84f, 1.39f, 0);
		MainMenucamera.orthographicSize = boardScreen;
	}
}
