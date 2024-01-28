using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Image primaryWeapon;
    [SerializeField] private Image secondaryWeapon;
    [SerializeField] private Image grenade;
    [SerializeField] private TextMeshProUGUI primarySlotNumber;
    [SerializeField] private TextMeshProUGUI secondarySlotNumber;
    [SerializeField] private TextMeshProUGUI grenadeSlotNumber;
    [SerializeField] private Slider slider;
    [SerializeField] private float points;


    private void Awake()
    {
        
        player = FindObjectOfType<Player>();
        playerStats = player.GetComponent<PlayerStats>();
        inventory = player.GetComponent<Inventory>();
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        UpdateSelectedWeapon();
        UpdateHappiness();
        UpdateScore();
    }

    public void UpdateScore()
    {
        points = GameManager.Instance.GetPoints();
        slider.value = points;
    }

    public void UpdateHappiness()
    {
        if (playerStats.GetHappiness() <= 0f)
        {
        }
    }

    public void UpdateSelectedWeapon() 
    {
        Debug.Log(inventory.inventoryIndex);
        if (inventory.inventoryIndex == 0)
        {
            Debug.Log("Primary Selected");
            primaryWeapon.gameObject.SetActive(true);
            secondaryWeapon.gameObject.SetActive(false);
            grenade.gameObject.SetActive(false);

            primarySlotNumber.fontStyle = FontStyles.Bold;
            secondarySlotNumber.fontStyle = FontStyles.Normal;
            grenadeSlotNumber.fontStyle = FontStyles.Normal;
        }
        else if (inventory.inventoryIndex == 1)
        {
            Debug.Log("Secondary Selected");
            primaryWeapon.gameObject.SetActive(false);
            secondaryWeapon.gameObject.SetActive(true);
            grenade.gameObject.SetActive(false);

            primarySlotNumber.fontStyle = FontStyles.Normal;
            secondarySlotNumber.fontStyle = FontStyles.Bold;
            grenadeSlotNumber.fontStyle = FontStyles.Normal;
        }
        else if (inventory.inventoryIndex == 2)
        {
            Debug.Log("Grenade Selected");
            primaryWeapon.gameObject.SetActive(false);
            secondaryWeapon.gameObject.SetActive(false);
            grenade.gameObject.SetActive(true);

            primarySlotNumber.fontStyle = FontStyles.Normal;
            secondarySlotNumber.fontStyle = FontStyles.Normal;
            grenadeSlotNumber.fontStyle = FontStyles.Bold;
        }
    }

    public void UpdateDashAmount() 
    {

    }
}
