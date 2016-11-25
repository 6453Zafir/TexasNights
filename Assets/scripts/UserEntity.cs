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
	public int id { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string username { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string password { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string realname { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int? score { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string rank { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string pic { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int? level { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int? winnum { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int? gatenum { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int? allnum { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int? allinnum { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int? roomIndex { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public DateTime? registerDate { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string friends { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public int? type { get; set; }

}

public class Root
{
	/// <summary>
	/// 
	/// </summary>
	public List <User> user { get; set; }
}