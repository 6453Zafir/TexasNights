using UnityEngine;
using System.Collections;

public class turnTheTimer : MonoBehaviour {
	Vector3 spawnPosition;
	Vector3 spawnRotaion;
//	Vector2 anchor;
	// Use this for initialization
	void Start () {
		spawnPosition = new Vector3(0f,408f,0); //if you want to always spwan in the middle
//		anchor = new Vector2 (0f, 410.2f);
		InvokeRepeating("ChangePosition", 0, 1); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void ChangePosition(){
		transform.position = spawnPosition;
		Debug.Log (transform.eulerAngles.z);
		if (transform.eulerAngles.z > 60 &&transform.eulerAngles.z <210) {
			transform.Rotate(0,0,210);
		}else{
			transform.Rotate (0, 0, 30);
		}

		//spawnPosition = new Vector3 (Random.Range(-5,5), Random.Range(-5,5), Random.Range(-5,5));
	}
}
