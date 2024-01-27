using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{

    [SerializeField]
    private WeaponBase bananaCannon;

	[SerializeField]
	private WeaponBase fartGun;

	[SerializeField]
	private WeaponBase fingerPistol;

    [SerializeField]
    private IThrowable banana;

	[SerializeField]
	private IThrowable gasGrenade;

	[SerializeField]
	private IThrowable pancake;

	[SerializeField]
	private IThrowable waffleShuriken;


	bool gunFireInput = false;

	// Start is called before the first frame update
	void Start()
    {
        
    }
	private void Update()
	{

		if(Input.GetMouseButtonDown(0))
		{
			gunFireInput= true;

		}
		else if(Input.GetMouseButtonUp(0))
		{
			gunFireInput = false;
		}
		if (Input.GetKeyDown("r"))
		{
			bananaCannon.Reload();

		}

	}
	// Update is called once per frame
	void FixedUpdate()
    {
        if(gunFireInput)
		{
			bananaCannon.UseGun();
			gunFireInput = false;
			
		}
		
    }

	
}
