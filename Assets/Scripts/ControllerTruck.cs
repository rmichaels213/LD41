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

	public Camera mainCamera;
	public GameObject hotDog;
	public Transform player;
	public GameObject truckBorder;

	public bool isPlayerMoving;

	public float acceleration = .5f;
	public float backwardsAcceleration = .5f;
	public float cameraLagTime = 30f;
	public float cameraAcceleration = .5f;
	public float cameraDeceleration = .1f;
	public float deceleration = 5f;
	public float horizontalSpeed = 5f;
	public float maxCameraHeight = 10f;
	public float minCameraHeight = 5f;
	public float maxSpeed = 50f;
	public float maxBackwardsSpeed = -20f;
	public float maxHorizontalPosition = 4.5f;
	public float playerSpeed = 1f;
	public float rotationSpeed = 1f;
	public float verticalSpeed = 1f;

	public Vector3 playerTargetPosition;

	public void Awake()
	{
		truck = this;
	}

	public void LateUpdate()
	{
		// Detect rotation and fix
		float zrot = transform.rotation.z;
		if ( zrot > 0f || zrot < 0f )
		{
			zrot += rotationSpeed * Time.deltaTime;
		}

		Quaternion trot = Quaternion.Euler( 0f, 0f, zrot );
		transform.rotation = Quaternion.Slerp( transform.rotation, trot, Time.deltaTime * 5.0f );

		// Detect sliding movement
		if ( transform.position.x != 0f || transform.position.y != 0f )
		{
			transform.position = new Vector3( 0f, 0f );
		}

		float xpos = ControllerRoad.controller.transform.position.x;
		float ypos = ControllerRoad.controller.transform.position.y;

		//TODO: Handle changing direction from up to down, need to go to zero first. Currently going from +30 to -30

		// Handle acceleration
		if ( Input.GetAxis( "Vertical" ) > 0f )
		{
			if ( verticalSpeed < maxSpeed )
			{
				verticalSpeed += acceleration;
			}
			if ( mainCamera.orthographicSize < maxCameraHeight )
			{
				mainCamera.orthographicSize += cameraAcceleration * Time.deltaTime;
			}
		}
		else if ( Input.GetAxis( "Vertical" ) < 0f )
		{
			if ( verticalSpeed > 0 )
			{
				// We're still moving forward, just decelerate to 0
				verticalSpeed -= deceleration;
			}
			else if ( verticalSpeed > maxBackwardsSpeed )
			{
				verticalSpeed -= backwardsAcceleration;
			}

			// Handle camera
			if ( mainCamera.orthographicSize < maxCameraHeight )
			{
				mainCamera.orthographicSize += cameraAcceleration * Time.deltaTime;
			}
		}
		else
		{
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

			if ( mainCamera.orthographicSize > minCameraHeight )
			{
				mainCamera.orthographicSize -= cameraDeceleration;
			}
		}

		// Set new positions (negative so we affect the road properly)
		xpos += horizontalSpeed * Time.deltaTime * -Input.GetAxis( "Horizontal" );
		ypos -= verticalSpeed * Time.deltaTime;

		// Handle clamps for road (truck)
		if ( xpos > maxHorizontalPosition )
		{
			xpos = maxHorizontalPosition;
		}
		if ( xpos < -maxHorizontalPosition )
		{
			xpos = -maxHorizontalPosition;
		}

		// Handle clamps for camera
		if ( mainCamera.orthographicSize > maxCameraHeight )
		{
			mainCamera.orthographicSize = maxCameraHeight;
		}
		if ( mainCamera.orthographicSize < minCameraHeight )
		{
			mainCamera.orthographicSize = minCameraHeight;
		}

		// Apply to road
		ControllerRoad.controller.transform.position = new Vector3( xpos, ypos );

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
	/// Shoot a hot dog
	/// </summary>
	public void ShootHotDog()
	{
		GameObject bPrefab = Instantiate( hotDog, new Vector3( transform.position.x, transform.position.y, transform.position.z ), Quaternion.identity );
		bPrefab.GetComponent<Rigidbody2D>().AddForce( Vector2.up * 10f );
	}
}
