/*----------------------------------------
Filename:		ControllerGameManager.cs	
Created By:		Ryan Michaels
Created Date:	04/21/2018
Updated Date:	04/21/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGameManager : MonoBehaviour
{
	public static ControllerGameManager controller;
	public GameObject hotdog;

	/// <summary>
	/// Runs one time at game creation.
	/// </summary>
	public void Awake()
	{
		if ( controller == null )
		{
			controller = this;
			DontDestroyOnLoad( controller );
		}
		else if ( this != controller )
		{
			Destroy( this );
		}
	}

	/// <summary>
	/// Raycast to see if we are clicking something
	/// </summary>
	public Collider2D CastRay()
	{
		Collider2D result = null;

		RaycastHit2D rayCast = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ), Vector2.zero );

		if ( rayCast.collider != null )
		{
			//Debug.Log( "Hey, we click on " + rayCast.collider.name );
			result = rayCast.collider;
		}

		return result;
	}

	/// <summary>
	/// Shoot a hot dog in the dorection of the collider
	/// </summary>
	private void ShootHotDog()
	{
		Debug.Log( "Shooting a hot dog at (" + Input.mousePosition.x + "," + Input.mousePosition.y + ")" );

		GameObject shootingDog = Instantiate( hotdog );
	}

	/// <summary>
	/// Rusn every frame.
	/// </summary>
	public void Update()
	{
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Collider2D collider = CastRay();
			if ( collider != null && collider.name == "truck" )
			{
				// We are clicking the truck, move the player
				Debug.Log( "Clicked the truck." );
				Vector2 target = Camera.main.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0f ) );
				ControllerTruck.truck.MovePlayer( target );
			}
			else
			{
				// We are probably trying to shoot something! Shoot in that direction, if we have hot dogs, of course.
				ShootHotDog();
			}
		}
		else if ( Input.GetMouseButtonDown( 1 ) )
		{
			ControllerTruck.truck.ShootHotDog();
		}
	}
}
