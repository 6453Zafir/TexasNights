using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class turnTheTimer : MonoBehaviour {
	public GameObject theTurningBar;
	public GameObject TimerText;
	public Image cooldownMask;
	public float waitTime = 10.0f;
	public int currentPlayer;
	Vector3 spawnPosition;
	Vector3 spawnRotaion;
	private bool isTimerJumped = false;
	// Use this for initialization
	void Start () {
		spawnPosition = new Vector3(0f,408f,0); //if you want to always spwan in the middle
		InvokeRepeating("ChangePosition", 0, 10); 

	}
	
	// Update is called once per frame
	void Update () {
		currentPlayer = TimerText.GetComponent<Timer> ().currentPlayer;
		isTimerJumped = TimerText.GetComponent<Timer> ().isTimerSwitched;
		switch (currentPlayer) {
		case 0:
			if(isTimerJumped){
				cooldownMask.fillAmount = 0.832f;
				isTimerJumped = false;
			}
			cooldownMask.fillAmount -= 1.0f/100 * Time.deltaTime;
			break;
		case 1:		
			if(isTimerJumped){
				cooldownMask.fillAmount = 0.915f;
				isTimerJumped = false;
			}
			cooldownMask.fillAmount -= 1.0f/110* Time.deltaTime;
			break;
		case 2:
			if(isTimerJumped){
				cooldownMask.fillAmount = 0.999f;
				isTimerJumped = false;
			}
			cooldownMask.fillAmount -= 1.0f/120 * Time.deltaTime;
			break;
		case 3:
			if(isTimerJumped){
				cooldownMask.fillAmount = 0.087f;
				isTimerJumped = false;
			}
			cooldownMask.fillAmount -= 1.0f/110 * Time.deltaTime;
			break;
		case 4:
			if(isTimerJumped){
				cooldownMask.fillAmount = 0.167f;
				isTimerJumped = false;
			}
			cooldownMask.fillAmount -= 1.0f/120 * Time.deltaTime;
			break;
		case 5:
			if(isTimerJumped){
				cooldownMask.fillAmount = 0.251f;
				isTimerJumped = false;
			}
			cooldownMask.fillAmount -= 1.0f/130 * Time.deltaTime;
			break;
		default:
			Debug.Log("something is wrong!!!");
			break;
		}

	}

	public void ChangePosition(){
		theTurningBar.transform.position = spawnPosition;
		if (theTurningBar.transform.eulerAngles.z > 60 &&theTurningBar.transform.eulerAngles.z <210) {
			theTurningBar.transform.Rotate(0,0,210);
		}else{
			theTurningBar.transform.Rotate (0, 0, 30);
		}
	}
}
