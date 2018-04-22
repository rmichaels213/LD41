using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHotDog : MonoBehaviour
{
	public float speed;

	// Use this for initialization
	void Start()
	{
		this.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(this.transform.position.x, speed);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
