using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] weapons;
    public FingerPistol fingerpistol;
    public GameObject WeaponPlaceHolder;
    public GameObject throwPlaceHolder;
  
    public int inventoryIndex = 0;

    private void Start()
    {
        weapons = new GameObject[3];
        weapons[0] = fingerpistol.gameObject;
        weapons[0].SetActive(true);
        weapons[1] = WeaponPlaceHolder.gameObject;
        weapons[1].SetActive(false);
        weapons[2] = throwPlaceHolder.gameObject;
        weapons[2].SetActive(false);
    }
}
