using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float greetingScreen = 0.85f;
	public float fullScreen = 3.75f;
	public float boardScreen = 2.37f;
	public Camera MainMenucamera;
	private Animator CamAniController;

	void Start () {	
		CamAniController = GetComponent<Animator>();
	}

	public void ZoomToFullAnimation(){
		CamAniController.SetBool ("isStarted", true);
		//CamAniController.Play ("greetingToFUllCam");
	}

	public void ZoomToInfoAnimation(){
		CamAniController.SetBool ("InfoOpen", true);
		//CamAniController.Play ("fullToInfo");
	}

	public void ZoomoutToFull(){
		MainMenucamera.orthographicSize = fullScreen;
		transform.position = new Vector3(0, 0, 0);
	}
	public void ZoomToGreeting(){
		transform.position = new Vector3(-3.8f, 0.93f, 0);
		MainMenucamera.orthographicSize = greetingScreen;
	}

	public void ZoomToInfo(){
		transform.position = new Vector3(-2.84f, 1.39f, 0);
		MainMenucamera.orthographicSize = boardScreen;
	}
}
