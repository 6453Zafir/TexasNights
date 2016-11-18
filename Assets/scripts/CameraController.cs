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
		GameObject infopanelarea = FlexableCanvas.transform.GetChild (2).gameObject.transform.GetChild(0).gameObject;
		GameObject friendpanelarea = FlexableCanvas.transform.GetChild (3).gameObject.transform.GetChild(0).gameObject;
		GameObject settingpanelarea = FlexableCanvas.transform.GetChild (4).gameObject.transform.GetChild(0).gameObject;
		GameObject roompanelarea = FlexableCanvas.transform.GetChild (5).gameObject.transform.GetChild(0).gameObject;

		CamAniController.SetBool ("isStarted",GameManager.instance.isStarted) ;
		CamAniController.SetBool ("InfoOpen", GameManager.instance.InfoOpen);
		CamAniController.SetBool ("friendListOpen",GameManager.instance.friendListOpen);
		CamAniController.SetBool ("settingListOpen",GameManager.instance.settingOpen);
		CamAniController.SetBool ("richListOpen",GameManager.instance.richlist);
		CamAniController.SetBool ("storeOpen",GameManager.instance.store);
		FlexableCanvas.GetComponent<AlertController> ().isChooseOpen= GameManager.instance.isChooseOpen;

		if(GameManager.instance.isStarted){
			if(GameManager.instance.isLogin||GameManager.instance.isInfoFilled){
				turnOffBlurEffects();
				if(GameManager.instance.InfoOpen){
					turnOnBlurEffects();
					HideAlertIfClickedOutside(infopanelarea);
				}else if(GameManager.instance.settingOpen){
					turnOnBlurEffects();
					HideAlertIfClickedOutside(settingpanelarea);
				}else if(GameManager.instance.friendListOpen){
					turnOnBlurEffects();
					HideAlertIfClickedOutside(friendpanelarea);
				}else if(GameManager.instance.isChooseOpen){
					turnOnBlurEffects();
					HideAlertIfClickedOutside(roompanelarea);
				}else{
				}
			}else if(!GameManager.instance.isLogin && !GameManager.instance.isLoginViewAlerted){
				turnOnBlurEffects();
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
		GameManager.instance.isStarted = true;
	}
	public void ZoomToInfo(){
		GameManager.instance.InfoOpen = true;
	}
	public void ZoomToFriendList(){
		GameManager.instance.friendListOpen = true;
	}
	public void ZoomToSetting(){
		GameManager.instance.settingOpen = true;
	}
	public void SlideToRichlist(){
		GameManager.instance.richlist = true;
	}
	public void SlideToStore(){
		GameManager.instance.store= true;
	}

	public void RichListBackToMain(){
		GameManager.instance.richlist = false;
	}
	public void StoreBackToMain(){
		GameManager.instance.store = false;
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
				GameManager.instance.InfoOpen = false;
				turnOffBlurEffects();
			}else if(panelParent.name == "FriendListView"){
				GameManager.instance.friendListOpen = false;
				turnOffBlurEffects();
			}else if(panelParent.name == "SettingView"){
				GameManager.instance.settingOpen = false;
				turnOffBlurEffects();
			}else if(panelParent.name == "ChooseRoomView"){
				GameManager.instance.isChooseOpen = false;
				FlexableCanvas.GetComponent<AlertController>().hideall();
				turnOffBlurEffects();
			}else{
				Debug.Log("nothing opened");
			}
		}
	}
	/* 
	public void setisRegister(){
		GameManager.instance.isRegister = true;
		GameManager.instance.InfoOpen = false;
	}
	*/

	public void setisFillInfo(){

	}

	public void turnOnBlurEffects(){
		GetComponent<Blur>().enabled = true;
		GetComponent<VignetteAndChromaticAberration>().enabled = true;
	}
	public void turnOffBlurEffects(){
		GetComponent<Blur>().enabled = false;
		GetComponent<VignetteAndChromaticAberration>().enabled = false;
	}
}
