/*----------------------------------------
Filename:		ControllerMenu.cs	
Created By:		Ryan Michaels
Created Date:	04/20/2018
Updated Date:	04/20/2018
----------------------------------------*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMenu : MonoBehaviour
{
	public void Navigation_Options()
	{
		Debug.Log( "We are moving to the options screen." );
		SceneManager.LoadScene( "Options", LoadSceneMode.Additive );
	}

	public void Navigation_Quit()
	{
		Debug.Log( "We are quitting the game!" );
		Application.Quit();
	}

	public void Navigation_Start()
	{
		Debug.Log( "We are starting the game!" );
		SceneManager.LoadScene( "MainGame" );
	}

	public void Navigation_Title()
	{
		SceneManager.LoadScene( "Title" );
	}

	/// <summary>
	/// Runs every frame. Check for typical input.
	/// </summary>
	public virtual void Update()
	{
		if ( Input.GetKeyDown( KeyCode.Escape ) )
		{
			Debug.Log( "We are quitting the game!" );
			Application.Quit();
		}
	}
}
