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
	public Button ForgetPasswordGetVerifyCode;
	public Text FVtimer;
	private bool startFVCodeTimer = false ;
	private float VerifyFCodeTimeLimit = 60;

	public InputField RegisterPhoneNumInputField;
	public InputField RegisterVarifyCodeInputField;
	public InputField RegisterPasswordInputField;
	public InputField RegisterComfirmNewPasswordInputField;
	public Button RegisterGetVerifyCode;
	public Text RVtimer;
	private bool startRVCodeTimer = false ;
	private float VerifyRCodeTimeLimit = 60;

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
	void Update(){
		if (startFVCodeTimer) {
			VerifyFCodeTimeLimit -= Time.deltaTime;
			FVtimer.text = VerifyFCodeTimeLimit.ToString ("f0");
		} else {
			FVtimer.text = "";
		}
		if (startRVCodeTimer) {
			VerifyRCodeTimeLimit -= Time.deltaTime;
			RVtimer.text = VerifyRCodeTimeLimit.ToString ("f0");
		} else {
			RVtimer.text = "";
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
			StartCoroutine (LoginRequest ());
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
			ForgetPasswordText.text = "密码长度需至少大于六位";
		} else if (!ForgetPasswordNewPasswordInputField.text.Equals( ForgetPasswordComfirmNewPasswordInputField.text) ) {
			ForgetPasswordText.text = "两次密码输入不一致";
		} else {
			ForgetPasswordText.text = "";
			StartCoroutine(forgetPasswordRequest());
			//FlexiableView.GetComponent<AlertController>().switchbetwenforgetPassword();
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
			StartCoroutine(RegisterRequest());
			//FlexiableView.GetComponent<AlertController>().toFillInfomation();
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
	


	IEnumerator LoginRequest(){   
		//"how to send a form in request"
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

				//User GameManager.instance. = new User();
				if(jsonData["token"]!=null){
					GameManager.instance.userOj.token = jsonData["token"].ToString();
				}
				if(jsonData["data"]["id"]!=null){
					GameManager.instance.userOj.id = int.Parse(jsonData["data"]["id"].ToString());
				}
				if(jsonData["data"]["username"]!=null){
					GameManager.instance.userOj.phoneNum = jsonData["data"]["username"].ToString();
				}
				if(jsonData["data"]["password"]!=null){
					GameManager.instance.userOj.password = jsonData["data"]["password"].ToString();
				}
				if(jsonData["data"]["realname"]!=null){
					GameManager.instance.userOj.nickname = jsonData["data"]["realname"].ToString();
				}
				if(jsonData["data"]["score"]!=null){
					GameManager.instance.userOj.score = int.Parse(jsonData["data"]["score"].ToString());
				}
				if(jsonData["data"]["allnum"]!=null){
					GameManager.instance.userOj.playtimes = int.Parse(jsonData["data"]["allnum"].ToString());
				}
				if(jsonData["data"]["winnum"]!=null||jsonData["data"]["allnum"]!=null){
					GameManager.instance.userOj.winRate = int.Parse(jsonData["data"]["winnum"].ToString())/int.Parse(jsonData["data"]["allnum"].ToString());
				}
				if(jsonData["data"]["gatenum"]!=null||jsonData["data"]["allnum"]!=null){
					GameManager.instance.userOj.inRace = int.Parse(jsonData["data"]["gatenum"].ToString())/int.Parse(jsonData["data"]["allnum"].ToString());
				}
				if(jsonData["data"]["rank"]!=null){
					GameManager.instance.userOj.range = int.Parse(jsonData["data"]["rank"].ToString());
				}
				if(jsonData["data"]["level"]!=null){
					GameManager.instance.userOj.level = int.Parse(jsonData["data"]["level"].ToString());
				}
				if(jsonData["data"]["pic"]!=null){
					GameManager.instance.userOj.avatar = int.Parse(jsonData["data"]["pic"].ToString());
				}

				Debug.Log("userAvatar "+GameManager.instance.userOj.avatar);

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


	public void getVerifyCode(){
		StartCoroutine (varifyCodeRequest ());
	}

	IEnumerator varifyCodeRequest(){
		VerifyFCodeTimeLimit = 60;
		VerifyRCodeTimeLimit = 60;

		if (ForgetPasswordPhoneNumInputField.text != "") {
			WWW w = new WWW("http://139.224.59.3:8080/poker/api/user/getVerifyCode?phone="+ForgetPasswordPhoneNumInputField.text);
			while (!w.isDone){yield return new WaitForEndOfFrame();}
			
			JsonData jsonData = JsonMapper.ToObject (w.text);
			if (((IDictionary)jsonData).Contains ("callStatus")) {
				Debug.Log ("验证码请求结果" + jsonData ["callStatus"]);	
				if (jsonData ["callStatus"].Equals ("SUCCEED")) {
					if(FlexiableView.transform.GetChild(0).gameObject.activeSelf){
						if(VerifyFCodeTimeLimit >= 0){
							startFVCodeTimer = true;
							ForgetPasswordGetVerifyCode.interactable = false;
							ForgetPasswordText.text="验证码已发送";
							Debug.Log("验证码已发送");
						}else{
							startFVCodeTimer = false;
							ForgetPasswordGetVerifyCode.interactable = true;
						}
					}else if(FlexiableView.transform.GetChild(1).gameObject.activeSelf){
						if(VerifyRCodeTimeLimit >= 0){
							startRVCodeTimer = true;
							RegisterGetVerifyCode.interactable = false;
							RegistererrorText.text="验证码已发送";
							Debug.Log("验证码已发送");
						}else{
							startRVCodeTimer = false;
							RegisterGetVerifyCode.interactable = true;
						}
					}else{
						Debug.Log("弹窗跳转有误");
					}

				} else if (jsonData ["callStatus"].Equals ("FAILED")) {
					if(FlexiableView.transform.GetChild(0).gameObject.activeSelf){
						ForgetPasswordText.text = "验证码发送失败";
					}else if(FlexiableView.transform.GetChild(1).gameObject.activeSelf){
						RegistererrorText.text="验证码发送失败";
					}else{
						Debug.Log("弹窗跳转有误");
					}
				} else {
					Debug.Log ("callStatus”值异常");
				}
			} else {
				Debug.Log ("返回的json对象中并没有“callStatus”键");
			}

		} else {
			ForgetPasswordText.text = "请填写手机号！";
		}
	} 

	
	IEnumerator forgetPasswordRequest(){
		startFVCodeTimer = false;
		WWW w = new WWW ("http://139.224.59.3:8080/poker/api/user/forgetPWD?username="+ForgetPasswordPhoneNumInputField.text+"&password="+ForgetPasswordNewPasswordInputField.text+"&code="+ForgetPasswordVarifyCodeInputField.text);
		while (!w.isDone){yield return new WaitForEndOfFrame();}
		JsonData jsonData = JsonMapper.ToObject (w.text);

		if (((IDictionary)jsonData).Contains ("callStatus")) {
			Debug.Log ("忘记密码请求结果" + jsonData ["callStatus"]);	
			if (jsonData ["callStatus"].Equals ("SUCCEED")) {
				ForgetPasswordText.color = Color.green;
				ForgetPasswordText.text="密码修改成功！请重新登录";
				FlexiableView.GetComponent<AlertController>().switchbetwenforgetPassword();
				Debug.Log("密码修改成功");
			} else if (jsonData ["callStatus"].Equals ("FAILED")) {
				LoginerrorText.color = new Color (0.8f, 0.2f, 0.2f);
				if (((IDictionary)jsonData).Contains ("errorCode")) {
					if (jsonData ["errorCode"].Equals ("Verify_Code_5min")) {
						LoginerrorText.text = "验证码超时!";
						Debug.Log ("验证码超时");
					} else if (jsonData ["errorCode"].Equals ("User_notExist")) {
						LoginerrorText.text = "用户不存在!";
						Debug.Log ("用户不存在");
					}else if(jsonData ["errorCode"].Equals ("Verify_Code_notExist")){
						LoginerrorText.text = "该手机号未请求过验证码！";
						Debug.Log("该手机号未请求过验证码");
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

	IEnumerator RegisterRequest(){
		startRVCodeTimer = false;
		WWW w = new WWW ("http://139.224.59.3:8080/poker/api/user/register?username="+RegisterPhoneNumInputField.text+"&password="+RegisterPasswordInputField.text+"&code="+RegisterVarifyCodeInputField.text);
		while (!w.isDone){yield return new WaitForEndOfFrame();}
		JsonData jsonData = JsonMapper.ToObject (w.text);

		if (((IDictionary)jsonData).Contains ("callStatus")) {
			Debug.Log ("注册请求结果" + jsonData ["callStatus"]);	
			if (jsonData ["callStatus"].Equals ("SUCCEED")) {
				RegistererrorText.color = Color.green;
				RegistererrorText.text="注册成功";
				GameManager.instance.isRegister = true;
				FlexiableView.GetComponent<AlertController>().toFillInfomation();
				Debug.Log("注册成功");
				//newUserHere....
			} else if (jsonData ["callStatus"].Equals ("FAILED")) {
				RegistererrorText.color = new Color (0.8f, 0.2f, 0.2f);
				if (((IDictionary)jsonData).Contains ("errorCode")) {
					if (jsonData ["errorCode"].Equals ("Verify_Code_5min")) {
						RegistererrorText.text = "验证码超时!";
						Debug.Log ("验证码超时");
					} else if (jsonData ["errorCode"].Equals ("Username_Already_Exist")) {
						RegistererrorText.text = "用户已存在，请直接登录!";
						Debug.Log ("用户已存在，请直接登录!");
					}else if(jsonData ["errorCode"].Equals ("Verify_Code_notExist")){
						RegistererrorText.text = "该手机号未请求过验证码！";
						Debug.Log("该手机号未请求过验证码");
					}else if(jsonData ["errorCode"].Equals ("Verify_Code_Error")){
						RegistererrorText.text = "验证码输入错误，请重新输入！";
						Debug.Log("验证码输入错误，请重新输入！");
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

	IEnumerator FillInfoRequest(){
		startRVCodeTimer = false;
		WWW w = new WWW ("http://139.224.59.3:8080/poker/api/user/register?username="+RegisterPhoneNumInputField.text+"&password="+RegisterPasswordInputField.text+"&code="+RegisterVarifyCodeInputField.text);
		while (!w.isDone){yield return new WaitForEndOfFrame();}
		JsonData jsonData = JsonMapper.ToObject (w.text);
	}
}
