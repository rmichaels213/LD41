/*----------------------------------------
Filename:		ControllerCanvasMainMenu.cs	
Created By:		Ryan Michaels
Created Date:	04/20/2018
Updated Date:	04/20/2018
----------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerCanvasMainMenu : ControllerMenu
{
	public Button buttonContinue;
	/// <summary>
	/// Runs every frame.
	/// </summary>
	public override void Update()
	{
		buttonContinue.enabled = false;
	}
}
