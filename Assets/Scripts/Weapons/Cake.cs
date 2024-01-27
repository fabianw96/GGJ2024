using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : ProjectileBase, IThrowable
{
	[SerializeField]
	private Transform playerHandPos;
	public void PickUp()
	{

		playerHandPos = gameObject.transform.parent.transform;
	}

	public void Throw()
	{
		rb.AddForce(playerHandPos.forward * throwForceForward + playerHandPos.up * throwForceUp, ForceMode.Impulse);
	}
}
