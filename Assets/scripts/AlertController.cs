using UnityEngine;
using System.Collections;

public class AlertController : MonoBehaviour {
	public int s = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void alertInfo(){
		foreach (Transform child in transform)
			if (child.name == "InfoView") {
				child.gameObject.SetActive (true);
			Animation a =  child.GetComponent<Animation>();
			a.Play();
			a["infoBoard"].speed = 2;	
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
			a["friendlistBoard"].speed = 2;	
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
			a["settingBoard"].speed = 2;	
		} else {
			child.gameObject.SetActive(false);
		}
	}
	public void hideall() {
		foreach (Transform child in transform)
			child.gameObject.SetActive(false);
	}

}
