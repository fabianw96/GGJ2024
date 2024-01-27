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
		rb.isKinematic = false;
		transform.parent = null;
		rb.AddForce(playerHandPos.forward * throwForceForward + playerHandPos.up * throwForceUp, ForceMode.Impulse);
		rb.detectCollisions = true;
	}

	public void SetPlayerHandTransform(Transform parent)
	{
		playerHandPos= parent;
	}
	public override void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == null) {
			base.OnTriggerEnter(other); 
		}
	}
}
