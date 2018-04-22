/*----------------------------------------
Filename:		ControllerRoad.cs	
Created By:		Ryan Michaels
Created Date:	04/20/2018
Updated Date:	04/20/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRoad : MonoBehaviour
{
	public static ControllerRoad controller;

	/// <summary>
	/// Runs one time at game object creation
	/// </summary>
	public void Awake()
	{
		controller = this;
	}

	public void Update()
	{
		
	}

}
