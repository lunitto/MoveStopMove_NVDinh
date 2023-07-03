using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speedFollow;
    [SerializeField] private Vector3 currentOffsetTarget;
    [SerializeField] private Vector3 currentOffsetPosition;
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + currentOffsetPosition, speedFollow * Time.fixedDeltaTime);
        transform.LookAt(target.position + currentOffsetTarget);
        
    }
}
