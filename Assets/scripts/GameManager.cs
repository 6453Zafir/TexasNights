using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public bool isLogin = false;
	public bool isRegister = false;
	public bool isInfoFilled = false;
	public bool isLoginViewAlerted = false;
	public bool isStarted = false;
	public bool InfoOpen = false;
	public bool friendListOpen = false;
	public bool settingOpen = false;
	public bool richlist = false; 
	public bool store = false;
	public bool isChooseOpen = false;
	public bool hasfillInfo = false;
	public int AvatarNum = 0;
	public User userOj = new User();

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad (this);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
