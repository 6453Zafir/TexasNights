using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Vector3 location = new Vector3(0,0,0); // place you want it
	// Use this for initialization
	void Start () {	
		Transform Playerparent = GameObject.Find("Players").transform;
		GameObject player1 = Instantiate(Resources.Load("Player")) as GameObject;  // instatiate the object
		player1.transform.parent = Playerparent;
		player1.transform.position = new Vector3 (-620,200, 0);
		player1.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

		GameObject player2 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player2.transform.parent = Playerparent;
		player2.transform.position = new Vector3 (-470,-90, 0);
		player2.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

		GameObject player3 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player3.transform.parent = Playerparent;
		player3.transform.position = new Vector3 (-180,-270, 0);
		player3.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

		GameObject player4 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player4.transform.parent = Playerparent;
		player4.transform.position = new Vector3 (180,-270, 0);
		player4.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

		GameObject player5 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player5.transform.parent = Playerparent;
		player5.transform.position = new Vector3 (470,-90, 0);
		player5.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

		GameObject player6 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player6.transform.parent = Playerparent;
		player6.transform.position = new Vector3 (620,200, 0);
		player6.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
