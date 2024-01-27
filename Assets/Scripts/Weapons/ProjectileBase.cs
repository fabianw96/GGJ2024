using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
	[SerializeField]
	protected float damage = 0;

	[SerializeField]
	protected float throwForceForward = 0;
	[SerializeField]
	protected float throwForceUp= 0;


	Effectivity.EffectType weakness;
	Effectivity.EffectType superEfectiveness;

	public Rigidbody rb;

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<IDamageableFoe>() != null)
		{
			other.gameObject.GetComponent<IDamageableFoe>().TakeDamage();
			Destroy(this);
		}
	}



}
