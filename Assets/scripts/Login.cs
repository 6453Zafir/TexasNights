using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Login : MonoBehaviour {
	public GameObject FlexiableView;
	public Camera MainCamera;
	public InputField PhoneNumInputField;
	public InputField PasswordInputField;
	public Text errcodeText;
	private int loginPhoneNum;
	private string loginPassword;
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		loginPhoneNum = int.Parse(PhoneNumInputField.text);
		loginPassword = PasswordInputField.text;
		print (loginPhoneNum + loginPassword);
	}
	public void loginSubmit(){
		string url = "http://localhost:8080/poker/api/user/login?username=loginPhoneNum&password=loginPassword";
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

	public void varifyLoginInput(){
		if (loginPhoneNum == null || loginPassword == null) {
			errcodeText.text = "用户名或密码不能为空";
		}
		if (loginPassword.Length <= 4) {
			errcodeText.text = "密码长度需大于四位";
		} else {
			errcodeText.text = "";
			FlexiableView.GetComponent<AlertController>().hideall();
			MainCamera.GetComponent<CameraController>().setisLogin();
		}
	}
}
