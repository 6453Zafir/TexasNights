using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class turnTheTimer : MonoBehaviour {
	public GameObject theTurningBar;
	public Button addtimeButton;
	public Text timerText;
	public Image cooldownMask;
	public float timeLimit = 10;
	public int currentPlayer;
	public bool isTimeAdded = false;
	public bool isTimerSwitched = false;
	Vector3 spawnPosition;
	// Use this for initialization

	void Start () {
		currentPlayer = 0;  
		spawnPosition = new Vector3(0f,408f,0);
	}
	
	// Update is called once per frame
	void Update () {

		if (timeLimit >= 0) {
			timeLimit -= Time.deltaTime;
			isTimerSwitched = false;
			timerText.text = timeLimit.ToString ("f1");
			if(timeLimit < 5){
				timerText.color = new Color(0.882f,0.447f,0.365f);
			}
		} else {
			Debug.Log("Time is over");
			if(currentPlayer<5){
				currentPlayer++;
			}else{
				currentPlayer = 0;
			}
			timerText.color = new Color(1,1,1);
			timeLimit = 10;
			addtimeButton.interactable = true;
			isTimeAdded = false;
			isTimerSwitched = true;
		}

		switch (currentPlayer) {
		case 0:
			if(isTimerSwitched){
				cooldownMask.fillAmount = 0.832f;
				theTurningBar.transform.position = spawnPosition;
				theTurningBar.transform.eulerAngles = new Vector3(0,0,270);
			}
			cooldownMask.fillAmount -= 1.0f/120 * Time.deltaTime;
			break;
		case 1:		
			if(isTimerSwitched){
				cooldownMask.fillAmount = 0.915f;
				theTurningBar.transform.eulerAngles = new Vector3(0,0,300);
			}
			cooldownMask.fillAmount -= 1.0f/120* Time.deltaTime;
			break;
		case 2:
			if(isTimerSwitched){
				cooldownMask.fillAmount = 0.999f;
				theTurningBar.transform.eulerAngles = new Vector3(0,0,330);
			}
			cooldownMask.fillAmount -= 1.0f/120 * Time.deltaTime;
			break;
		case 3:
			if(isTimerSwitched){
				cooldownMask.fillAmount = 0.087f;
				theTurningBar.transform.eulerAngles = new Vector3(0,0,0);
			}
			cooldownMask.fillAmount -= 1.0f/120 * Time.deltaTime;
			break;
		case 4:
			if(isTimerSwitched){
				cooldownMask.fillAmount = 0.167f;
				theTurningBar.transform.eulerAngles = new Vector3(0,0,30);
			}
			cooldownMask.fillAmount -= 1.0f/120 * Time.deltaTime;
			break;
		case 5:
			if(isTimerSwitched){
				cooldownMask.fillAmount = 0.251f;
				theTurningBar.transform.eulerAngles = new Vector3(0,0,60);
			}
			cooldownMask.fillAmount -= 1.0f/120 * Time.deltaTime;
			break;
		default:
			Debug.Log("the player num is worng");
			break;
		}

	}

	public void addTime(){
		if (isTimeAdded == false) {
			timeLimit += 5;
			cooldownMask.fillAmount += 0.045f;
			isTimeAdded = true;
			addtimeButton.interactable = false;
			timerText.color = new Color(0.906f,0.804f,0.125f);
			timerText.fontSize = 90;
		} else {
			Debug.Log("there is only one chance to add time");
		}
	}
}
