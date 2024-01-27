using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{


	[SerializeField]
	protected int maxAmmo = 0;
	[SerializeField]
	public int currentAmmo = 0;
	[SerializeField]
	protected float reloadTime = 0;
	[SerializeField]
	protected float directDamage = 0;
	[SerializeField]
	protected float projectileLauchForceForward = 0;
	[SerializeField]
	protected float projectileLauchForceUp = 0;



	Effectivity.EffectType weakness;
	Effectivity.EffectType superEfectiveness;

    protected Mesh weaponMesh;
    [SerializeField]
	protected ProjectileBase projectile;
	[SerializeField]
	protected GameObject player;
	[SerializeField]
	protected Transform projectileSpawn;


	public bool isOnReload = false;
	[SerializeField]
	protected bool pressedReload = false;



	public virtual void Start()
	{
		currentAmmo = maxAmmo;
	}
	public virtual void UseGun() 
    {

		if(currentAmmo>0&&!isOnReload)
		{
			Shoot();
			currentAmmo--;

		}
		if((!isOnReload&&currentAmmo<1))
        {
            isOnReload = true;
            StartCoroutine(CoolDown());
        }
		
       
    }

	
	public void Reload()
	{
		isOnReload= true;
		StartCoroutine(CoolDown());
	}
    
	public abstract void Shoot();
    

    IEnumerator CoolDown()
    {

		yield return new WaitForSeconds(reloadTime);
		currentAmmo = maxAmmo;
        isOnReload = false;
		pressedReload = false;

	}


	
}
