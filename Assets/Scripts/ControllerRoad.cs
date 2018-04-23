/*----------------------------------------
Filename:		ControllerRoad.cs	
Created By:		Ryan Michaels
Created Date:	04/22/2018
Updated Date:	04/22/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRoad : MonoBehaviour
{
	public GameObject roadPrefabBlock;
	public GameObject roadPrefab1;

	public int currentRoad;
	private List<GameObject> roads;
	private float offset = 28f;

	private void AddNewRoad()
	{
		GameObject newRoad = Instantiate( roadPrefab1, new Vector3( 0f, roads.Count * offset, 0f ), Quaternion.identity );
		roads.Add( newRoad );
	}

	/// <summary>
	/// Runs when object is created.
	/// </summary>
	public void Start()
	{
		currentRoad = 0;
		roads = new List<GameObject>
		{
			gameObject
		};

		// Add blocking road at bottom
		Instantiate( roadPrefabBlock, new Vector3( 0f, -offset, 0f ), Quaternion.identity );

		// Add a new road to start
		AddNewRoad();
	}

	/// <summary>
	/// Runs every frame.
	/// </summary>
	public void Update()
	{
		if ( ControllerTruck.truck.transform.position.y > ( roads[currentRoad].gameObject.transform.position.y + ( offset / 2 ) ) )
		{
			Debug.Log( "Moving current road up one." );
			currentRoad++;
		}

		if ( currentRoad > 0 )
		{
			if ( ControllerTruck.truck.transform.position.y < ( roads[currentRoad - 1].gameObject.transform.position.y + ( offset / 2 ) ) )
			{
				Debug.Log( "Moving current road down one." );
				currentRoad--;
			}
		}

		while ( ( roads.Count - 1 ) <= currentRoad )
		{
			AddNewRoad();
		}
	}

}
