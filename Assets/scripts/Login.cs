using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Login : MonoBehaviour {
	public GameObject FlexiableView;
	public Camera MainCamera;
	public InputField PhoneNumInputField;
	public InputField PasswordInputField;

	public InputField ForgetPasswordPhoneNumInputField;
	public InputField ForgetPasswordVarifyCodeInputField;
	public InputField ForgetPasswordNewPasswordInputField;
	public InputField ForgetPasswordComfirmNewPasswordInputField;

	public Text LoginerrorText;
	public Text ForgetPasswordText;

	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void loginSubmit(){
		string EncodedPassword = Md5Sum (PasswordInputField.text);
		string url = "http://localhost:8080/poker/api/user/login?username="+int.Parse(PhoneNumInputField.text)+"&password="+ EncodedPassword;
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	public void forgetPasswordSubmit(){


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

	public void getVariyCode(){
		string url = "http://localhost:8080/poker/api/user/getVerifyCode?phone="+int.Parse(ForgetPasswordPhoneNumInputField.text);
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	public void varifyLoginInput(){
		if (int.Parse(PhoneNumInputField.text) == null || PasswordInputField.text == "") {
			LoginerrorText.text = "用户名或密码不能为空";
		}
		if (PasswordInputField.text.Length <= 6) {
			LoginerrorText.text = "密码长度需大于六位";
		} else {
			LoginerrorText.text = "";
			FlexiableView.GetComponent<AlertController>().hideall();
			MainCamera.GetComponent<CameraController>().setisLogin();
		}
	}

	public void varifyForgetPasswordInput(){
		if (int.Parse (ForgetPasswordPhoneNumInputField.text) == null || 
			ForgetPasswordVarifyCodeInputField.text == "" ||
			ForgetPasswordNewPasswordInputField.text == "") {
			ForgetPasswordText.text = "手机号、验证码或密码不能为空";
		} else if (int.Parse (ForgetPasswordPhoneNumInputField.text) != null && ForgetPasswordNewPasswordInputField.text.Length <= 6) {
			ForgetPasswordText.text = "密码长度需大于六位";
		} else if (!ForgetPasswordNewPasswordInputField.text.Equals( ForgetPasswordComfirmNewPasswordInputField.text) ) {
			print ("comfirm problem");
			ForgetPasswordText.text = "两次密码输入不一致";
		} else {
			ForgetPasswordText.text = "";
			FlexiableView.GetComponent<AlertController>().switchbetwenforgetPassword();
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
