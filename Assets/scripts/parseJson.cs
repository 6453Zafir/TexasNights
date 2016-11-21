using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class parseJson : MonoBehaviour {
	private string jsonString;
	private JsonData itemData;

	// Use this for initialization
	void Start () {
		jsonString = File.ReadAllText (Application.dataPath + "/scripts/user.json");
		itemData = JsonMapper.ToObject (jsonString);
		//Debug.Log (itemData["user"][0]["phoneNum"]);
		Debug.Log(GetItem(1,"room")["currentPlayer"]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	JsonData GetItem(int roomType,string type){
		Debug.Log ("length"+itemData[type].Count);
		for (int i = 0; i<itemData[type].Count; i++) {
			if(itemData[type][i]["roomtype"].Equals(roomType)){
				return itemData[type][i];
			}
		}
		return null;
	}
}
