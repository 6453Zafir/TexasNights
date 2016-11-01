using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject Player1Labell;
	public GameObject Player1Label2;
	public GameObject Player1Label3;
	public GameObject Player1Label4;
	public GameObject Player1Label5;
	public GameObject Player1Label6;
	
	// Use this for initialization
	void Start () {	
		//Transform Playerparent = GameObject.Find("Players").transform;
		Transform Playerl1 = Player1Labell.transform;
		Transform Player12 = Player1Label2.transform;
		Transform Player13 = Player1Label3.transform;
		Transform Player14 = Player1Label4.transform;
		Transform Player15 = Player1Label5.transform;
		Transform Player16 = Player1Label6.transform;
		
		GameObject player1 = Instantiate(Resources.Load("Player")) as GameObject;  // instatiate the object
		player1.transform.parent = Playerl1;
		player1.transform.localPosition = new Vector3 (-20, 0, 0);
		player1.transform.localScale = new Vector3(1f, 1f, 1f);
		
		GameObject player2 = Instantiate(Resources.Load("Player")) as GameObject;  // instatiate the object
		player2.transform.parent = Player12;
		player2.transform.localPosition = new Vector3 (-20, 0, 0);
		player2.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
		
		GameObject player3 = Instantiate(Resources.Load("Player")) as GameObject;  // instatiate the object
		player3.transform.parent = Player13;
		player3.transform.localPosition = new Vector3 (-20, 0, 0);
		player3.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
		
		GameObject player4 = Instantiate(Resources.Load("Player")) as GameObject;  // instatiate the object
		player4.transform.parent = Player14;
		player4.transform.localPosition = new Vector3 (-20, 0, 0);
		player4.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
		
		GameObject player5 = Instantiate(Resources.Load("Player")) as GameObject;  // instatiate the object
		player5.transform.parent = Player15;
		player5.transform.localPosition = new Vector3 (-20, 0, 0);
		player5.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
		
		GameObject player6 = Instantiate(Resources.Load("Player")) as GameObject;  // instatiate the object
		player6.transform.parent = Player16;
		player6.transform.localPosition = new Vector3 (-20, 0, 0);
		player6.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
