using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana :ProjectileBase,IThrowable
{
	[SerializeField]
	private Transform playerHandPos;
	


	// Start is called before the first frame update
	void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Throw()
	{
		rb.AddForce(playerHandPos.forward * throwForceForward + playerHandPos.up * throwForceUp, ForceMode.Impulse);
	}


	public void PickUp()
	{
		playerHandPos = gameObject.transform.parent.transform;
	}
}
