/*----------------------------------------
Filename:		ControllerHotDog.cs	
Created By:		Ryan Michaels
Created Date:	04/22/2018
Updated Date:	04/22/2018
----------------------------------------*/

using UnityEngine;

public class ControllerHotDog : MonoBehaviour
{
	public float spinSpeed;

	/// <summary>
	/// Run on collision
	/// </summary>
	/// <param name="collision"></param>
	void OnCollisionEnter( Collision collision )
	{
		//TODO: Doesn't seem to ignore Player inside truck collisions?
		if ( collision.gameObject.tag == "Player" )
		{
			Physics2D.IgnoreCollision( GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>() );
		}
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		GetComponent<Rigidbody2D>().rotation += spinSpeed * Time.deltaTime;
	}
}
