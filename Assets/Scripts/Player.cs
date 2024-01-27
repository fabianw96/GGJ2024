using Cinemachine;
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.SceneView;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public Cinemachine3rdPersonFollow ThirdPersonFollow;
    public Transform WeaponHolder;
    public Inventory inventory;
    [SerializeField]
    private float interactDistance = 10f;


    [Header("Movement")]
    [SerializeField] private float movespeed;
    [SerializeField] private float groundDrag;
    Vector2 move;

    [Header("Look")]
    [SerializeField] private Vector2 mouseSensitivity;
    [SerializeField] private float rotationStrength;
    [SerializeField] private float lookRotation;
    public Transform cameraHolder;
    public float lookXLimit = 60.0f;
    Vector2 look;

    [Header("Jump")]
    [SerializeField] private float jumpForce;

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private Transform groundCheck;

    [Header("Player Values")]
    [SerializeField] private float playerWidth;
    [SerializeField] private float playerHeight;

    float mouseScrollInput;

    // Start is called before the first frame update
    private void Start()
    {
        ThirdPersonFollow = cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageCurrentWeapon();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        Look();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inventory.weapons[inventory.inventoryIndex].GetComponent<WeaponBase>() != null)
            {
                Debug.Log(inventory.weapons[0].name);
                inventory.weapons[inventory.inventoryIndex].GetComponent<WeaponBase>().UseGun();
            }
            else if (inventory.weapons[inventory.inventoryIndex].GetComponent<IThrowable>() != null)
            {
                inventory.weapons[inventory.inventoryIndex].GetComponent<IThrowable>().Throw();
            }
        }
    }
    
    public void OnReload(InputAction.CallbackContext context)
    {
       
        if (inventory.weapons[inventory.inventoryIndex].GetComponent<WeaponBase>().isOnReload)
        {
            return;
        }

        if (context.started)
        {
           
            if (inventory.weapons[inventory.inventoryIndex].GetComponent<WeaponBase>() != null)
            {
                
                inventory.weapons[inventory.inventoryIndex].GetComponent<WeaponBase>().Reload();
            }
        }
    }
    
    public void OnCameraFlipX(InputAction.CallbackContext context)
    {
        if (ThirdPersonFollow.CameraSide == 0)
        {
            ThirdPersonFollow.CameraSide = 1;
        }
        else
        {
            ThirdPersonFollow.CameraSide = 0;
        }
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,interactDistance))
        {
            return;
        }
        if (hit.transform.GetComponent<WeaponBase>() != null)
        {
            
            inventory.weapons[1] = hit.transform.GetComponent<WeaponBase>().gameObject;
            inventory.weapons[1].transform.SetParent(WeaponHolder, false);
            inventory.weapons[1].transform.position = WeaponHolder.position;
            inventory.weapons[1].SetActive(false);
           
        }
        else if (hit.transform.GetComponent<IThrowable>() != null)
        {
            
             inventory.weapons[2] = hit.transform.gameObject;
             inventory.weapons[2].transform.SetParent(WeaponHolder, false);
             inventory.weapons[2].transform.position = WeaponHolder.position;
             inventory.weapons[2].SetActive(false);
        }
        
    }

    public void OnWeaponSwap(InputAction.CallbackContext context)
    {

        if (context.started) { 
        mouseScrollInput = context.ReadValue<float>();
        if (mouseScrollInput > 0)
        {
            inventory.weapons[inventory.inventoryIndex].SetActive(false);
            inventory.inventoryIndex++;
            inventory.inventoryIndex = Mathf.Clamp(inventory.inventoryIndex, 0, 2);
        }
        else if (mouseScrollInput < 0)
        {
            inventory.weapons[inventory.inventoryIndex].SetActive(false);
            inventory.inventoryIndex--;
            inventory.inventoryIndex = Mathf.Clamp(inventory.inventoryIndex, 0, 2);

        }
    }
    }
    void Move()
    {
        Vector3 moveDirection = transform.forward * move.y + transform.right * move.x;
        rb.AddForce(moveDirection.normalized * movespeed);
    }

    void Look()
    {
        // Turn
        transform.Rotate(Vector3.up * look.x * mouseSensitivity.x * Time.deltaTime);

        // Look
        lookRotation += (-look.y * mouseSensitivity.y * Time.deltaTime);
        lookRotation = Mathf.Clamp(lookRotation, -80, 80);
        cameraHolder.transform.eulerAngles = new Vector3(lookRotation, cameraHolder.transform.eulerAngles.y, cameraHolder.transform.eulerAngles.z);
    }

    void Jump()
    {
        Vector3 jumpStrength = Vector3.zero;

        if (isGrounded)
        {
            jumpStrength = Vector3.up * jumpForce;
            isGrounded = false;
        }

        rb.AddForce(jumpStrength, ForceMode.Force);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, playerWidth, Ground);
        // handle drag & Groundcheck
        if (isGrounded)
        {
            isGrounded = true;
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void ManageCurrentWeapon()
    {
        if (mouseScrollInput != 0)
        {
            inventory.weapons[inventory.inventoryIndex].SetActive(true);
        }

    }
}
