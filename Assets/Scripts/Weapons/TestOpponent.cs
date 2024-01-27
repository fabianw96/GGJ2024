using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOpponent : MonoBehaviour,IDamageableFoe
{
	
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void TakeDamage(float damage)
	{
        Debug.Log("Took Damage");
	}
}
