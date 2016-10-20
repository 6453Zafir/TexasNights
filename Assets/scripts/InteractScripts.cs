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
		StartCoroutine(ExecuteAfterTime(10000));
		Debug.Log("really?");
		foreach (Transform child in transform)     
		{  
			child.gameObject.SetActiveRecursively(true);   
		}   
	}

	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		
		// Code to execute after the delay
	}
}
