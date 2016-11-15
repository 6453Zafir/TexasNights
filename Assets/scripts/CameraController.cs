using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour {
	public GameObject FlexableCanvas;
	private Animator CamAniController;

	void Start () {	
		CamAniController = GetComponent<Animator>();
	}

	void Update () {
		print ("singleton isStarted" + GameManager.instance.isStarted);
		GameObject infopanelarea = FlexableCanvas.transform.GetChild (2).gameObject.transform.GetChild(0).gameObject;
		GameObject friendpanelarea = FlexableCanvas.transform.GetChild (3).gameObject.transform.GetChild(0).gameObject;
		GameObject settingpanelarea = FlexableCanvas.transform.GetChild (4).gameObject.transform.GetChild(0).gameObject;
		GameObject roompanelarea = FlexableCanvas.transform.GetChild (5).gameObject.transform.GetChild(0).gameObject;

		GameManager.instance.isStarted = CamAniController.GetBool ("isStarted");
		GameManager.instance.InfoOpen = CamAniController.GetBool ("InfoOpen");
		GameManager.instance.friendListOpen = CamAniController.GetBool ("friendListOpen");
		GameManager.instance.settingOpen = CamAniController.GetBool ("settingListOpen");
		GameManager.instance.richlist = CamAniController.GetBool ("richListOpen");
		GameManager.instance.store = CamAniController.GetBool("storeOpen");
		GameManager.instance.isChooseOpen = FlexableCanvas.GetComponent<AlertController> ().isChooseOpen;	 

		if(GameManager.instance.isStarted){
			turnOnEffects();
			print ("isStarted "+GameManager.instance.isStarted+"isRegister"+GameManager.instance.isRegister+"isLoginViewAlerted"+GameManager.instance.isRegister);
			if(GameManager.instance.isLogin||GameManager.instance.isRegister){
				turnOffEffects();
				if(GameManager.instance.InfoOpen){
					turnOnEffects();
					HideAlertIfClickedOutside(infopanelarea);
				}else if(GameManager.instance.settingOpen){
					turnOnEffects();
					HideAlertIfClickedOutside(settingpanelarea);
				}else if(GameManager.instance.friendListOpen){
					turnOnEffects();
					HideAlertIfClickedOutside(friendpanelarea);
				}else if(GameManager.instance.isChooseOpen){
					turnOnEffects();
					HideAlertIfClickedOutside(roompanelarea);
				}else{
					Debug.Log("Noting opened");
				}
			}else if(!GameManager.instance.isLogin && !GameManager.instance.isLoginViewAlerted){
				Invoke("ZoomToInfo",0);
				FlexableCanvas.GetComponent<AlertController>().alertLogin();
				GameManager.instance.isLoginViewAlerted = true;
				Debug.Log("start the game but haven't login");					
				}
		}else{
			if(GameManager.instance.isLogin){
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
		GameManager.instance.isLogin = true;
		CamAniController.SetBool ("InfoOpen", false);
	}
	public void setisRegister(){
		GameManager.instance.isRegister = true;
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
