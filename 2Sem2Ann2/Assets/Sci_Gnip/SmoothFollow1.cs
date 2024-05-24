using System;
using UnityEngine;

[ExecuteInEditMode]
public class SmoothFollow1 : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    public float depth;
    public float angle;

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
        Vector3 targetPosition = target.position + rotation * (Vector3.up * depth);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}