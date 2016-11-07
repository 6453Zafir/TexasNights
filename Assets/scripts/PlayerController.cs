using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	const int playerNum = 6;
	private Transform[] Players = new Transform[playerNum];
	private GameObject[] player = new GameObject[playerNum];
//	private GameObject[] playerPoker = new GameObject[playerNum]; 
	// Use this for initialization
	void Start () {	
		for (int i = 0; i<	gameObject.transform.childCount; i++) {
			Players[i] = gameObject.transform.GetChild(i).transform;

			player[i] = Instantiate(Resources.Load("Player")) as GameObject; 
			player[i].transform.parent = Players[i];
			player[i].transform.localPosition = new Vector3 (-20, 0, 0);
			//player[i].transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}
