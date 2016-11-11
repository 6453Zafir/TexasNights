using UnityEngine;
using System.Collections;

public class TableController : MonoBehaviour {
	public int status = 0;
	/*
		status = 0:玩家不足等待玩家，即只有一个玩家
		status = 1:发手牌
		status = 2:发底牌
		status = 3:发第4张底牌
		status = 4:发第5张底牌
		status = 5:某玩家获胜
		
	 */
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
