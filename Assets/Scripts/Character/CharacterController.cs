using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public LayerMask groundMask;

    public float moveSpeed = 3f;
    public float andularSpeed = 5f;

    private NavMeshAgent agent = null;
    private Animator ator = null;

    private Vector3 destination = Vector3.zero;
    private float atorSpped = 0;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        ator = GetComponent<Animator>();
    }

    private void Update()
    {
        //agent.SetDestination(destination.position);

        if (Input.GetMouseButton(1))
            destination = GetPositionOnGround();

        if (destination == Vector3.zero)
            return;

        bool followTarget = OnFollowDestination(destination);
        atorSpped = Mathf.Lerp(atorSpped, followTarget ? 1 : 0, 0.05f);
        ator.SetFloat("Speed", atorSpped);

        if (!followTarget)
            return;

        RotateToDestination(destination);
        MoveToDestination();
    }

    private void RotateToDestination(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        direction.y = transform.forward.y;

        Quaternion destinationQuaternion = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, destinationQuaternion, andularSpeed * Time.deltaTime);
    }

    private void MoveToDestination()
    {
        if (agent.isOnNavMesh)
            agent.Move(transform.forward * moveSpeed * Time.deltaTime);
    }

    private bool OnFollowDestination(Vector3 destination)
    {
        float distance = Vector3.Distance(transform.position, destination);
        return distance > agent.radius * 1.5f;
    }

    private Vector3 GetPositionOnGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundMask))
        {
            return hitInfo.point;
        }
        return Vector3.zero;
    }
}
