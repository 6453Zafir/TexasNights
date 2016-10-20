using UnityEngine;
using System.Collections;

public class InteractController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void hideAllButton(){
		foreach (Transform child in transform)     
		{  
			child.gameObject.SetActiveRecursively(false);   
		}   
	}
	public void showAllButton(){
		foreach (Transform child in transform)     
		{  
			child.gameObject.SetActiveRecursively(true);   
		}   
	}
}
