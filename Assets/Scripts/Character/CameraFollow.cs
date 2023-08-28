using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget = null;
    public float slerpValue = 5f;
    public float heightFollow;
    public float angleFollow;
    public float distanceFollow;


    private void Awake()
    {
        transform.position = followTarget.transform.position + Vector3.forward * distanceFollow + Vector3.up * heightFollow; ;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = followTarget.transform.position + Vector3.forward * distanceFollow + Vector3.up * heightFollow;
        //transform.position = new Vector3(followTarget.position.x , followTarget.position.y + heightFollow, followTarget.position.z + distanceFollow);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
        //transform.position = targetPos;
        transform.LookAt(followTarget.position);
    }
}
