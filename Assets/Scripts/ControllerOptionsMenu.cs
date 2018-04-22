/*----------------------------------------
Filename:		ControllerOptionsMenu.cs	
Created By:		Ryan Michaels
Created Date:	04/20/2018
Updated Date:	04/20/2018
----------------------------------------*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerOptionsMenu : ControllerMenu
{
	public void Navigation_Back()
	{
		SceneManager.UnloadSceneAsync( "Options" );
	}
}
