using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 2, -10));
        targetPosition.y = Mathf.Max(targetPosition.y, 2f);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    void Update()
    {
        
    }
}
