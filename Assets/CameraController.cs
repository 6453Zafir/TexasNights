using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Zoom(){
		transform.position = Vector3.Lerp(transform.position, 100,100,100, Time.deltaTime * 3);
	}
}
