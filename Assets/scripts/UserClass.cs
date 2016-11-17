using UnityEngine;
using System;
using System.Collections;

public class User
{
	public string token;
	public string phoneNum;
	public string password;
	public string nickname;
	public bool gender;
	public int score;
	public int range;
	public int avatar;
	public int level;
	public float winRate;
	public float inRace;
	public int playtimes;
	public int maxScore;
	
	public void modifyScore(int scoreChange){
		score += scoreChange;
	}
}

public class Room
{
	public int RoomType;
	public int playRound;
	public int playerNum;
	public int currentPlayer;

}
