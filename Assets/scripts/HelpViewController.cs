using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HelpViewController : MonoBehaviour {

	public GameObject helpPanel;
	public GameObject RulePanel;
	public Button leaveButton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (helpPanel != null) {
			HideAlertIfClickedOutside (helpPanel);
		}
		if (RulePanel != null) {
			HideAlertIfClickedOutside (RulePanel);
		}


	}
	private void HideAlertIfClickedOutside(GameObject panel) {
		if (Input.GetMouseButton(0) && panel.activeSelf && 
		    !RectTransformUtility.RectangleContainsScreenPoint(
			panel.GetComponent<RectTransform>(), 
			Input.mousePosition, 
			Camera.main)) {
			panel.SetActive(false);
			GameObject panelParent = panel.transform.parent.gameObject;
			panelParent.SetActive(false);
		}
	}

	public void backToMain(){
		Application.LoadLevel ("menu");
		Debug.Log ("level loaded");
	}

}
