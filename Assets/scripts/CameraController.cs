using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour {
	public float greetingScreen = 85f;
	public float fullScreen = 375f;
	public float boardScreen = 237f;
	public float secsToNext= 0.01f;
	public GameObject FlexableCanvas;

	private bool isLogin = false;
	private bool isRegister = false;
	private bool isLoginViewAlerted = false;
	private bool isStarted = false;
	private bool InfoOpen = false;
	private bool friendListOpen = false;
	private bool settingOpen = false;
	private bool richlist = false; 
	private bool store = false;
	private bool isChooseOpen = false;
	private Animator CamAniController;


	void Start () {	
		CamAniController = GetComponent<Animator>();
	}

	void Update () {

		GameObject infopanelarea = FlexableCanvas.transform.GetChild (2).gameObject.transform.GetChild(0).gameObject;
		GameObject friendpanelarea = FlexableCanvas.transform.GetChild (3).gameObject.transform.GetChild(0).gameObject;
		GameObject settingpanelarea = FlexableCanvas.transform.GetChild (4).gameObject.transform.GetChild(0).gameObject;
		GameObject roompanelarea = FlexableCanvas.transform.GetChild (5).gameObject.transform.GetChild(0).gameObject;
		isStarted = CamAniController.GetBool ("isStarted");
		InfoOpen = CamAniController.GetBool ("InfoOpen");
		friendListOpen = CamAniController.GetBool ("friendListOpen");
		settingOpen = CamAniController.GetBool ("settingListOpen");
		richlist = CamAniController.GetBool ("richListOpen");
		store = CamAniController.GetBool("storeOpen");
		isChooseOpen = FlexableCanvas.GetComponent<AlertController> ().isChooseOpen;	 

		if(isStarted){
			turnOnEffects();
			if(isLogin||isRegister){
				turnOffEffects();
				if(InfoOpen){
					turnOnEffects();
					HideAlertIfClickedOutside(infopanelarea);
				}else if(settingOpen){
					turnOnEffects();
					HideAlertIfClickedOutside(settingpanelarea);
				}else if(friendListOpen){
					turnOnEffects();
					HideAlertIfClickedOutside(friendpanelarea);
				}else if(isChooseOpen){
					turnOnEffects();
					HideAlertIfClickedOutside(roompanelarea);
				}else{
					Debug.Log("Noting opened");
				}
			}else if(!isLogin && !isLoginViewAlerted){
				Invoke("ZoomToInfo",0);
				FlexableCanvas.GetComponent<AlertController>().alertLogin();
				isLoginViewAlerted = true;
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

	public void RichListBackToMain(){
		CamAniController.SetBool ("richListOpen", false);
	}
	public void StoreBackToMain(){
		Debug.Log("backed!!!!!!!");
		CamAniController.SetBool ("storeOpen", false);
	}

	private void HideAlertIfClickedOutside(GameObject panel) {
		if (Input.GetMouseButton(0) && panel.activeSelf && 
		    !RectTransformUtility.RectangleContainsScreenPoint(
			panel.GetComponent<RectTransform>(), 
			Input.mousePosition, 
			Camera.main)) {
			panel.SetActive(false);
			GameObject panelParent = panel.transform.parent.gameObject;
			panelParent.SetActive(false);
			if(panelParent.name == "InfoView"){
				CamAniController.SetBool ("InfoOpen", false);
				turnOffEffects();
			}else if(panelParent.name == "FriendListView"){
				CamAniController.SetBool ("friendListOpen", false);
				turnOffEffects();
			}else if(panelParent.name == "SettingView"){
				CamAniController.SetBool ("settingListOpen", false);
				turnOffEffects();
			}else if(panelParent.name == "ChooseRoomView"){
				FlexableCanvas.GetComponent<AlertController>().isChooseOpen = false;
				FlexableCanvas.GetComponent<AlertController>().hideall();
				turnOffEffects();
			}else{
				Debug.Log("nothing opened");
			}
		}
	}
	 
	public void setisLogin(){
		isLogin = true;
		//放大相机至全局
		CamAniController.SetBool ("InfoOpen", false);
	}
	public void setisRegister(){
		isRegister = true;
		CamAniController.SetBool ("InfoOpen", false);
	}

	public void turnOnEffects(){
		GetComponent<Blur>().enabled = true;
		GetComponent<VignetteAndChromaticAberration>().enabled = true;
	}
	public void turnOffEffects(){
		GetComponent<Blur>().enabled = false;
		GetComponent<VignetteAndChromaticAberration>().enabled = false;
	}
}
