using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : ProjectileBase, IThrowable
{
	[SerializeField]
	private Transform playerHandPos;


	public void SetPlayerHandTransform(Transform parent)
	{
		playerHandPos = parent;
	}

	public void Throw()
	{
		rb.isKinematic = false;
		transform.parent = null;
		rb.AddForce(playerHandPos.forward * throwForceForward + playerHandPos.up * throwForceUp, ForceMode.Impulse);
		rb.detectCollisions = true;

	}
	public override void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>() == null)
		{
			base.OnTriggerEnter(other);
		}
	}

}
