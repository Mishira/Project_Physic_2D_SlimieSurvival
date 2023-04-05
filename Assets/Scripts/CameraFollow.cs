using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("===== Basic Setting =====")]
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    
    [Header("===== Get Component =====")]
    [SerializeField] private Transform targetFollowing;

    
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector3 targetPosition = targetFollowing.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
