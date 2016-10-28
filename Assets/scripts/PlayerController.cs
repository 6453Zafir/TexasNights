using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Vector3 location = new Vector3(0,0,0); // place you want it
	// Use this for initialization
	void Start () {	
		Transform Playerparent = GameObject.Find("Players").transform;
		GameObject player1 = Instantiate(Resources.Load("Player")) as GameObject;  // instatiate the object
		player1.transform.parent = Playerparent;
		player1.transform.position = new Vector3 (-620,220, 0);
		player1.transform.localScale = new Vector3(1, 1, 1);

		GameObject player2 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player2.transform.parent = Playerparent;
		player2.transform.position = new Vector3 (-470,-70, 0);
		player2.transform.localScale = new Vector3(1, 1, 1);

		GameObject player3 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player3.transform.parent = Playerparent;
		player3.transform.position = new Vector3 (-180,-250, 0);
		player3.transform.localScale = new Vector3(1, 1, 1);

		GameObject player4 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player4.transform.parent = Playerparent;
		player4.transform.position = new Vector3 (180,-250, 0);
		player4.transform.localScale = new Vector3(1, 1, 1);

		GameObject player5 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player5.transform.parent = Playerparent;
		player5.transform.position = new Vector3 (470,-70, 0);
		player5.transform.localScale = new Vector3(1, 1, 1);

		GameObject player6 = Instantiate(Resources.Load("Player"), location, Quaternion.identity) as GameObject;  // instatiate the object
		player6.transform.parent = Playerparent;
		player6.transform.position = new Vector3 (620,220, 0);
		player6.transform.localScale = new Vector3(1, 1, 1);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
