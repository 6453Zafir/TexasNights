using UnityEngine;
using System.Collections;

public class cc : MonoBehaviour {
	public Camera MainMenucamera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ZoomToGreeting(){
		transform.position = new Vector3(-120f, 120f, -995);
		MainMenucamera.orthographicSize = 170f;
	}
}
