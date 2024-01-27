using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaCannon : WeaponBase,ICollectable
{

	private void Awake()
	{
		weaponMesh = GetComponent<Mesh>();
	}
	// Start is called before the first frame update
	public override void Start()
    {
		base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
		
	}


	public override void Shoot()
	{
		ProjectileBase bananaProjectile = Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
		bananaProjectile.rb.AddForce(projectileSpawn.forward*projectileLauchForceForward+projectileSpawn.up*projectileLauchForceUp,ForceMode.Impulse);
		
	}

	public void PickUp()
	{
		throw new System.NotImplementedException();
	}
}
