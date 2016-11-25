using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;

public class Login : MonoBehaviour {
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

	public InputField FindFriendInputField;

	public Text LoginerrorText;
	public Text ForgetPasswordText;
	public Text RegistererrorText;
	public Text FillInfoerrerText;
	public Text FindFrienderrorText;

	public GameObject InfoButtonAvatar;
	private Object [] sprites;
	private GameObject avatar;
	private SpriteRenderer renderer;
	private SpriteRenderer InfoButtonRenderer;

	void Start(){
		//头像按钮
		avatar = new GameObject ("avatar");
		avatar.transform.parent = AvatarButton.transform;
		avatar.transform.position = AvatarButton.transform.position;
		avatar.transform.localScale = new Vector3 (5.6f, 5.4f, 5f);
		avatar.layer = 8;
		if (avatar != null) {
			renderer = avatar.AddComponent<SpriteRenderer> ();
			//InfoButtonRenderer = InfoButtonAvatar.GetComponent<SpriteRenderer>();
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
	public void findFriend(){
		if (FindFriendInputField.text != "") {
			StartCoroutine (findUserRequest ());
		} else {
			FindFrienderrorText.text = "请先输入要搜索好友的手机号!";
			Debug.Log("请先输入要搜索好友的手机号！");
		}
	}

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
			StartCoroutine(FillInfoRequest());

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

				string userJson = JsonMapper.ToJson(jsonData["data"]);
				User userTemp = JsonMapper.ToObject<User>(userJson);
				GameManager.instance.userOj = userTemp;
				if(jsonData["token"]!=null){
					GameManager.instance.userOj.token = jsonData["token"].ToString();
				}
				Debug.Log(GameManager.instance.userOj.pic);
				//若已完善个人信息，则直接隐藏弹窗，若没有完善个人信息，则跳转至完善信息页面
				//信息是否完善通过判断返回的userOj中的头像是否为0判断
				if(GameManager.instance.userOj.pic == null || int.Parse(GameManager.instance.userOj.pic) == 0){
					GameManager.instance.isLogin = true;
					gameObject.transform.GetChild(0).gameObject.SetActive(false);
					gameObject.transform.GetChild(1).gameObject.SetActive(true);
					gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
					gameObject.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
				}else{
					gameObject.GetComponent<AlertController> ().hideall ();
					GameManager.instance.isLogin = true;
					GameManager.instance.InfoOpen = false;
				}


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
		Debug.Log ("LoginView active + "+gameObject.transform.GetChild (0).gameObject.activeSelf);
		if (gameObject.transform.GetChild (0).gameObject.activeSelf) {
			VerifyFCodeTimeLimit = 60;
			if (ForgetPasswordPhoneNumInputField.text != "") {
				WWW w = new WWW ("http://139.224.59.3:8080/poker/api/user/getVerifyCode?phone=" + ForgetPasswordPhoneNumInputField.text);
				while (!w.isDone) {
					yield return new WaitForEndOfFrame ();
				}
				JsonData jsonData = JsonMapper.ToObject (w.text);
				if (((IDictionary)jsonData).Contains ("callStatus")) {
					Debug.Log ("忘记密码验证码请求结果" + jsonData ["callStatus"]);	
					if (jsonData ["callStatus"].Equals ("SUCCEED")) {
						if (VerifyFCodeTimeLimit >= 0) {
							startFVCodeTimer = true;
							ForgetPasswordGetVerifyCode.interactable = false;
							ForgetPasswordText.text = "验证码已发送";
							Debug.Log ("验证码已发送");
						} else {
							startFVCodeTimer = false;
							ForgetPasswordGetVerifyCode.interactable = true;
						}
					} else if (jsonData ["callStatus"].Equals ("FAILED")) {
						ForgetPasswordText.text = "验证码发送失败";
					} else {
						Debug.Log ("callStatus”值异常");
					}
				} else {
					Debug.Log ("返回的json对象中并没有“callStatus”键");
				}
			}else{
				ForgetPasswordText.text = "请先填写手机号!";
			}

		} else if (gameObject.transform.GetChild (1).gameObject.activeSelf) {
			VerifyRCodeTimeLimit = 60;
			if (RegisterPhoneNumInputField.text != "") {
				WWW w = new WWW ("http://139.224.59.3:8080/poker/api/user/getVerifyCode?phone=" + RegisterPhoneNumInputField.text);
				while (!w.isDone) {
					yield return new WaitForEndOfFrame ();
				}
				JsonData jsonData = JsonMapper.ToObject (w.text);
				if (((IDictionary)jsonData).Contains ("callStatus")) {
					Debug.Log ("注册验证码请求结果" + jsonData ["callStatus"]);	
					if (jsonData ["callStatus"].Equals ("SUCCEED")) {
						if (VerifyRCodeTimeLimit >= 0) {
							startRVCodeTimer = true;
							RegisterGetVerifyCode.interactable = false;
							RegistererrorText.text = "验证码已发送";
							Debug.Log ("验证码已发送");
						} else {
							startRVCodeTimer = false;
							RegisterGetVerifyCode.interactable = true;
						}
					} else if (jsonData ["callStatus"].Equals ("FAILED")) {
						RegistererrorText.text = "验证码发送失败";
					} else {
						Debug.Log ("callStatus”值异常");
					}
				} else {
					Debug.Log ("返回的json对象中并没有“callStatus”键");
				}
			}else{
				RegistererrorText.text = "请先填写手机号!";
			}
		} else {
			Debug.Log("弹窗跳转有误");
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
				gameObject.GetComponent<AlertController>().switchbetwenforgetPassword();
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
				Debug.Log("注册成功");
				string userJson = JsonMapper.ToJson(jsonData["data"]);
				User userTemp = JsonMapper.ToObject<User>(userJson);
				GameManager.instance.userOj = userTemp;
				if(jsonData["token"]!=null){
					GameManager.instance.userOj.token = jsonData["token"].ToString();
				}
				GameManager.instance.isRegister = true;
				gameObject.GetComponent<AlertController>().toFillInfomation();

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
			WWW w = new WWW ("http://139.224.59.3:8080/poker/api/user/addUserInfo?realName=" + FillInfoNicknameInputField.text + "&pic=" + GameManager.instance.AvatarNum + "&token=" + GameManager.instance.userOj.token);
			while (!w.isDone) {
				yield return new WaitForEndOfFrame ();
			}
			JsonData jsonData = JsonMapper.ToObject (w.text);
			if (((IDictionary)jsonData).Contains ("callStatus")) {
				Debug.Log ("完善信息结果" + jsonData ["callStatus"]);	
				if (jsonData ["callStatus"].Equals ("SUCCEED")) {
					string userJson = JsonMapper.ToJson(jsonData["data"]);
					User userTemp = JsonMapper.ToObject<User>(userJson);
					GameManager.instance.userOj = userTemp;
				if(jsonData["token"]!=null){
					GameManager.instance.userOj.token = jsonData["token"].ToString();
				}
					gameObject.GetComponent<AlertController>().hideall();
						GameManager.instance.isInfoFilled = true;
						GameManager.instance.InfoOpen = false;
					Debug.Log("信息已完善");
				} else if (jsonData ["callStatus"].Equals ("FAILED")) {
					LoginerrorText.color = new Color (0.8f, 0.2f, 0.2f);
					if (((IDictionary)jsonData).Contains ("errorCode")) {
						Debug.Log("信息完善失败");
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

	public void getUserInfo(){
		GameObject infopanel = gameObject.transform.GetChild(2).transform.GetChild(0).gameObject;
		float? winrate = GameManager.instance.userOj.winnum / GameManager.instance.userOj.allnum;
		infopanel.transform.GetChild (0).gameObject.GetComponent<Text> ().text = GameManager.instance.userOj.username;
		infopanel.transform.GetChild (1).gameObject.GetComponent<Text> ().text = GameManager.instance.userOj.realname;
		infopanel.transform.GetChild (2).gameObject.GetComponent<Text> ().text = GameManager.instance.userOj.score.ToString();
		infopanel.transform.GetChild (3).gameObject.GetComponent<Text> ().text = GameManager.instance.userOj.level.ToString();
		//infopanel.transform.GetChild (4).gameObject.GetComponent<Text> ().text = winrate.ToString();
		//infopanel.transform.GetChild (5).gameObject.GetComponent<Text> ().text = (GameManager.instance.userOj.gatenum/GameManager.instance.userOj.allnum).ToString();
		infopanel.transform.GetChild (6).gameObject.GetComponent<Text> ().text = GameManager.instance.userOj.rank;
		infopanel.transform.GetChild (7).gameObject.GetComponent<Text> ().text = GameManager.instance.userOj.allnum.ToString();
		infopanel.transform.GetChild (8).gameObject.GetComponent<Text> ().text = GameManager.instance.userOj.allinnum.ToString();
		infopanel.transform.GetChild (9).gameObject.GetComponent<Text> ().text = GameManager.instance.userOj.registerDate.ToString();

		GameObject infoavatar = new GameObject ("infoavatar");
		infoavatar.transform.parent = infopanel.transform.GetChild (10).transform;
		infoavatar.transform.position = infopanel.transform.GetChild (10).transform.position;
		infoavatar.transform.localScale = new Vector3 (4.5f, 4.4f, 5f);
		infoavatar.layer = 8;

		if (infoavatar != null) {
			InfoButtonRenderer = infoavatar.AddComponent<SpriteRenderer> ();
			//InfoButtonRenderer = InfoButtonAvatar.GetComponent<SpriteRenderer>();
			sprites = Resources.LoadAll ("avatars");
			InfoButtonRenderer.sortingLayerName = LAYER_NAME;
			InfoButtonRenderer.sortingOrder = 1;
			int avatarNum = int.Parse(GameManager.instance.userOj.pic);
			InfoButtonRenderer.sprite = (Sprite)sprites [avatarNum];
		} else {
			print("the avatar havn't newed");
		}
	}

	public void infoToEditinfo(){
		gameObject.transform.GetChild (0).gameObject.SetActive (false);
		gameObject.transform.GetChild (2).gameObject.SetActive (false);
		gameObject.transform.GetChild (1).gameObject.SetActive (true);
		gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
		gameObject.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
	}

	IEnumerator findUserRequest(){
		WWW w = new WWW ("http://139.224.59.3:8080/poker/api/user/findUser?userName=" + FindFriendInputField.text);
		while (!w.isDone) {
			yield return new WaitForEndOfFrame ();
		}
		JsonData jsonData = JsonMapper.ToObject (w.text);
		if (((IDictionary)jsonData).Contains ("callStatus")) {
			Debug.Log ("搜索好友结果" + jsonData ["callStatus"]);	
			if (jsonData ["callStatus"].Equals ("SUCCEED")) {
				Debug.Log("搜索成功");
				FindFrienderrorText.text = "";
			} else if (jsonData ["callStatus"].Equals ("FAILED")) {
				FindFrienderrorText.color = new Color (0.8f, 0.2f, 0.2f);
				if (((IDictionary)jsonData).Contains ("errorCode")) {
					if (jsonData ["errorCode"].Equals ("Username_NOT_Exsit")) {
						FindFrienderrorText.text = "手机号不存在，请重新输入!";
						Debug.Log ("搜索的手机号不存在");
					} else {
						Debug.Log ("errorCode值异常");
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
