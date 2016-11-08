using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public Text timerText;
	public float timeLimit = 10;
	public bool isTimeAdded = false;
	public Button addtimeButton;
	public int currentPlayer;
	public bool isTimerSwitched = false;
	// Use this for initialization
	void Start () {
		currentPlayer = 0;  
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
			timeLimit = 10;
			isTimeAdded = false;
			addtimeButton.interactable = true;
			timerText.color = new Color(1,1,1);
			isTimerSwitched = true;
			if(currentPlayer<5){
				currentPlayer++;
			}else{
				currentPlayer = 0;
			}
		}
	}

	public void addTime(){
		if (isTimeAdded == false) {
			timeLimit += 5;
			isTimeAdded = true;
			addtimeButton.interactable = false;
			timerText.color = new Color(0.906f,0.804f,0.125f);
			timerText.fontSize = 90;
		} else {
			Debug.Log("there is only one chance to add time");
		}
	}

}
