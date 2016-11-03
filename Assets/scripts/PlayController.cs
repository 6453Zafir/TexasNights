using UnityEngine;
using System.Collections;

public class PlayController : MonoBehaviour {

	public GameObject Player1;
	public GameObject Player2;
	public GameObject Player3;
	public GameObject Player5;
	public GameObject Player6;

	private Animator PlayTimeAnimation;
	private bool allReady = false;

	// Use this for initialization
	void Start () {
		PlayTimeAnimation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		allReady = PlayTimeAnimation.GetBool("isStarted");
		GameObject player1ins = Player1.transform.GetChild (0).gameObject;
		GameObject player2ins = Player2.transform.GetChild (0).gameObject;
		GameObject player3ins = Player3.transform.GetChild (0).gameObject;
		GameObject player5ins = Player5.transform.GetChild (0).gameObject;
		GameObject player6ins = Player6.transform.GetChild (0).gameObject;
		if (player1ins!=null && player2ins!=null && player3ins!=null && player5ins!=null && player6ins!=null) {
			PlayTimeAnimation.SetBool("isStarted",true);

			Debug.Log ("all players ready!");
		} else {
			PlayTimeAnimation.SetBool("isStarted",false);
			Debug.Log("Can not continue, lack of player");
		}
	}
}
