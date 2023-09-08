using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Camera Follow")]
    public Transform cameraFollow = null;
    public float sensivity_x = 1;
    public float sensivity_y = 1;
    public float lerpRate = 40f;
    public Vector2 maxAngleY;
    public Vector3 cameraTargetOffset;

    [Header("Character Controller")]
    public float moveSpeed = 10f;
    public float gravity = -5f;
    public float jumpHeight = 1f;
    public Transform groundCheck = null;
    public LayerMask groundMask;


    private float mouseX;
    private float mouseY;

    private float moveX;
    private float moveZ;

    private bool grounded = false;

    private Vector3 motion;
    private Vector3 velocity;
    private UnityEngine.CharacterController _controller = null;


    private void Awake()
    {
        _controller = GetComponent<UnityEngine.CharacterController>();
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");

        mouseY = 0 - Mathf.Clamp(0 - mouseY, maxAngleY.x, maxAngleY.y);

        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        grounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

        // ================= Character =========================== //
        motion = transform.forward * moveZ + transform.right * moveX;
        _controller.Move(motion * moveSpeed * Time.deltaTime);

        if (grounded)
            velocity.y = -1;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }

        velocity.y += gravity * Time.deltaTime;

        _controller.Move(velocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        // ================= Camera =========================== //
        Quaternion cameraRotation = Quaternion.Euler(0 - mouseY, mouseX, 0);
        cameraFollow.rotation = Quaternion.Slerp(cameraFollow.rotation, cameraRotation, lerpRate * Time.deltaTime);
        cameraFollow.position = transform.position + Vector3.up * cameraTargetOffset.y + transform.right * cameraTargetOffset.x + transform.forward * cameraTargetOffset.z;

        Quaternion characterRotation = Quaternion.Euler(0, mouseX, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, characterRotation, lerpRate * Time.deltaTime);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("hit");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }



}
