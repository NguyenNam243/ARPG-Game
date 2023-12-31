using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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
    public float weaponAngelXOffset = 15;
    public Transform groundCheck = null;
    public Transform weapon = null;
    public Transform spawmBulletPoint = null;
    public LayerMask groundMask;
    public LayerMask bulletContactMask;
    public float fireDelayTime = 0.2f;
    public GameObject bulletObj = null;
    public ParticleSystem explositionFx = null;
    public GameObject bomb = null;



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
        motion = cameraFollow.forward * moveZ + transform.right * moveX;
        _controller.Move(motion * moveSpeed * Time.deltaTime);

        Quaternion targetQuaternion = Quaternion.Euler(0, mouseX, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, lerpRate * Time.deltaTime);


        if (grounded)
            velocity.y = -1;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }

        velocity.y += gravity * Time.deltaTime;

        _controller.Move(velocity * Time.deltaTime);


        if (Input.GetMouseButton(0))
        {
            Vector3 fireTarget = GetFireTarget();
            if (fireTarget == Vector3.zero)
                return;

            if (countTime == 0)
                Fire(fireTarget);

            countTime += Time.deltaTime;

            if (countTime >= fireDelayTime)
                countTime = 0;
        }

        if (!isThrow && Input.GetKeyDown(KeyCode.Space))
            isThrow = true;

        if (isThrow)
        {
            Vector3 fireTarget = GetFireTarget();
            if (fireTarget == Vector3.zero)
                return;

            countTime += Time.deltaTime;
            float t = countTime / 2;

            float height = Vector3.Distance(spawmBulletPoint.position, fireTarget) * 0.75f;

            Vector3 p1 = Vector3.Lerp(spawmBulletPoint.position, fireTarget, 1 / 3f);
            p1 = new Vector3(p1.x, p1.y + height, p1.z);

            Vector3 p2 = Vector3.Lerp(spawmBulletPoint.position, fireTarget, 2 / 3f);
            p2 = new Vector3(p2.x, p2.y + (height * 0.75f), p2.z);

            Vector3 point = BezierMathf.GetBezierPoint(spawmBulletPoint.position, p1, p2, fireTarget, t);

            if (bombObj == null)
                bombObj = Instantiate(bomb);

            bombObj.transform.LookAt(point);
            bombObj.transform.position = point;

            if (!bombObj.activeInHierarchy)
                bombObj.SetActive(true);

            if (t >= 1)
            {
                explositionFx.transform.position = fireTarget;
                explositionFx.Play();
                Destroy(bombObj);
                isThrow = false;
                countTime = 0;
            }
        }
    }

    private bool isThrow = false;
    private GameObject bombObj = null;

    private void LateUpdate()
    {
        // ================= Camera =========================== //
        Quaternion cameraRotation = Quaternion.Euler(0 - mouseY, mouseX, 0);
        cameraFollow.rotation = Quaternion.Slerp(cameraFollow.rotation, cameraRotation, lerpRate * Time.deltaTime);
        cameraFollow.position = transform.position + Vector3.up * cameraTargetOffset.y + transform.right * cameraTargetOffset.x + transform.forward * cameraTargetOffset.z;

        Quaternion weaponRotation = Quaternion.Euler(cameraFollow.eulerAngles.x - weaponAngelXOffset, cameraFollow.eulerAngles.y - 4, cameraFollow.eulerAngles.z);
        weapon.rotation = Quaternion.Slerp(weapon.rotation, weaponRotation, lerpRate * Time.deltaTime);
    }


    private float countTime = 0f;
    private void Fire(Vector3 target)
    {
        GameObject obj = Instantiate(bulletObj);
        obj.transform.position = spawmBulletPoint.position;
        obj.gameObject.SetActive(true);
        Debug.DrawLine(spawmBulletPoint.position, target, Color.red, 1);
        BulletMove move = obj.GetComponent<BulletMove>();
        move.MoveToTarget(target);
    }

    private Vector3 GetFireTarget()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, bulletContactMask))
        {
            Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red, 1);
            return hitInfo.point;
        }
        return Vector3.zero;
    }
}
