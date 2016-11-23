using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;

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

	//换头像
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

	//申请查看好友列表
	//查找好友
	//添加好友
	//申请查看富豪榜列表
	//申请查看玩家资料卡
	
	//登录表单验证
	public void varifyLoginInput(){
		if (int.Parse(PhoneNumInputField.text) == null || PasswordInputField.text == "") {
			LoginerrorText.text = "手机号或密码不能为空";
		}
		if (PasswordInputField.text.Length <= 5) {
			LoginerrorText.text = "密码长度需至少大于六位";
		} else {
			LoginerrorText.text = "";
     		loginSubmit();
			//FlexiableView.GetComponent<AlertController>().hideall();
			//GameManager.instance.isLogin = true;
			//GameManager.instance.InfoOpen = false;

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
		//get the gender information
		IEnumerable<Toggle> activeToggles = GenderToggle.ActiveToggles();
		foreach(Toggle tg in activeToggles)
		{
			Debug.Log("active toggle"+tg.name);
		}
		if (GameManager.instance.AvatarNum == 0) {
			FillInfoerrerText.text = "请选择一个头像";
		} else if (FillInfoNicknameInputField.text == "") {
			FillInfoerrerText.text = "昵称不能为空";
		} else if (FillInfoNicknameInputField.text.Length < 4) {
			FillInfoerrerText.text = "请输入至少四位昵称";
		} else if (GenderToggle.AnyTogglesOn () == false) {
			FillInfoerrerText.text = "请选择性别";
		} else if (GameManager.instance.AvatarNum > 0 && FillInfoNicknameInputField.text != ""
			&& GenderToggle.AnyTogglesOn () == true) {
			FillInfoerrerText.text = "";
			FlexiableView.GetComponent<AlertController> ().hideall ();
			if (GameManager.instance.isRegister) {
				GameManager.instance.isInfoFilled = true;
				GameManager.instance.InfoOpen = false;
			} else {
				print ("还未注册！");
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
	

	//登录提交

	public void loginSubmit(){
		StartCoroutine (LoginRequest ());
	}


	IEnumerator LoginRequest(){   
		//"post请求发送json数据的方式"
		//WWWForm form = new WWWForm();
		//form.AddField("username","123");
		//form.AddField("password","123123123");


		string EncodedPassword = Md5Sum (PasswordInputField.text);
		//WWW w = new WWW("http://139.224.59.3:8080/poker/api/user/login?username=13162195750&password=1223123");	
		WWW w = new WWW("http://139.224.59.3:8080/poker/api/user/login?username="+PhoneNumInputField.text+"&password="+PasswordInputField.text);
	    while (!w.isDone){yield return new WaitForEndOfFrame();}

			JsonData jsonData = JsonMapper.ToObject (w.text);
			
			if (((IDictionary)jsonData).Contains ("callStatus")) {
				Debug.Log ("登录结果" + jsonData ["callStatus"]);
				
				if (jsonData ["callStatus"].Equals ("SUCCEED")) {
					LoginerrorText.color = Color.green;
					LoginerrorText.text = "登录成功!";
					Debug.Log ("登录成功");
				FlexiableView.GetComponent<AlertController> ().hideall ();
				GameManager.instance.isLogin = true;
				GameManager.instance.InfoOpen = false;
					/*
				User userOj = new User();
				userOj.token = jsonData["data"]["token"].ToString();
				userOj.id = int.Parse(jsonData["data"]["id"].ToString());
				userOj.phoneNum = jsonData["data"]["username"].ToString();
				userOj.password = jsonData["data"]["password"].ToString();
				userOj.nickname = jsonData["data"]["realname"].ToString();
				userOj.score = int.Parse(jsonData["data"]["score"].ToString());
				userOj.playtimes = int.Parse(jsonData["data"]["allnum"].ToString());
				userOj.winRate = int.Parse(jsonData["data"]["winnum"].ToString())/int.Parse(jsonData["data"]["allnum"].ToString());
				userOj.inRace = int.Parse(jsonData["data"]["gatenum"].ToString())/int.Parse(jsonData["data"]["allnum"].ToString());
				userOj.range = int.Parse(jsonData["data"]["rank"].ToString());
				userOj.level = int.Parse(jsonData["data"]["level"].ToString());
				userOj.avatar = int.Parse(jsonData["data"]["pic"].ToString());

				Debug.Log(userOj);
*/
				} else if (jsonData ["callStatus"].Equals ("FAILED")) {
					LoginerrorText.color = new Color (0.8f, 0.2f, 0.2f);
					if (((IDictionary)jsonData).Contains ("errorCode")) {
						if (jsonData ["errorCode"].Equals ("Login_Error")) {
							LoginerrorText.text = "用户名或密码错误!";
							Debug.Log ("用户名或密码错误");
						} else if (jsonData ["errorCode"].Equals ("Password_error")) {
							LoginerrorText.text = "密码错误!";
							Debug.Log ("密码错误");
						}else if(jsonData ["errorCode"].Equals ("Username_NOT_Exist")) {
							LoginerrorText.text = "用户名不存在!";
							Debug.Log ("用户名不存在");
						}else {
							Debug.Log ("未知错误");
						}
					} else {
						Debug.Log ("返回的json对象中并没有“errorCode”键");
					} 		
				} else {
					Debug.Log ("callStatus”值异常");
				}
			} else {
				Debug.Log ("返回的json对象中并没有“callStatus”键");
			}
	}

}
