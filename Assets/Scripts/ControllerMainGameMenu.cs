/*----------------------------------------
Filename:		ControllerMainGameMenu.cs	
Created By:		Ryan Michaels
Created Date:	04/20/2018
Updated Date:	04/20/2018
----------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerMainGameMenu : ControllerMenu
{
	public Canvas canvasPause;

	/// <summary>
	/// First thing to run.
	/// </summary>
	private void Awake()
	{
		// Hide pause menu
		canvasPause.gameObject.SetActive( false );
	}

	/// <summary>
	/// Pause the game and show the menu.
	/// </summary>
	public void Pause()
	{
		canvasPause.gameObject.SetActive( true );
	}

	/// <summary>
	/// Resume the game and hide the menu.
	/// </summary>
	public void Resume()
	{
		canvasPause.gameObject.SetActive( false );
	}
}
