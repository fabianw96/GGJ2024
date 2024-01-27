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
		Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit); 
		
		if(hit.collider.gameObject.GetComponent<IDamageableFoe>()!=null) 
		{
			hit.collider.gameObject.GetComponent<IDamageableFoe>().TakeDamage(directDamage);
		}
		
		
	}

	public void PickUp()
	{
		throw new System.NotImplementedException();
	}
}
