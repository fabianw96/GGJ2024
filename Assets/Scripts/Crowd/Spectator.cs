using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectator : MonoBehaviour 
{
	public Rigidbody rb;
	public MeshRenderer render;


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		render = rb.GetComponent<MeshRenderer>();
		
	}
	public void Hop()
	{
		rb.AddForce(Vector3.up*2.0f,ForceMode.Impulse);
	}
}

