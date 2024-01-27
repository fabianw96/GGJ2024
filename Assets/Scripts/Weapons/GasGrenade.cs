using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasGrenade : ProjectileBase, IThrowable
{
	[SerializeField]
	private Transform playerHandPos;

	public float gasDissolveTime = 0;

	bool activatedNade = false;
	bool nadeExploded = false;

	public void SetPlayerHandTransform(Transform parent)
	{
		playerHandPos = parent;
	}

	public void Throw()
	{
		rb.isKinematic = false;
		transform.parent = null;
		rb.AddForce(playerHandPos.forward * throwForceForward + playerHandPos.up * throwForceUp, ForceMode.Impulse);
		activatedNade = true;
		rb.detectCollisions = true;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (activatedNade)
		{
			nadeExploded = true;
			StartCoroutine(DissovleGas());
			activatedNade= false;
		}
	}

	public override void OnTriggerEnter(Collider other)
	{
		if(nadeExploded)
		{
			base.OnTriggerEnter(other);
		}
	}

	IEnumerator DissovleGas()
	{
		yield return new WaitForSeconds(gasDissolveTime);
		Destroy(gameObject);
	}
}
   

