using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static UnityEditor.SceneView;

public class Player : MonoBehaviour, IDamageableFoe
{
    [SerializeField] private Rigidbody rb;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public Cinemachine3rdPersonFollow thirdPersonFollow;

    [Header("Stats")]
    [SerializeField] private PlayerStats playerStats;
    private float _moveSpeed;

    
    [Header("Movement")]
    [SerializeField] private float speedMult;
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
    private float health;

    private void Awake()
    {
        playerStats.InitStats();
        _moveSpeed = playerStats.GetSpeed();
    }

    // Start is called before the first frame update
    private void Start()
    {
        thirdPersonFollow = cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    }
    public void OnReload(InputAction.CallbackContext context)
    {

    }
    public void OnCameraFlipX(InputAction.CallbackContext context)
    {
        if (thirdPersonFollow.CameraSide == 0)
        {
            thirdPersonFollow.CameraSide = 1;
        }
        else
        {
            thirdPersonFollow.CameraSide = 0;
        }
    }
    void Move()
    {
        Vector3 moveDirection = transform.forward * move.y + transform.right * move.x;
        Vector3 normalizedMoveDir = moveDirection.normalized * speedMult;
        rb.AddForce(normalizedMoveDir);
        if (rb.velocity.magnitude > _moveSpeed)
        {
            rb.velocity *= _moveSpeed;
        }
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

    public void TakeDamage(float damage)
    {
        playerStats.TakeDamage(damage);
    }
}
