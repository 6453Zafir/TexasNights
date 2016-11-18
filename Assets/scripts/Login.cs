using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Login : MonoBehaviour {
	public GameObject FlexiableView;
	public Camera MainCamera;
	public GameObject AvatarButton;
	public const string LAYER_NAME = "element";

	public InputField PhoneNumInputField;
	public InputField PasswordInputField;

	public InputField ForgetPasswordPhoneNumInputField;
	public InputField ForgetPasswordVarifyCodeInputField;
	public InputField ForgetPasswordNewPasswordInputField;
	public InputField ForgetPasswordComfirmNewPasswordInputField;

	public InputField RegisterPhoneNumInputField;
	public InputField RegisterVarifyCodeInputField;
	public InputField RegisterPasswordInputField;
	public InputField RegisterComfirmNewPasswordInputField;

	public InputField FillInfoNicknameInputField;
	public ToggleGroup GenderToggle;


	public Text LoginerrorText;
	public Text ForgetPasswordText;
	public Text RegistererrorText;
	public Text FillInfoerrerText;

	private Object [] sprites;
	private GameObject avatar;
	private SpriteRenderer renderer;

	void Start(){
		//头像按钮
		avatar = new GameObject ("avatar");
		avatar.transform.parent = AvatarButton.transform;
		avatar.transform.position = AvatarButton.transform.position;
		avatar.transform.localScale = new Vector3 (5.6f, 5.4f, 5f);
		avatar.layer = 8;
		if (avatar != null) {
			renderer = avatar.AddComponent<SpriteRenderer> ();
			sprites = Resources.LoadAll ("avatars");
			renderer.sortingLayerName = LAYER_NAME;
			renderer.sortingOrder = 1;
		} else {
			print("the avatar havn't newed");
		}

	}

	//登录提交
	public void loginSubmit(){
		string EncodedPassword = Md5Sum (PasswordInputField.text);
		string url = "http://localhost:8080/poker/api/user/login?username="+int.Parse(PhoneNumInputField.text)+"&password="+ EncodedPassword;
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	//获取验证码
	public void getVariyCode(){
		string url = "http://localhost:8080/poker/api/user/getVerifyCode?phone="+int.Parse(ForgetPasswordPhoneNumInputField.text);
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	//环头像
	public void changeAvatar(){
		if (GameManager.instance.AvatarNum < sprites.Length-1) {
			renderer.sprite = (Sprite)sprites [++GameManager.instance.AvatarNum];
		}else{
			GameManager.instance.AvatarNum = 1;
			renderer.sprite = (Sprite)sprites [GameManager.instance.AvatarNum];
		}
	}
		
	//忘记密码提交
	public void forgetPasswordSubmit(){
		
	}

	//注册提交
	public void registerSubmit(){
		
	} 


	//登录表单验证
	public void varifyLoginInput(){
		if (int.Parse(PhoneNumInputField.text) == null || PasswordInputField.text == "") {
			LoginerrorText.text = "手机号或密码不能为空";
		}
		if (PasswordInputField.text.Length <= 6) {
			LoginerrorText.text = "密码长度需大于六位";
		} else {
			LoginerrorText.text = "";
			FlexiableView.GetComponent<AlertController>().hideall();
			GameManager.instance.isLogin = true;
			GameManager.instance.InfoOpen = false;
		}
	}

	//忘记密码表单验证
	public void varifyForgetPasswordInput(){
		if (int.Parse (ForgetPasswordPhoneNumInputField.text) == null || 
			ForgetPasswordVarifyCodeInputField.text == "" ||
			ForgetPasswordNewPasswordInputField.text == "") {
			ForgetPasswordText.text = "手机号、验证码或密码不能为空";
		} else if (int.Parse (ForgetPasswordPhoneNumInputField.text) != null && ForgetPasswordNewPasswordInputField.text.Length <= 6) {
			ForgetPasswordText.text = "密码长度需大于六位";
		} else if (!ForgetPasswordNewPasswordInputField.text.Equals( ForgetPasswordComfirmNewPasswordInputField.text) ) {
			ForgetPasswordText.text = "两次密码输入不一致";
		} else {
			ForgetPasswordText.text = "";
			FlexiableView.GetComponent<AlertController>().switchbetwenforgetPassword();
		}
	}

	//注册表单验证
	public void varifyRegisterInput(){
		if (int.Parse (RegisterPhoneNumInputField.text) == null || 
		    RegisterVarifyCodeInputField.text == "" ||
		    RegisterPasswordInputField.text == "") {
			RegistererrorText.text = "手机号、验证码或密码不能为空";
		} else if (int.Parse (RegisterPasswordInputField.text) != null && RegisterPasswordInputField.text.Length <= 6) {
			RegistererrorText.text = "密码长度需大于六位";
		} else if (!RegisterPasswordInputField.text.Equals( RegisterComfirmNewPasswordInputField.text) ) {
			RegistererrorText.text = "两次密码输入不一致";
		} else {
			RegistererrorText.text = "";
			GameManager.instance.isRegister = true;
			FlexiableView.GetComponent<AlertController>().toFillInfomation();
		}
	}

	//完善信息表单验证
	public void varifyInfoInput(){
		if (FillInfoNicknameInputField.text == null || GenderToggle.AnyTogglesOn() == false 
		    ||GameManager.instance.AvatarNum<1||GameManager.instance.AvatarNum>6) {
			FillInfoerrerText.text = "昵称、头像或性别不能为空";
		}
		if (FillInfoNicknameInputField.text.Length <= 6) {
			FillInfoerrerText.text = "昵称长度需大于六位";
		} else {
			FillInfoerrerText.text = "";
			FlexiableView.GetComponent<AlertController>().hideall();
			if (GameManager.instance.isRegister) {
				print("信息已完善");
				GameManager.instance.isInfoFilled = true;
				GameManager.instance.InfoOpen = false;
			} else {
				print("还未注册手机号");
			}
		}		
	}

	//Md5密码加密
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

	//网络请求等待
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
