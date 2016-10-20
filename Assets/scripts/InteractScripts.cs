using UnityEngine;
using System.Collections;

public class InteractScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HideAllButton()  
	{    
		foreach (Transform child in transform)     
		{  
			child.gameObject.SetActiveRecursively(false);   
		}   
	}

	public void ShowAllButton()  
	{    
		foreach (Transform child in transform)     
		{  
			child.gameObject.SetActiveRecursively(true);   
		}   
	}
}
