using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	private Rigidbody2D rigidBody;
	public float speed;
		
	void Start()
	{
		rigidBody= GetComponent<Rigidbody2D>();
		rigidBody.velocity = transform.right * -speed;
		
	}
}
