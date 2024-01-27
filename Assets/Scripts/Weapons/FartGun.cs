using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartGun :WeaponBase,ICollectable
{

	public CapsuleCollider trigger;

	public bool recentlyFarted = false;
	public float fartDissolveTime = 0;

	public override void Start()
	{
		base.Start();
		trigger.enabled = false;
		

	}

	public override void Shoot()
	{
		if (!recentlyFarted)
		{
			trigger.enabled = true;
			StartCoroutine(DissolveFart());
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.GetComponent<IDamageableFoe>() != null)
		{
			other.gameObject.GetComponent<IDamageableFoe>().TakeDamage(directDamage);
			
		}
	}

	IEnumerator DissolveFart()
	{
		yield return new WaitForSeconds(fartDissolveTime);
		recentlyFarted= false;
		trigger.enabled = false;
	}

	public void PickUp()
	{
		throw new System.NotImplementedException();
	}
}
