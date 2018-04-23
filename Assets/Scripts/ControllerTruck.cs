/*----------------------------------------
Filename:		ControllerTruck.cs	
Created By:		Ryan Michaels
Created Date:	04/20/2018
Updated Date:	04/21/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerTruck : MonoBehaviour
{
	public static ControllerTruck truck;

	private const float TRUCKZPOSITION = -1.5f;

	public GameObject hotDog;
	public Transform player;
	public GameObject truckBorder;

	public bool isAccelerating;
	public bool isDecelerating;
	public bool isPlayerMoving;

	public float acceleration = .5f;
	public float backwardsAcceleration = .5f;
	public float deceleration = 5f;
	public float horizontalSpeed = 5f;
	public float hotDogMovingSpeed = 5f;
	public float maxSpeed = 50f;
	public float maxBackwardsSpeed = -20f;
	public float maxHorizontalPosition = 4.5f;
	public float playerSpeed = 1f;
	public float rotationSpeed = 1f;
	public float verticalSpeed = 1f;

	private Vector2 lastPosition;

	public Vector3 playerTargetPosition;

	public void Awake()
	{
		truck = this;
	}

	public void Update()
	{
		// Detect rotation and fix
		float zrot = transform.rotation.z;
		if ( zrot > 0f || zrot < 0f )
		{
			zrot += rotationSpeed * Time.deltaTime;
		}

		Quaternion trot = Quaternion.Euler( 0f, 0f, zrot );
		transform.rotation = Quaternion.Slerp( transform.rotation, trot, Time.deltaTime * 5.0f );

		float xpos = transform.position.x;
		float ypos = transform.position.y;

		//TODO: Handle changing direction from up to down, need to go to zero first. Currently going from +30 to -30

		// Handle acceleration
		if ( Input.GetAxis( "Vertical" ) > 0f )
		{
			isAccelerating = true;
			isDecelerating = false;

			if ( verticalSpeed < maxSpeed )
			{
				verticalSpeed += acceleration;
			}
		}
		else if ( Input.GetAxis( "Vertical" ) < 0f )
		{
			isAccelerating = false;
			isDecelerating = true;

			if ( verticalSpeed > 0 )
			{
				// We're still moving forward, just decelerate to 0
				verticalSpeed -= deceleration;
			}
			else if ( verticalSpeed > maxBackwardsSpeed )
			{
				verticalSpeed -= backwardsAcceleration;
			}
		}
		else
		{
			isAccelerating = false;
			isDecelerating = false;

			if ( verticalSpeed > 0f )
			{
				verticalSpeed -= deceleration;

				if ( verticalSpeed < 0f )
				{
					verticalSpeed = 0f;
				}
			}

			if ( verticalSpeed < 0f )
			{
				verticalSpeed += deceleration;

				if ( verticalSpeed > 0f )
				{
					verticalSpeed = 0f;
				}
			}
		}

		// Set new positions (negative so we affect the road properly)
		xpos += horizontalSpeed * Time.deltaTime * Input.GetAxis( "Horizontal" );
		ypos += verticalSpeed * Time.deltaTime;

		// Handle clamps for road (truck)
		if ( xpos > maxHorizontalPosition )
		{
			xpos = maxHorizontalPosition;
		}
		if ( xpos < -maxHorizontalPosition )
		{
			xpos = -maxHorizontalPosition;
		}

		// Apply to the truck
		lastPosition = transform.position;
		transform.position = new Vector3( xpos, ypos );

		// Update player
		if ( isPlayerMoving )
		{
			Vector3 movement = new Vector3( playerSpeed * playerTargetPosition.x, playerSpeed * playerTargetPosition.y, 0f );
			movement *= Time.deltaTime;

			player.transform.Translate( movement );

			if ( player.transform.position == playerTargetPosition )
			{
				isPlayerMoving = false;
			}
		}
	}

	/// <summary>
	/// Move the player inside the truck to the mouse position
	/// </summary>
	/// <param name="position"></param>
	public void MovePlayer( Vector2 position )
	{
		//player.transform.Translate( new Vector3( position.x, position.y, 0f ) );
		isPlayerMoving = true;
		playerTargetPosition = new Vector3( position.x, position.y, TRUCKZPOSITION );
	}

	/// <summary>
	/// Shoot hotdog.
	/// </summary>
	public void Shoot()
	{
		// Shoot
		GameObject newHotDog = Instantiate( hotDog, transform.position, Quaternion.identity );

		// TODO: Need to take into account truck velocity
		Vector2 mouseTarget = Camera.main.ScreenToWorldPoint( new Vector2( Input.mousePosition.x, Input.mousePosition.y ) );
		Vector2 current = new Vector2( lastPosition.x, lastPosition.y );
		//Debug.Log( "Current: " + current + ", target: " + target + ", mouseTarget: " + mouseTarget );

		Vector2 velocity = mouseTarget - current;
		//Debug.Log( "Velocity: " + velocity );

		// Get vector of the moving truck
		Vector2 movingVelocity = new Vector2( transform.position.x, transform.position.y ) - lastPosition;
		Debug.Log( "Moving velocity: " + movingVelocity );

		velocity = velocity + movingVelocity;

		newHotDog.GetComponent<Rigidbody2D>().velocity = velocity.normalized * hotDogMovingSpeed;

		// Remove hotdog after 5 seconds
		Destroy( newHotDog, 5f );
	}
}
