using UnityEngine;
using System.Collections;

public class AlertController : MonoBehaviour {

	public bool isChooseOpen = false;
	public void alertLogin(){
		foreach (Transform child in transform)
		if (child.name == "LoginView") {
			child.gameObject.SetActive (true);
			Animation a =  child.GetComponent<Animation>();
			a.Play();
			a["infoBoard"].speed = 3;
			child.transform.GetChild(0).gameObject.SetActive(true);
		} else {
			child.gameObject.SetActive(false);
		}
	}

	public void switchbetwenforgetPassword(){
		GameObject loginpanel = transform.GetChild (0).gameObject;
		if (loginpanel.activeSelf) {
			if(loginpanel.transform.GetChild(0).gameObject.activeSelf){
				loginpanel.transform.GetChild(0).gameObject.SetActive(false);
				loginpanel.transform.GetChild(1).gameObject.SetActive(true);
			}else{
				loginpanel.transform.GetChild(0).gameObject.SetActive(true);
				loginpanel.transform.GetChild(1).gameObject.SetActive(false);
			}
		} else {
			Debug.Log("Login panel didn't open");
		}
	}
	public void switchbetwenRegister(){
		GameObject loginpanel = transform.GetChild (0).gameObject;
		GameObject registerpanel = transform.GetChild (1).gameObject;
		if (loginpanel.activeSelf) {
			loginpanel.SetActive(false);
			registerpanel.SetActive(true);
			registerpanel.transform.GetChild(0).gameObject.SetActive(true);
		} else {
			loginpanel.SetActive(true);
			registerpanel.SetActive(false);
			loginpanel.transform.GetChild(0).gameObject.SetActive(true);
			loginpanel.transform.GetChild(1).gameObject.SetActive(false);
		}
	}

	public void toFillInfomation(){
		GameObject registerpanel = transform.GetChild (1).gameObject;
		if (registerpanel.activeSelf) {
			if(registerpanel.transform.GetChild(0).gameObject.activeSelf){
				registerpanel.transform.GetChild(0).gameObject.SetActive(false);
				registerpanel.transform.GetChild(1).gameObject.SetActive(true);
			}else{
				Debug.Log("haven't register");
			}
		}
	}

	public void alertInfo(){
		foreach (Transform child in transform)
			if (child.name == "InfoView") {
				child.gameObject.SetActive (true);
				Animation a =  child.GetComponent<Animation>();
				a.Play();
				a["infoBoard"].speed = 3;
			child.transform.GetChild(0).gameObject.SetActive(true);
			} else {
				child.gameObject.SetActive(false);
			}
	}

	public void alertFriendList(){
		foreach (Transform child in transform)
		if (child.name == "FriendListView") {
			child.gameObject.SetActive (true);
			Animation a =  child.GetComponent<Animation>();
			a.Play();
			a["friendlistBoard"].speed = 3;	
			child.transform.GetChild(0).gameObject.SetActive(true);
		} else {
			child.gameObject.SetActive(false);
		}
	}

	public void alertSetting(){
		foreach (Transform child in transform)
		if (child.name == "SettingView") {
			child.gameObject.SetActive (true);
			Animation a =  child.GetComponent<Animation>();
			a.Play();
			a["settingBoard"].speed = 3;	
			child.transform.GetChild(0).gameObject.SetActive(true);
		} else {
			child.gameObject.SetActive(false);
		}
	}

	public void alertChoose(){
		if (!isChooseOpen) {
			foreach (Transform child in transform)
			if (child.name == "ChooseRoomView") {
				child.gameObject.SetActive (true);
				Animation a =  child.GetComponent<Animation>();
				a.Play();
				a["chooseRoomBoard"].speed = 3;	
				isChooseOpen = true;
				child.transform.GetChild(0).gameObject.SetActive(true);
			} else {
				child.gameObject.SetActive(false);
			}
		}
	}

	public void hideall() {
		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
		}
	}

	public void toPlayMode(){
		Application.LoadLevel("table");
	}

	public void loginSubmit(){
		string url = "http://localhost:8080/poker/api/user/login?username=123123&password=312312";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
}
