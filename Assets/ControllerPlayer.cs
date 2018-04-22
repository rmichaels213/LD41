using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour {

	void OnCollisionEnter( Collision col )
	{
		if ( col.gameObject.name == "truckBorder" )
		{
			ControllerTruck.truck.isPlayerMoving = false;
		}
	}
}
