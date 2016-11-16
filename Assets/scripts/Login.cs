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
	}
	public void loginSubmit(){
		string EncodedPassword = Md5Sum (loginPassword);
		string url = "http://localhost:8080/poker/api/user/login?username="+loginPhoneNum+"&password="+ EncodedPassword;
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

	public string Md5Sum(string password)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(password);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}
}
