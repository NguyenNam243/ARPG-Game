using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public ParticleSystem hitEffect = null;

    public float moveSpeed = 100f;

    private Rigidbody body = null;


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void MoveToTarget(Vector3 target)
    {
        transform.LookAt(target);
        body.AddForce(transform.forward * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 contact = other.transform.position;
        hitEffect.transform.SetParent(null);
        hitEffect.transform.position = contact;
        hitEffect.gameObject.SetActive(true);
        hitEffect.Play();
        Destroy(gameObject);
    }
}
