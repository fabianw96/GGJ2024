using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerPistol : WeaponBase,ICollectable
{

	public override void Start()
	{
		base.Start();
	}

	public override void Shoot()
	{
		RaycastHit hit;
		if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
		{
			return;
		}; 
		
		if(hit.collider.gameObject.GetComponent<IDamageableFoe>()!=null) 
		{
			hit.collider.gameObject.GetComponent<IDamageableFoe>().TakeDamage();
		}
		
		
	}

	public void PickUp()
	{
		throw new System.NotImplementedException();
	}
}
