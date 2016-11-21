using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class User
{
	/// <summary>
	/// 
	/// </summary>
	public string token { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string phoneNum { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string password { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string nickname { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string gender { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int score { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int range { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int avatar { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int level { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public double winRate { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public double inRace { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int playtimes { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int maxScore { get; set; }
}

public class Root
{
	/// <summary>
	/// 
	/// </summary>
	public List <User> user { get; set; }
}