using Cinemachine;
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static UnityEditor.SceneView;

public class Player : MonoBehaviour,IDamageableFoe
{
    // [SerializeField] private Rigidbody rb;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public Cinemachine3rdPersonFollow ThirdPersonFollow;
    public Transform WeaponHolder;
    public Inventory inventory;
    [SerializeField]
    private float interactDistance = 10f;


    [Header("Movement")]
    // [SerializeField] private float groundDrag;
    [SerializeField] private float currentVelocity;
    [SerializeField] private CharacterController characterController;
    Vector2 move;

    [Header("Look")]
    [SerializeField] private Vector2 mouseSensitivity;
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
    [SerializeField] private PlayerStats playerStats;
    private float _moveSpeed;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    private int _velocityHash = Animator.StringToHash("PlayerVelocity");

    float mouseScrollInput;
    bool swappedWeapon = false;
    
    [Header("Gravity")] 
    [SerializeField] private float gravityValue = -9.81f;
    private Vector3 _characterVelocity;

    private void Awake()
    {
        playerStats.InitStats();
        _moveSpeed = playerStats.GetSpeed();
    }

    // Start is called before the first frame update
    private void Start()
    {
        ThirdPersonFollow = cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        // rb = GetComponent<Rigidbody>();
        // rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimationState();
        ManageCurrentWeapon();
        GroundCheck();
    }

    private void PlayerAnimationState()
    {
        animator.SetFloat(_velocityHash, currentVelocity, 0.05f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Move();
        HandleGravity();
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
        if (context.performed && isGrounded)
        {
            Jump();
        }
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
                inventory.weapons[inventory.inventoryIndex] = inventory.throwPlaceHolder;


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

            if(inventory.weapons[1] != inventory.WeaponPlaceHolder)
            {
                inventory.weapons[1].transform.parent = null;

				inventory.weapons[1].SetActive(true);
				inventory.weapons[1].transform.position = hit.transform.position;
			}
            
            inventory.weapons[1] = hit.transform.GetComponent<WeaponBase>().gameObject;
            inventory.weapons[1].transform.SetParent(WeaponHolder, false);
		    inventory.weapons[1].transform.position = WeaponHolder.position;
			inventory.weapons[1].transform.localRotation= Quaternion.identity;
		   inventory.weapons[1].SetActive(false);
           
        }
        else if (hit.transform.GetComponent<IThrowable>() != null)
        {
			if (inventory.weapons[2] != inventory.throwPlaceHolder)
			{
				return;
			}


			hit.transform.GetComponent<IThrowable>().SetPlayerHandTransform(WeaponHolder);
            hit.transform.GetComponent<ProjectileBase>().rb.isKinematic = true;
			hit.transform.GetComponent<ProjectileBase>().rb.detectCollisions = false;
			inventory.weapons[2] = hit.transform.gameObject;
            inventory.weapons[2].transform.SetParent(WeaponHolder,false);
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
                swappedWeapon = true;
            }
            else if (mouseScrollInput < 0)
            {
                inventory.weapons[inventory.inventoryIndex].SetActive(false);
                inventory.inventoryIndex--;
                inventory.inventoryIndex = Mathf.Clamp(inventory.inventoryIndex, 0, 2);
                swappedWeapon = true;

            }
        }
    }

    public void OnZoom(InputAction.CallbackContext Context)
    {
        if(Context.started)
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = 45;
        }
        
        if (Context.canceled)
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = 60;
        }
    }
    void Move()
    {
        Vector3 moveDirection = transform.forward * move.y + transform.right * move.x;
        characterController.Move(moveDirection * (Time.fixedDeltaTime * _moveSpeed));
        currentVelocity = characterController.velocity.magnitude;
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
    
    private void HandleGravity()
    {
        if (isGrounded && _characterVelocity.y < 0)
        {
            _characterVelocity.y = 0f;
        }

        _characterVelocity.y += gravityValue;
        characterController.Move(_characterVelocity * Time.fixedDeltaTime);
    }

    void Jump()
    {
        _characterVelocity.y += Mathf.Sqrt((jumpForce - 1) * -2f * gravityValue);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, playerWidth, Ground);
    }

    private void ManageCurrentWeapon()
    {
        if (swappedWeapon)
        {
            swappedWeapon= false;
            inventory.weapons[inventory.inventoryIndex].SetActive(true);
			if(inventory.weapons[inventory.inventoryIndex].GetComponent<WeaponBase>() != null)
            {
                if (inventory.weapons[inventory.inventoryIndex].GetComponent<WeaponBase>().isOnReload)
                {
                    Debug.Log("Swap Reloading");
                    inventory.weapons[inventory.inventoryIndex].GetComponent<WeaponBase>().Reload();
                }
            }
		}
    }

    public void TakeDamage(float damage)
    {
        playerStats.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, playerWidth);
    }
}
